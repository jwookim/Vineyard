using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DIRECT
{
    DIR_FRONT,
    DIR_BACK,
    DIR_LEFT,
    DIR_RIGHT
}

enum STATE
{
    STATE_NORMAL,
    STATE_LIFT,
    STATE_PUSH
}

public abstract class Character : MonoBehaviour
{
    const float DustGenTime = 0.07f;  // 달리기 시 먼지 생성 시간

    protected const float DefaultSpeed = 4f;  // 기본 이동속도
    protected const float slowScale = 0.5f;
    const float RunScale = 1.5f;  // 달리기 배율
    const float StairSlow = 0.3f; // 계단 위 감속

    const float KnockBackTime = 0.5f;
    const float KnockBackSpeed = 1f;

    const float TrapCooldown = 1f;
    const float TrapDmg = 10f;

    const float DmgCooldown = 0.5f;

    const float DodgeCooldown = 3f;

    [SerializeField] private Animator animator;
    private GameObject[] CharDir = new GameObject[4];  // 방향별 모델링

    protected DIRECT curDir; // 캐릭터가 보는 방향

    protected Vector2 direction;  // 움직이는 방향

    private bool isRun;  // 달리기 여부

    protected Weapon weapon;

    [SerializeField] protected GameObject HpBar;

    private STATE curState;

    private bool Controlable;  // 컨트롤 가능 여부

    private bool isAttack; // 공격중인가?
    protected bool attackOrder;

    private float dodgeTime;

    private Coroutine MovingFunc;

    protected delegate IEnumerator delegateCoroutine();

    protected delegateCoroutine AttackCoroutine;

    private Objects InteractObject;

    private Hashtable AttackedTime;

    private float TrapHitTime;

    private bool onStair;

    ////////////// 스테이터스 ////////////////

    protected float Speed;

    protected float Power;

    protected float Defensive;
    protected float MagicResist;

    protected float MaxHp;

    [SerializeField]protected float curHp;

    //////////////////////
    protected virtual void Awake()
    {
        CharDir[(int)DIRECT.DIR_FRONT] = gameObject.transform.Find("Front").gameObject;
        CharDir[(int)DIRECT.DIR_BACK] = gameObject.transform.Find("Back").gameObject;
        CharDir[(int)DIRECT.DIR_LEFT] = gameObject.transform.Find("Left").gameObject;
        CharDir[(int)DIRECT.DIR_RIGHT] = gameObject.transform.Find("Right").gameObject;
    }
    protected virtual void Start()
    {
        curDir = DIRECT.DIR_FRONT;
        curState = STATE.STATE_NORMAL;

        isRun = false;

        isAttack = false;
        attackOrder = false;

        dodgeTime = 0f;

        weapon = null;

        direction = Vector2.zero;

        Speed = DefaultSpeed;

        AttackCoroutine = null;

        Controlable = true;

        MovingFunc = null;

        InteractObject = null;

        AttackedTime = new Hashtable();
        TrapHitTime = 0f;

        onStair = false;

        HpBar.GetComponent<StateBar>().parent = gameObject;
    }

    protected virtual void OnEnable()
    {
        for(int i=0; i<4; i++)
        {
            if (i == (int)curDir)
                CharDir[i].SetActive(true);
            else
                CharDir[i].SetActive(false);
        }
        StartCoroutine("makeDust");
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();
        MovingFunc = null;
        HpBar.SetActive(false);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (MovingFunc == null)
            doWalk();

        CooldownCheck();
    }

    private void CooldownCheck()
    {
        if (TrapHitTime > 0f)
            TrapHitTime -= Time.deltaTime * GameManager.Instance.TimeScale;

        if(dodgeTime > 0f)
            dodgeTime -= Time.deltaTime * GameManager.Instance.TimeScale;
    }

    protected float GetHalfSize()
    {
        return transform.lossyScale.x * 0.5f;
    }

    /*protected void Run()
    {
        animator.SetInteger("State", (int)dir.magnitude);

        transform.position += new Vector3(dir.x, 0f, dir.y) * Speed * Time.deltaTime;
    }*/

