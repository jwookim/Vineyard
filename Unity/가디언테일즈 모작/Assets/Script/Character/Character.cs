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

    const float DefaultSpeed = 0.5f;  // 기본 이동속도
    const float RunScale = 1.5f;  // 달리기 배율

    const float CollisionRange = 0.55f;

    const float KnockBackTime = 0.5f;
    const float KnockBackSpeed = 1f;

    [SerializeField] private Animator animator;
    private GameObject[] CharDir = new GameObject[4];  // 방향별 모델링

    private DIRECT curDir; // 캐릭터가 보는 방향

    private Vector2 direction;  // 움직이는 방향

    private bool isRun;  // 달리기 여부

    protected Weapon weapon;

    protected Action Attack;  // 공격 delegate

    private STATE curState;

    private bool Controlable;  // 컨트롤 가능 여부
    private Coroutine MovingFunc;

    private Objects InteractObject;

    protected float Speed;
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

        weapon = null;

        direction = Vector2.zero;

        Speed = 10f;

        Attack = null;

        Controlable = true;

        MovingFunc = null;

        InteractObject = null;
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
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (MovingFunc == null)
            doWalk();
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
            if (dir != Vector2.zero)
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
            RaycastHit hit;
            LayerMask layer = LayerMask.GetMask("Liftable");
            if (Physics.BoxCast(transform.position, (transform.lossyScale - Vector3.one / 10) / 2, forward, out hit, Quaternion.identity, 0.5f, layer))
                Lift(hit.transform.gameObject);
        }

        else if (curState == STATE.STATE_LIFT)
            Throw();
    }

    void Lift(GameObject obj)
    {
        Debug.Log(obj);
        if (obj.GetComponent<LiftableObject>().Lifting(this))
            InteractObject = obj.GetComponent<Objects>();

        CoroutineStop();

        MovingFunc = StartCoroutine(DuringLift());
    }

    void Throw()
    {
        if(InteractObject != null)
        {
            InteractObject.GetComponent<LiftableObject>().Throw(curDir, 1f + 0.5f * direction.magnitude);
            InteractObject = null;
        }
    }

    public void doRun()
    {
        isRun = true;

        if (MovingFunc == null)
            MovingFunc = StartCoroutine(GeneralMoveCoroutine());
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


    protected void SwordAttack()
    {

    }

    protected void Skill()
    {
        if (weapon != null)
            ;
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

    private void CollisionObject()
    {
        Ray ray = new Ray(transform.position, new Vector3(direction.x, 0f, direction.y));
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.48f, out hit, CollisionRange))
            if (hit.transform.tag == "Object")
                hit.transform.GetComponent<Objects>().Collision();
    }

    private bool CollisionCheckX()
    {
        bool check = true;
        if (direction.x != 0f)
        {
            check = Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), new Vector3(direction.x, 0f, 0f), CollisionRange);

        }

        return check;
    }

    private bool CollisionCheckY()
    {
        bool check = true;

        if (direction.y != 0f)
        {
            check = Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), new Vector3(0f, 0f, direction.y), CollisionRange);
        }

        return check;
    }

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

    protected bool Move(Vector2 pos)
    {
        bool x = CollisionCheckX();
        bool y = CollisionCheckY();

        if (x && y)
            return false;

        transform.position += new Vector3(x ? 0f : pos.x, 0f, y ? 0f : pos.y) * Speed * Time.deltaTime;
        return true;
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
                float scale = DefaultSpeed;
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
            yield return null;
        }

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
            curTime -= Time.deltaTime;

            transform.position += new Vector3(actPos.x, 0f, actPos.y) * KnockBackSpeed * Time.deltaTime;
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
            if (curState == STATE.STATE_NORMAL && isRun && direction != Vector2.zero)
                ObjectPoolManger.Instance.GenerateDust(CharDir[(int)curDir].transform.Find("Shadow").position);
            yield return new WaitForSeconds(DustGenTime);
        }
    }    
}
