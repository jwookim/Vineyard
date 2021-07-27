using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BreakableObject : Objects
{

    protected override void Awake()
    {
        base.Awake();
        gameObject.layer = LayerMask.NameToLayer("Breakable");
    }
}
