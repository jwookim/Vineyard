using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Type_Melee,
    Type_Magic,
    Type_True
}

public abstract class Projectile : MonoBehaviour
{
    protected int AttackCount;

    protected float Speed;
    protected float Timer;
    protected float Power;
    protected Vector2 dir;

    protected Dictionary<GameObject, int> targetTable;

    protected int hitlimit;
    protected float timeLimit;
    protected bool PenetrateTarget;
    protected bool PassWall;

    protected AttackType hitType;

    GameObject parent;

    protected virtual void Awake()
    {
        targetTable = new Dictionary<GameObject, int>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        TimeFlow();
    }


    private void Move()
    {
        if (dir != Vector2.zero)
            gameObject.transform.position += new Vector3(dir.x, 0f, dir.y) * Time.deltaTime * Speed * GameManager.Instance.TimeScale;
    }

    void TimeFlow()
    {
        Timer += Time.deltaTime * GameManager.Instance.TimeScale;

        if (Timer >= timeLimit)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        AttackCount = 0;
        Timer = 0f;
    }

    protected virtual void OnDisable()
    {
        targetTable.Clear();
        parent = null;
        Power = 0f;
    }

    private void OnTriggerStay(Collider other)
    {
        CollisionCheck(other.gameObject);
    }

    protected void HitTarget(GameObject target)
    {
        GameManager.Instance.Attackto(parent, target, hitType, Power);
    }


    private bool hitCheck(GameObject obj)
    {
        if (!targetTable.ContainsKey(obj))
            targetTable.Add(obj, 0);
        else if (targetTable[obj] >= hitlimit)
            return false;
        targetTable[obj]++;

        return true;
    }

    private void CollisionCheck(GameObject target)
    {

        if(target.tag=="Character" && target.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (hitCheck(target))
                HitTarget(target);

            if(!PenetrateTarget)
            {
                gameObject.SetActive(false);
            }
        }

        else if(target.tag=="Obstacle" || target.tag =="Object")
        {
            if(!PassWall)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void inputInformation(Vector3 vector, GameObject Parent, float damage)
    {
        dir = vector;

        parent = Parent;
        Power = damage;

        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.rotation *= Quaternion.FromToRotation(Vector3.up, vector);
    }
}
