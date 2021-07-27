using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Switch
{
    protected Fire fire;

    SwitchHub switchHub;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        fire = transform.Find("Fire").GetComponent<Fire>();
        switchHub = GetComponent<SwitchHub>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        SwitchCheck();
        onOff = fire.OnOff;
    }


    void SwitchCheck()
    {
        if (switchHub.Check())
            fire.Ignite();
    }
}