    public void MoveControl(Vector2 dir)
    {
        if (!Controlable)
            return;

        if(dir!=direction)
        {
            if (dir != Vector2.zero && !isAttack)
            {
                if (direction == Vector2.zero)
                {
                    if (dir.x != 0f)
                    {
                        if (dir.x > 0f)
                            Turn(DIRECT.DIR_RIGHT);
                        else
                            Turn(DIRECT.DIR_LEFT);
                    }
                    else
                    {
                        if (dir.y > 0f)
                            Turn(DIRECT.DIR_BACK);
                        else
                            Turn(DIRECT.DIR_FRONT);
                    }
                }

                else
                {
                    bool check = false;
                    switch (curDir)
                    {
                        case DIRECT.DIR_FRONT:
                            if (dir.y < 0f)
                                check = true;
                            break;
                        case DIRECT.DIR_BACK:
                            if (dir.y > 0f)
                                check = true;
                            break;
                        case DIRECT.DIR_LEFT:
                            if (dir.x < 0f)
                                check = true;
                            break;
                        case DIRECT.DIR_RIGHT:
                            if (dir.x > 0f)
                                check = true;
                            break;
                    }

                    if (!check)
                    {
                        if (dir.x != 0f)
                        {
                            if (dir.x > 0f)
                                Turn(DIRECT.DIR_RIGHT);
                            else
                                Turn(DIRECT.DIR_LEFT);
                        }
                        else
                        {
                            if (dir.y > 0f)
                                Turn(DIRECT.DIR_BACK);
                            else
                                Turn(DIRECT.DIR_FRONT);
                        }
                    }
                }
            }

            direction = dir.normalized;
        }
    }

    /*protected void Control() // 걷기, 달리기
    {
        if (Controlable)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (dir.x == 0f || dir.y != 0f)
                    Turn(DIRECT.DIR_BACK);
                dir.y = 1f;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (dir.x == 0f || dir.y != 0f)
                    Turn(DIRECT.DIR_FRONT);
                dir.y = -1f;
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                if (dir.y > 0f)
                {
                    dir.y = 0f;

                    if (dir.x != 0f)
                    {
                        if (dir.x > 0f)
                            Turn(DIRECT.DIR_RIGHT);
                        else
                            Turn(DIRECT.DIR_LEFT);
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                if (dir.y < 0f)
                {
                    dir.y = 0f;

                    if (dir.x != 0f)
                    {
                        if (dir.x > 0f)
                            Turn(DIRECT.DIR_RIGHT);
                        else
                            Turn(DIRECT.DIR_LEFT);
                    }
                }
            }



            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (dir.y == 0f || dir.x != 0f)
                    Turn(DIRECT.DIR_LEFT);
                dir.x = -1f;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (dir.y == 0f || dir.x != 0f)
                    Turn(DIRECT.DIR_RIGHT);
                dir.x = 1f;
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if (dir.x < 0f)
                {
                    dir.x = 0f;

                    if (dir.y != 0f)
                    {
                        if (dir.y > 0f)
                            Turn(DIRECT.DIR_BACK);
                        else
                            Turn(DIRECT.DIR_FRONT);
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                if (dir.x > 0f)
                {
                    dir.x = 0f;

                    if (dir.y != 0f)
                    {
                        if (dir.y > 0f)
                            Turn(DIRECT.DIR_BACK);
                        else
                            Turn(DIRECT.DIR_FRONT);
                    }
                }
            }

            *//*dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));*//*
            dir.Normalize();

            if(Input.GetKeyDown(KeyCode.Space))
            {
                KnockBack(new Vector2(1f, 0f));
            }


            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isRun = true;
                StartCoroutine("makeDust");
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isRun = false;
                StopCoroutine("makeDust");
            }
        }
    }*/
    private void OnDrawGizmos()
    {
        Vector3 forward = Vector3.zero;
        switch (curDir)
        {
            case DIRECT.DIR_FRONT:
                forward.z = -1f;
                break;
            case DIRECT.DIR_BACK:
                forward.z = 1f;
                break;
            case DIRECT.DIR_LEFT:
                forward.x = -1f;
                break;
            case DIRECT.DIR_RIGHT:
                forward.x = 1f;
                break;
        }
        RaycastHit hit;
        Gizmos.color = Color.red;
        LayerMask layer = LayerMask.GetMask("Liftable");
        if (Physics.BoxCast(transform.position, (transform.lossyScale - Vector3.one / 10) / 2, forward, out hit, Quaternion.identity, 0.5f, layer)) 
        {
            //Debug.Log(hit.transform.gameObject);
            Gizmos.DrawRay(transform.position, forward * hit.distance);
            Gizmos.DrawWireCube(transform.position + forward * hit.distance, transform.lossyScale);
            //Lift(hit.transform.gameObject);
        }
        else
        {
            Gizmos.DrawRay(transform.position, forward * 1f);
        }
    }

