using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader_soldier : Enemy
{
    private bool onlyChase;

    protected override void ChangeMode(MODE mode)
    {
        if (onlyChase && mode == MODE.SCATTER)
            mode = MODE.CHASE;

        base.ChangeMode(mode);
    }

    protected override Vector3 TargetCoord()
    {
        return GameManager.Instance.GetPlayerFrontCoord();
    }

    public override void SetScatterPoint(int xMax, int yMax)
    {
        ScatterPoint = new Vector3(GameManager.Instance.Max_x - 2f, GameManager.Instance.Max_y - 2f, 0f);
    }


    public void EmergencyCall()
    {
        onlyChase = true;

        ChangeOrder(MODE.CHASE);
    }

    public void Discharge()
    {
        onlyChase = false;
    }
}
