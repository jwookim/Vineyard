using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : ISwitch
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        push = true;
        Debug.Log(push);
    }

    private void OnTriggerExit(Collider collision)
    {
        push = false;
        Debug.Log(push);
    }

}
