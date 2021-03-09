using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : Switch
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }


    private void OnTriggerStay(Collider collision)
    {
        onOff = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        onOff = false;
    }

}
