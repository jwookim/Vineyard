using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Switch
{
    Fire fire;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        fire = GetComponent<Fire>();
    }

    // Update is called once per frame
    void Update()
    {
        onOff = fire.OnOff;
    }
}
