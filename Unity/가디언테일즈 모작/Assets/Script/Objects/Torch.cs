using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Switch
{
    protected Fire fire;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        fire = transform.GetChild(0).GetComponent<Fire>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        onOff = fire.OnOff;
    }
}
