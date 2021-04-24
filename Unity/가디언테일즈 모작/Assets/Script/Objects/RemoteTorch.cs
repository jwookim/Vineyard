using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteTorch : Torch
{
    SwitchHub switchHub;
    protected override void Start()
    {
        base.Start();
        switchHub = GetComponent<SwitchHub>();
    }

    protected override void Update()
    {
        SwitchCheck();
        base.Update();
    }

    void SwitchCheck()
    {
        if (switchHub.Check())
            fire.Ignite();
    }
}
