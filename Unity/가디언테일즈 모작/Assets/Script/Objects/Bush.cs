using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : LiftableObject
{
    const float BurnTime = 3f;
    protected Fire fire;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        fire = transform.GetChild(0).GetComponent<Fire>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        BurnCheck();
    }

    void BurnCheck()
    {
        if(fire.OnOff)
        {
            if (!IsInvoking("Destroy"))
                Invoke("Destroy", BurnTime);
        }
    }

    public override void Collision()
    {
        Destroy();
    }

}
