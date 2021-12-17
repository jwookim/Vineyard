using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader_magician : Enemy
{
    protected override Vector3 TargetCoord()
    {
        Vector3 target = GameManager.Instance.GetPlayerFrontCoord();

        if (Vector3.Distance(transform.position, target) >= 8)
            return target;
        else
            return ScatterPoint;
    }

    public override void SetScatterPoint(int xMax, int yMax)
    {
        ScatterPoint = new Vector3(1f, 1f, 0f);
    }
}
