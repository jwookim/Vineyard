using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MODE
{
    CHASE,
    SCATTER,
    FRIGHTENED,
    COLLAPSE,
    EATEN
}
public abstract class Enemy : Character
{
    List<Vector3> aroundCheck;
    MODE curMode;

    private float modeDuration;
    // Start is called before the first frame update

    protected Vector3 ScatterPoint;


    [SerializeField] protected SkeletonDataAsset dead_front;
    [SerializeField] protected SkeletonDataAsset dead_back;
    [SerializeField] protected SkeletonDataAsset dead_side;

    [SerializeField] private AudioClip deadSound;
    [SerializeField] private AudioClip attackSound;
    protected override void Awake()
    {
        aroundCheck = new List<Vector3>();
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        Initialization();
    }

    protected override void Update()
    {
        base.Update();
        if (curMode == MODE.COLLAPSE)
            DurationCheck();
    }
    protected abstract Vector3 TargetCoord();

    private Vector3 calcDestination()
    {
        switch(curMode)
        {
            case MODE.CHASE:
                return TargetCoord();
            case MODE.SCATTER:
                return ScatterPoint;
            case MODE.EATEN:
                return GameManager.Instance.ShipPosition;
        }

        return default;
    }
    protected override void DicisionDir()
    {
        base.DicisionDir();

        if (!GameManager.Instance.mapCheck(transform.position + Vector3.up))
            aroundCheck.Add(Vector3.up);
        if (!GameManager.Instance.mapCheck(transform.position + Vector3.left))
            aroundCheck.Add(Vector3.left);
        if (!GameManager.Instance.mapCheck(transform.position + Vector3.down))
            aroundCheck.Add(Vector3.down);
        if (!GameManager.Instance.mapCheck(transform.position + Vector3.right))
            aroundCheck.Add(Vector3.right);


        Vector3 dest;
        switch(aroundCheck.Count)
        {
            case 0:
                Turn(Vector3.zero);
                break;
            case 1:
                Turn(aroundCheck[0]);
                break;
            default:
                for (int i = 0; i < aroundCheck.Count; i++)
                {
                    if (aroundCheck[i] == -curDir)
                    {
                        aroundCheck.RemoveAt(i);
                        break;
                    }
                }

                if(curMode == MODE.FRIGHTENED)
                {
                    Turn(aroundCheck[Random.Range(0, aroundCheck.Count)]);
                    break;
                }

                dest = calcDestination();
                //Debug.Log(dest);
                Vector3 next = aroundCheck[0];
                foreach (var dir in aroundCheck)
                {
                    //Debug.Log(Vector3.Distance(transform.position + dir, dest) + " " + Vector3.Distance(transform.position + next, dest));
                    if (Vector3.Distance(transform.position + dir, dest) < Vector3.Distance(transform.position + next, dest))
                        next = dir;
                }
                Turn(next);
                break;
        }
        /*foreach (var d in aroundCheck)
            Debug.Log(d);*/

        aroundCheck.Clear();
    }

    protected override void ChangeSkel(DIRECT direct)
    {
        if (curMode != MODE.EATEN)
            base.ChangeSkel(direct);
        else
        {
            switch (direct)
            {
                case DIRECT.FRONT:
                    ChangeSkel(dead_front);
                    break;
                case DIRECT.BACK:
                    ChangeSkel(dead_back);
                    break;
                case DIRECT.SIDE:
                    ChangeSkel(dead_side);
                    break;
            }
        }
    }

    protected void OriginChangeSkel(DIRECT direct)
    {
        base.ChangeSkel(direct);
    }

    private void ChangeMode(MODE mode)
    {
        if (mode == curMode)
            return;

        MODE tmp = curMode;
        curMode = mode;

        switch(mode)
        {
            case MODE.CHASE:
                Speed = defaultSpeed;
                break;
            case MODE.SCATTER:
                if (tmp == MODE.CHASE)
                    TurnBack();
                Speed = defaultSpeed;
                break;
            case MODE.COLLAPSE:
                Speed = 0f;
                modeDuration = 2.5f;
                break;
            case MODE.FRIGHTENED:
                TurnBack();
                Speed = slowSpeed;
                break;
            case MODE.EATEN:
                Speed = slowSpeed;

                if (curDir == Vector3.up)
                    ChangeSkel(DIRECT.BACK);
                else if (curDir == Vector3.down)
                    ChangeSkel(DIRECT.FRONT);
                else
                    ChangeSkel(DIRECT.SIDE);
                break;
        }


        animator.SetInteger("Mode", (int)mode);
    }

    public virtual void ChangeOrder(MODE mode)
    {
        if (curMode == MODE.CHASE || curMode == MODE.SCATTER)
            ChangeMode(mode);
    }

    public abstract void SetScatterPoint(int xMax, int yMax);

    void DurationCheck()
    {
        modeDuration -= Time.deltaTime * GameManager.Instance.timeScale;

        if (modeDuration <= 0f)
        {
            ChangeMode(MODE.EATEN);
            modeDuration = 0f;
        }
    }

    public void Suprise()
    {
        if (curMode == MODE.CHASE || curMode == MODE.SCATTER)
            ChangeMode(MODE.FRIGHTENED);
    }

    public void CalmDown()
    {
        if (curMode == MODE.FRIGHTENED)
            ChangeMode(GameManager.Instance.getMode());
    }

    public void Dead()
    {
        gameObject.GetComponent<AudioSource>().clip = deadSound;
        gameObject.GetComponent<AudioSource>().Play();
        GameManager.Instance.killVillain();
        ChangeMode(MODE.COLLAPSE);

        ChangeSkel(DIRECT.SIDE);
    }

    IEnumerator Attack()
    {
        GameManager.Instance.HitLana();
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<AudioSource>().clip = attackSound;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public override void Initialization()
    {
        base.Initialization();
        //Turn(Vector3.up);
        ChangeMode(GameManager.Instance.getMode());

        if (curDir == Vector3.up)
            ChangeSkel(DIRECT.BACK);
        else if (curDir == Vector3.down)
            ChangeSkel(DIRECT.FRONT);
        else
            ChangeSkel(DIRECT.SIDE);
    }

    public bool EatenCheck()
    {
        return curMode == MODE.EATEN ? true : false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (curMode == MODE.FRIGHTENED)
                Dead();

            if (curMode == MODE.CHASE || curMode == MODE.SCATTER)
                StartCoroutine(Attack());
        }
    }
}
