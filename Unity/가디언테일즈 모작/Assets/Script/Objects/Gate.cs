using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Objects
{
    [SerializeField] GameObject destination;

    protected override void Awake()
    {
        base.Awake();
        gameObject.layer = LayerMask.NameToLayer("ETC object");
    }

    public override void Interaction()
    {
        GameManager.Instance.StartCoroutine("Teleport", destination.transform.position);
    }
}
