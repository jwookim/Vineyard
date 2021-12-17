using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MODE
{
    CHASE,
    SCATTER,
    FRIGHTENED,
    EATEN
}
public abstract class Enemy : Character
{
    List<Vector3> aroundCheck;
    MODE curMode;

    
    // Start is called before the first frame update

    protected Vector3 ScatterPoint;

    private void Awake()
    {
        aroundCheck = new List<Vector3>();
    }

    protected override void Start()
    {
        base.Start();
        Turn(Vector3.up);
        curMode = MODE.CHASE;
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


    private void ChangeMode(MODE mode)
    {
        if(mode != curMode)
            curMode = mode;
    }

    public abstract void SetScatterPoint(int xMax, int yMax);

    public void Dead()
    {
        ChangeMode(MODE.EATEN);
    }
}
