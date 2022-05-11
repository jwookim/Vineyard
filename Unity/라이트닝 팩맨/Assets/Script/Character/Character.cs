using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DIRECT
{
    FRONT,
    BACK,
    SIDE
}

enum MOTION
{
    Idle,
    Run
}
public abstract class Character : SpriteObj
{
    protected const float defaultSpeed = 3f;
    protected const float runSpeed = 3.5f;
    protected const float slowSpeed = 1.5f;

    protected Vector3 curDir;

    [SerializeField] protected SkeletonDataAsset normal_front;
    [SerializeField] protected SkeletonDataAsset normal_back;
    [SerializeField] protected SkeletonDataAsset normal_side;


    SkeletonDataAsset curSkel;

    SkeletonMecanim skeletonMecanim;

    protected float Speed;

    protected float moveDistance;

    protected Animator animator;
    // Start is called before the first frame update

    protected virtual void Awake()
    {
        skeletonMecanim = transform.GetChild(0).GetComponent<SkeletonMecanim>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        curSkel = normal_side;
        curDir = Vector3.zero;
        Speed = defaultSpeed;
        moveDistance = 1f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }


    public virtual void Initialization()
    {
        transform.localScale = Vector3.one;
        curDir = Vector3.right;
        moveDistance = 0f;
        Speed = defaultSpeed;
    }

    protected virtual void Turn(Vector3 dir)
    {
        if (curDir == dir)
            return;

        if (dir == Vector3.left)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (curDir == Vector3.left)
            transform.localScale = Vector3.one;

        if (dir == Vector3.up)
            ChangeSkel(DIRECT.BACK);
        else if (dir == Vector3.down)
            ChangeSkel(DIRECT.FRONT);
        else
            ChangeSkel(DIRECT.SIDE);

        curDir = dir;

        animator.SetInteger("Vertical", (int)curDir.y);
        animator.SetInteger("Horizontal", (int)curDir.x);
    }


    protected void TurnBack()
    {
        Turn(-curDir);
        moveDistance = 1f - moveDistance;
    }

    protected virtual void ChangeSkel(DIRECT direct)
    {
        switch(direct)
        {
            case DIRECT.FRONT:
                ChangeSkel(normal_front);
                break;
            case DIRECT.BACK:
                ChangeSkel(normal_back);
                break;
            case DIRECT.SIDE:
                ChangeSkel(normal_side);
                break;
        }
    }

    protected void ChangeSkel(SkeletonDataAsset skel)
    {
        if (curSkel == skel)
            return;

        /*if (curSkel == null)
            curSkel = skel;*/
        curSkel = skel;

        skeletonMecanim.skeletonDataAsset = curSkel;
        skeletonMecanim.Initialize(true);
    }


    private void Move()
    {

        float deltaTime = Time.deltaTime * Speed * GameManager.Instance.timeScale;
        Vector3 moveVec;

        while (deltaTime > 0f)
        {
            if (moveDistance == 1f)
            {
                transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
                DicisionDir();
                if (GameManager.Instance.mapCheck(transform.position + curDir) || curDir == Vector3.zero)
                {
                    animator.SetBool("Move", false);
                    break;
                }
            }

            moveVec = curDir;
            if (deltaTime >= moveDistance)
            {
                deltaTime -= moveDistance;
                moveVec *= moveDistance;
                moveDistance = 1f;

            }
            else
            {
                moveVec *= deltaTime;
                moveDistance -= deltaTime;
                deltaTime = 0f;
            }

            transform.position += moveVec;
            animator.SetBool("Move", true);

        }

    }

    private void ReversalPosition()
    {
        Vector3 point = Vector3.zero;
        if(transform.position.x <= 0f)
            point = transform.position + new Vector3(GameManager.Instance.Max_x - 1, 0f);
        else if(transform.position.x >= GameManager.Instance.Max_x - 1)
            point = transform.position - new Vector3(GameManager.Instance.Max_x - 1, 0f);

        if (transform.position.y <= 0f)
            point = transform.position + new Vector3(0f, GameManager.Instance.Max_y - 1);
        else if (transform.position.y >= GameManager.Instance.Max_y - 1)
            point = transform.position + new Vector3(0f, GameManager.Instance.Max_y - 1);

        if (point != Vector3.zero && !GameManager.Instance.mapCheck(point))
            transform.position = point;
    }
    protected virtual void DicisionDir()
    {
        ReversalPosition();
    }
}
