using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader_knight : Enemy
{
    const int chasingDist = 4;
    protected override void Start()
    {
        base.Start();
    }


    protected override void ChangeSkel(DIRECT direct)
    {
        OriginChangeSkel(direct);
    }
    protected override Vector3 TargetCoord()
    {
        return GameManager.Instance.GetPlayerFrontCoord(chasingDist);
    }

    public override void SetScatterPoint(int xMax, int yMax)
    {
        ScatterPoint = new Vector3(1f, GameManager.Instance.Max_y - 2f, 0f);
    }
}
