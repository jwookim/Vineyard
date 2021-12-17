using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader_soldier : Enemy
{
    protected override Vector3 TargetCoord()
    {
        return GameManager.Instance.GetPlayerFrontCoord();
    }

    public override void SetScatterPoint(int xMax, int yMax)
    {
        ScatterPoint = new Vector3(GameManager.Instance.Max_x - 2f, GameManager.Instance.Max_y - 2f, 0f);
    }
}
