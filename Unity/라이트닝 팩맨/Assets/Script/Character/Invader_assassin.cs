using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader_assassin : Enemy
{
    const int targetDist = 2;
    protected override Vector3 TargetCoord()
    {
        Vector3 symmetricPoint = GameManager.Instance.GetPlayerFrontCoord(targetDist);
        Vector3 soldierPosition = GameManager.Instance.GetInvaderCoord(EnemyName.Invader_soldier);
        return symmetricPoint * 2f - soldierPosition;
    }

    public override void SetScatterPoint(int xMax, int yMax)
    {
        ScatterPoint = new Vector3(1f, GameManager.Instance.Max_y - 2f, 0f);
    }
}