    public void KeyDownAttack()
    {
        attackOrder = true;
        if (!isAttack && InteractObject == null)
            StartCoroutine(DuringAttack());
    }

    public void KeyUpAttack()
    {
        attackOrder = false;
    }

    public void Interaction()
    {
        if(!Controlable)
            return;


        if (curState == STATE.STATE_NORMAL)
        {
            Vector3 forward = Vector3.zero;
            switch (curDir)
            {
                case DIRECT.DIR_FRONT:
                    forward.z = -1f;
                    break;
                case DIRECT.DIR_BACK:
                    forward.z = 1f;
                    break;
                case DIRECT.DIR_LEFT:
                    forward.x = -1f;
                    break;
                case DIRECT.DIR_RIGHT:
                    forward.x = 1f;
                    break;
            }
            RaycastHit[] hits;
            GameObject target = null;
            LayerMask layer = LayerMask.GetMask("Liftable") | LayerMask.GetMask("Pushable") | LayerMask.GetMask("Breakable") | LayerMask.GetMask("ETC object") | LayerMask.GetMask("Cliff");
            if ((hits = Physics.BoxCastAll(transform.position, (transform.lossyScale - Vector3.one / 10) / 2, forward, Quaternion.identity, 0.5f, layer)).Length > 0)
            {
                foreach(var hit in hits)
                {
                    if (target == null)
                        target = hit.transform.gameObject;
                    else if (Vector3.Distance(transform.position, target.transform.position) > Vector3.Distance(transform.position, hit.transform.position))
                        target = hit.transform.gameObject;
                }



                if (target.layer == LayerMask.NameToLayer("Liftable"))
                    Lift(target);
                else if (target.layer == LayerMask.NameToLayer("Pushable"))
                    Push(target);
                else if (target.layer == LayerMask.NameToLayer("Breakable"))
                    BreakRock(target);
                else if (target.layer == LayerMask.NameToLayer("Cliff"))
                    Jump(target);
                else
                    target.GetComponent<Objects>().Interaction();
            }
        }

        else if (curState == STATE.STATE_LIFT)
            Throw();
    }

    public void InteractionCancel()
    {
        switch(curState)
        {
            case STATE.STATE_PUSH:
                InteractObject = null;
                break;
        }
    }

    void Lift(GameObject obj)
    {
        //Debug.Log(obj);
        if (obj.GetComponent<LiftableObject>().Lifting(this))
            InteractObject = obj.GetComponent<Objects>();

        CoroutineStop();

        MovingFunc = StartCoroutine(DuringLift());
    }

    void Push(GameObject obj)
    {
        InteractObject = obj.GetComponent<Objects>();

        CoroutineStop();

        MovingFunc = StartCoroutine(DuringPush());
    }

    void Throw()
    {
        if(InteractObject != null)
        {
            InteractObject.GetComponent<LiftableObject>().Throw(curDir, 1f + 0.5f * direction.magnitude);
            InteractObject = null;
        }
    }

