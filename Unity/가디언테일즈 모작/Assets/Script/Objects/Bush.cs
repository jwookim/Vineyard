using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : LiftableObject
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


    public override bool Collision()  // �浹 �� �ı��Ǿ��ٴ� �ǹ̷� true ��ȯ
    {
        Destroy();
        return true;
    }

}
