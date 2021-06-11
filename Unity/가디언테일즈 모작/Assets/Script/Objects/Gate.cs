using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Objects
{
    [SerializeField] GameObject destination;

    public override void Interaction()
    {
        GameManager.Instance.StartCoroutine("Teleport", destination.transform.position);
    }
}