    void Jump(GameObject obj)
    {
        Vector3 dir = Vector3.zero;
        switch(curDir)
        {
            case DIRECT.DIR_BACK:
                dir.z += (obj.transform.position - transform.position).z + 0.5f;
                break;
            case DIRECT.DIR_FRONT:
                dir.z += (obj.transform.position - transform.position).z - 0.5f;
                break;
            case DIRECT.DIR_LEFT:
                dir.x += (obj.transform.position - transform.position).x - 0.5f;
                break;
            case DIRECT.DIR_RIGHT:
                dir.x += (obj.transform.position - transform.position).x + 0.5f;
                break;
        }

        RaycastHit hit;
        Vector3 Extents = Standard.halfExtentsScale * transform.lossyScale;
        Extents.y = 0f;

        if (Physics.BoxCast(transform.position + dir, Extents, Vector3.down, out hit))
        {
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Ground") || hit.distance <= 0.5f)
                return;
        }
        else
            return;


        CoroutineStop();

        MovingFunc = StartCoroutine(DuringJump(dir));
    }

    void BreakRock(GameObject obj)
    {
        Debug.Log(obj);
        CoroutineStop();

        MovingFunc = StartCoroutine(BlockBreak(obj));
    }

    public void doRun()
    {
        isRun = true;

        if (isAttack && dodgeTime <= 0f)
        {
            CoroutineStop();
            MovingFunc = StartCoroutine(Dodge());
        }
    }

    public void doWalk()
    {
        isRun = false;

        if (MovingFunc == null)
            MovingFunc = StartCoroutine(GeneralMoveCoroutine());
    }

    protected void CoroutineStop()
    {
        isRun = false;
        direction = Vector2.zero;
        if (MovingFunc != null)
        {
            StopCoroutine(MovingFunc);
            MovingFunc = null;
        }
    }

    protected void Stop()
    {
        isRun = false;
        direction = Vector2.zero;
    }

    protected void Turn(DIRECT direct)
    {
        if (curDir != direct)
        {
            CharDir[(int)curDir].SetActive(false);
            CharDir[(int)direct].SetActive(true);
            curDir = direct;
        }
    }



    protected void Skill()
    {
        if (weapon != null)
            ;
    }

    public virtual float Damage(float damage, AttackType type)
    {
        if (type == AttackType.Type_Melee)
            damage -= Defensive;
        else if (type == AttackType.Type_Magic)
            damage -= MagicResist;

        curHp -= damage;

        HpBar.GetComponent<StateBar>().HpUpdate(curHp / MaxHp);

        if (curHp <= 0f)
            Dead();

        return damage;
    }


    public void HitbyTrap()
    {
        if (TrapHitTime <= 0f)
        {
            KnockBack();
            Damage(MaxHp / 10f, AttackType.Type_True);
            TrapHitTime = TrapCooldown;
        }
    }


    public void KnockBack(Vector2 actPos)
    {
        if(Mathf.Abs(actPos.x) >= Mathf.Abs(actPos.y))
        {
            if (actPos.x > 0)
                Turn(DIRECT.DIR_LEFT);
            else
                Turn(DIRECT.DIR_RIGHT);
        }
        else
        {
            if (actPos.y > 0)
                Turn(DIRECT.DIR_FRONT);
            else
                Turn(DIRECT.DIR_BACK);
        }
        /*if (MovingFunc != null)
            StopCoroutine(MovingFunc);
        MovingFunc = */StartCoroutine(KnockBackCoroutine(actPos.normalized));
    }

    public void KnockBack()
    {
        Vector3 actPos = Vector3.zero;

        switch(curDir)
        {
            case DIRECT.DIR_FRONT:
                actPos.y += 1f;
                break;
            case DIRECT.DIR_BACK:
                actPos.y -= 1f;
                break;
            case DIRECT.DIR_LEFT:
                actPos.x += 1f;
                break;
            case DIRECT.DIR_RIGHT:
                actPos.x -= 1f;
                break;
        }
        /*if (MovingFunc != null)
            StopCoroutine(MovingFunc);
        MovingFunc = */StartCoroutine(KnockBackCoroutine(actPos));
    }

