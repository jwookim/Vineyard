using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lana : Character
{
    Vector3 nextDir;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        Turn(Vector3.right);
    }

    // Update is called once per frame
    protected override void Update()
    {
        Controll();
        base.Update();
    }

    private void Controll()
    {
        Vector3 dir;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            dir = Vector3.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            dir = Vector3.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            dir = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            dir = Vector3.right;
        else
            return;

        if (dir == curDir * -1f)
            TurnBack();
        else
            nextDir = dir;
    }


    protected override void Turn(Vector3 dir)
    {
        base.Turn(dir);
        nextDir = curDir;
    }
    protected override void DicisionDir()
    {
        if (!GameManager.Instance.mapCheck(transform.position + nextDir))
            Turn(nextDir);
        /*else if (GameManager.Instance.mapCheck(transform.position + curDir))
            Turn(Vector3.zero);*/

    }


    public Vector3 getFrontCoord(int distance = 0)
    {
        return transform.position + curDir * distance;
    }
}
