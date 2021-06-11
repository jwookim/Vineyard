using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsBlock : Objects
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Contact(Character ch)
    {
        ch.HitbyTrap();
    }
}