    private void Landing()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
            transform.position += new Vector3(0f, GetHalfSize() - hit.distance, 0f);

        onStair = false;
    }

    private void CollisionObject()
    {
        RaycastHit hit;

        if (Physics.BoxCast(transform.position, transform.lossyScale * Standard.halfExtentsScale, new Vector3(direction.x, 0f, direction.y), out hit, Quaternion.identity, Standard.CollisionRange + 0.1f))
            if (hit.transform.tag == "Object")
                hit.transform.GetComponent<Objects>().Collision();
    }

    /*private bool CollisionCheckX()
    {
        bool check = true;
        if (direction.x != 0f)
        {
            check = Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), new Vector3(direction.x, 0f, 0f), transform.lossyScale.x * 0.5f + Standard.CollisionRange);

        }

        return check;
    }

    private bool CollisionCheckY()
    {
        bool check = true;

        if (direction.y != 0f)
        {
            check = Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), new Vector3(0f, 0f, direction.y), transform.lossyScale.y * 0.5f + Standard.CollisionRange);
        }

        return check;
    }*/

    /*private bool CollisionCheck()
    {
        if (direction == Vector2.zero)
            return false;

        bool check = true;

        if (direction.x != 0f)
        {
            check = Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), new Vector3(direction.x, 0f, 0f), 0.51f);
        }

        if (direction.y != 0f)
        {
            check = check && Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), new Vector3(0f, 0f, direction.y), 0.6f);
        }

        return check;
    }*/
    private bool StraightMove(Vector2 pos)
    {
        RaycastHit hit;
        Vector3 Extents = transform.lossyScale * Standard.halfExtentsScale;

        if (pos.x != 0f)
            Extents.x = 0f;
        else
            Extents.z = 0f;

        Physics.BoxCast(transform.position, Extents, new Vector3(pos.x, 0f, pos.y), out hit, Quaternion.identity, Standard.CollisionRange + GetHalfSize());
        
        if (hit.transform == null || hit.transform.gameObject.layer == LayerMask.NameToLayer("Stairs"))
        {
            transform.position += new Vector3(pos.x, 0f, pos.y) * Time.deltaTime * GameManager.Instance.TimeScale;
            return true;
        }
        else
        {
            if (hit.transform.gameObject.tag == "Object")
            {
                hit.transform.GetComponent<Objects>().Contact(this);
                return true;
            }

            Vector3 left, right;

            switch (curDir)
            {
                case DIRECT.DIR_FRONT:
                    left = Vector3.right;
                    right = Vector3.left;
                    break;
                case DIRECT.DIR_BACK:
                    left = Vector3.left;
                    right = Vector3.right;
                    break;
                case DIRECT.DIR_LEFT:
                    left = Vector3.back;
                    right = Vector3.forward;
                    break;
                case DIRECT.DIR_RIGHT:
                    left = Vector3.forward;
                    right = Vector3.back;
                    break;
                default:
                    left = Vector3.zero;
                    right = Vector3.zero;
                    break;
            }
            left *= Standard.halfExtentsScale * 2f;
            right *= Standard.halfExtentsScale * 2f;

            bool L = Physics.Raycast(transform.position + left * GetHalfSize(), new Vector3(pos.x, 0f, pos.y), GetHalfSize() + Standard.CollisionRange/*, LayerMask.GetMask("Obstacle")*/);
            bool R = Physics.Raycast(transform.position + right * GetHalfSize(), new Vector3(pos.x, 0f, pos.y), GetHalfSize() + Standard.CollisionRange/*, LayerMask.GetMask("Obstacle")*/);

            //Debug.Log(L + " " + R);
            if (L && !R)
                return DiagonalMove((pos.normalized + new Vector2(right.x, right.z)).normalized * pos.magnitude);
            else if (!L && R)
                return DiagonalMove((pos.normalized + new Vector2(left.x, left.z)).normalized * pos.magnitude);

        }
        return false;
    }

    private bool DiagonalMove(Vector2 pos)
    {
        RaycastHit hit;

        Vector3 ExtentX = transform.lossyScale * Standard.halfExtentsScale;
        Vector3 ExtentY = ExtentX;

        ExtentX.x = 0f;
        ExtentY.z = 0f;

        if (Physics.BoxCast(transform.position, ExtentX, new Vector3(pos.x, 0f, 0f), out hit, Quaternion.identity, Standard.CollisionRange + GetHalfSize()))
        {
            if (hit.transform.gameObject.tag == "Object")
            {
                hit.transform.GetComponent<Objects>().Contact(this);
            }

            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Stairs"))
                pos.x = 0f;
        }

        if (Physics.BoxCast(transform.position, ExtentY, new Vector3(0f, 0f, pos.y), out hit, Quaternion.identity, Standard.CollisionRange + GetHalfSize()))
        {
            if (hit.transform.gameObject.tag == "Object")
            {
                hit.transform.GetComponent<Objects>().Contact(this);
            }
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Stairs"))
                pos.y = 0f;
        }


        transform.position += new Vector3(pos.x, 0f, pos.y) * Time.deltaTime * GameManager.Instance.TimeScale;

        if (pos == Vector2.zero)
            return false;
        else
            return true;
    }

    protected bool Move(Vector2 pos)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, transform.lossyScale.y * 2f, LayerMask.GetMask("Stairs")))
        {
            onStair = true;
            transform.position += new Vector3(0f, GetHalfSize() - hit.distance, 0f);
        }
        else if (onStair)
            Landing();

        if (onStair)
            pos *= StairSlow;

        if (pos.x != 0f && pos.y != 0f)
            return DiagonalMove(pos);
        else
            return StraightMove(pos);


        /*bool x = CollisionCheckX();
        bool y = CollisionCheckY();

        if (x && y)
            return false;

        transform.position += new Vector3(x ? 0f : pos.x, 0f, y ? 0f : pos.y) * Speed * Time.deltaTime;
        return true;*/
    }


    IEnumerator GeneralMoveCoroutine()
    {
        curState = STATE.STATE_NORMAL;

        while (true)
        {
            if (direction != Vector2.zero)
            {
                if (isRun)
                    CollisionObject();
                float scale = Speed;
                if (isRun)
                    scale *= RunScale;

                if (!Move(direction * scale) && isRun)
                {
                    KnockBack();
                }
            }
            animator.SetInteger("State", (int)direction.magnitude);
            yield return null;
        }
    }

    IEnumerator DuringAttack()
    {
        if (AttackCoroutine == null)
            yield break;

        if (MovingFunc != null)
            CoroutineStop();
        isAttack = true;

        yield return MovingFunc = StartCoroutine(AttackCoroutine());

        if (isAttack)
        {
            CoroutineStop();
            isAttack = false;
        }
    }

    IEnumerator Dodge()
    {
        float timer = 0.2f;

        dodgeTime = DodgeCooldown;
        while (timer > 0f)
        {
            if (direction != Vector2.zero)
                Move(direction * DefaultSpeed * 2f);
            timer -= Time.deltaTime * GameManager.Instance.TimeScale;
            yield return null;
        }

        CoroutineStop();
        isAttack = false;
    }

    IEnumerator DuringLift()
    {
        curState = STATE.STATE_LIFT;
        while(InteractObject != null)
        {
            if (!InteractObject.gameObject.activeSelf)
            {
                InteractObject = null;
                break;
            }
            Move(direction * DefaultSpeed);
            animator.SetInteger("State", (int)direction.magnitude);
            yield return null;
        }

        CoroutineStop();
    }

    IEnumerator DuringPush()
    {
        curState = STATE.STATE_PUSH;
        Controlable = false;
        float timer = 0f;

        Vector3 vector;

        switch (curDir)
        {
            case DIRECT.DIR_FRONT:
                vector = new Vector3(transform.position.x - InteractObject.transform.position.x, 0f, InteractObject.transform.lossyScale.z / 2f + transform.lossyScale.z / 2f);
                break;
            case DIRECT.DIR_BACK:
                vector = new Vector3(transform.position.x - InteractObject.transform.position.x, 0f, -(transform.lossyScale.z / 2f + InteractObject.transform.lossyScale.z / 2f));
                break;
            case DIRECT.DIR_LEFT:
                vector = new Vector3(InteractObject.transform.lossyScale.x / 2f + transform.lossyScale.x / 2f, 0f, transform.position.z - InteractObject.transform.position.z);
                break;
            case DIRECT.DIR_RIGHT:
                vector = new Vector3(-(transform.lossyScale.x / 2f + InteractObject.transform.lossyScale.x / 2f), 0f, transform.position.z - InteractObject.transform.position.z);
                break;
            default:
                vector = Vector3.zero;
                break;
        }

        while (InteractObject != null)
        {
            timer += Time.deltaTime * GameManager.Instance.TimeScale;

            transform.position = InteractObject.transform.position + vector;
            if(timer >= 1f)
            {
                InteractObject.GetComponent<PushableObject>().Push(curDir);
                timer = 0f;
            }
            yield return null;
        }

        Controlable = true;
        CoroutineStop();
    }

    IEnumerator DuringJump(Vector3 dir)
    {
        Controlable = false;
        float DropSpeed = 4f;
        float time = 0f;

        RaycastHit hit;

        animator.SetBool("Jump", true);

        while (time < 1f)
        {
            time += Time.deltaTime;
            DropSpeed -= Standard.gravityScale * Time.deltaTime;
            dir.y = DropSpeed;

            transform.position += dir * Time.deltaTime;

            yield return null;
        }

        while (!Physics.Raycast(transform.position, Vector3.down, out hit, Standard.CollisionRange + GetHalfSize(), LayerMask.GetMask("Ground") | LayerMask.GetMask("Stairs")))
        {
            DropSpeed -= Standard.gravityScale * Time.deltaTime;

            transform.position += new Vector3(0f, DropSpeed * Time.deltaTime, 0f);

            yield return null;
        }

        transform.position += new Vector3(0f, transform.lossyScale.y / 2f - hit.distance, 0f);

        animator.SetBool("Jump", false);
        Controlable = true;
        CoroutineStop();
    }

    IEnumerator BlockBreak(GameObject target)
    {
        Controlable = false;
        Vector3 dir = target.transform.position - transform.position;
        float timer = 0.1f;

        transform.position += -dir * Time.deltaTime * 10f;
        timer += Time.deltaTime;


        target.GetComponent<BreakableObject>().Destroy();

        while(timer>0)
        {
            yield return null;
            timer -= Time.deltaTime;

            transform.position += dir * Time.deltaTime * 10f;
        }

        transform.position = target.transform.position;

        Controlable = true;
        CoroutineStop();
    }

    IEnumerator KnockBackCoroutine(Vector2 actPos)
    {
        Stop();
        Controlable = false;
        animator.SetTrigger("Paralysis");
        float curTime = KnockBackTime;

        while (curTime > 0f)
        {
            curTime -= Time.deltaTime * GameManager.Instance.TimeScale;

            transform.position += new Vector3(actPos.x, 0f, actPos.y) * KnockBackSpeed * Time.deltaTime * GameManager.Instance.TimeScale;
            yield return null;
        }
        animator.SetTrigger("Return");
        Controlable = true;
        //MovingFunc = null;
    }

    IEnumerator makeDust()
    {
        while (true)
        {
            if (curState == STATE.STATE_NORMAL && isRun && !isAttack && direction != Vector2.zero)
                ObjectPoolManger.Instance.GenerateDust(CharDir[(int)curDir].transform.Find("Shadow").position);
            yield return new WaitForSeconds(DustGenTime);
        }
    }    


    void Dead()
    {
        gameObject.SetActive(false);
    }
}
