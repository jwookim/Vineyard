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
        ScatterPoint = new Vector3(GameManager.Instance.Max_x - 2f, 1f, 0f);
    }
}
