using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECT
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

enum MOTION
{
    Idle,
    Run
}
public abstract class Character : SpriteObj
{
    protected const float defaultSpeed = 2f;
    protected const float slowSpeed = 1f;

    protected Vector3 curDir;

    [SerializeField] SkeletonDataAsset normal_front;
    [SerializeField] SkeletonDataAsset normal_back;
    [SerializeField] SkeletonDataAsset normal_side;


    SkeletonDataAsset curSkel;

    SkeletonMecanim skeletonMecanim;

    private float Speed;

    protected float moveDistance;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        skeletonMecanim = transform.GetChild(0).GetComponent<SkeletonMecanim>();
        curSkel = null;
        curDir = Vector3.zero;
        Speed = defaultSpeed;
        moveDistance = 1f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
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
            ChangeSkel(normal_back);
        else if (dir == Vector3.down)
            ChangeSkel(normal_front);
        else
            ChangeSkel(normal_side);

        curDir = dir;
    }


    protected void TurnBack()
    {
        Turn(-curDir);
        moveDistance = 1f - moveDistance;
    }

    protected void ChangeSkel(SkeletonDataAsset skel)
    {
        if (curSkel == skel)
            return;

        curSkel = skel;

        skeletonMecanim.skeletonDataAsset = curSkel;
        skeletonMecanim.Initialize(true);
    }

    void ChangeAnime(MOTION motion)
    {

    }

    private void Move()
    {
        

        float deltaTime = Time.deltaTime * Speed * GameManager.Instance.timeScale;
        Vector3 moveVec = curDir;

        while (deltaTime > 0f)
        {
            if (moveDistance == 1f)
            {
                transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
                DicisionDir();
                if (GameManager.Instance.mapCheck(transform.position + curDir))
                {
                    break;
                }
            }

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

        }

    }
    protected abstract void DicisionDir();
}
