using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Speed = 10f;

        hitlimit = 1;
        timeLimit = 0.5f;
        PenetrateTarget = false;
        PassWall = false;

        hitType = AttackType.Type_Melee;
    }

}
