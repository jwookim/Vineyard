using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : Objects
{
    [SerializeField] bool Passable;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        if(Passable)
        {
            gameObject.layer = LayerMask.NameToLayer("Cliff");
        }
        else
        {
            tag = "Obstacle";
            gameObject.layer = LayerMask.NameToLayer("Obstacle");
        }
    }



}
