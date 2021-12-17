using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected int AttackCount;

    protected float Speed;
    protected Vector2 dir;

    protected Dictionary<GameObject, int> targetTable;

    protected int hitlimit;
    protected bool PenetrateTarget;
    protected bool PassWall;

    protected virtual void Awake()
    {
        targetTable = new Dictionary<GameObject, int>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnDisable()
    {
        
    }


    private void OnCollisionStay(Collision collision)
    {
        CollisionCheck(collision.gameObject);
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
}
