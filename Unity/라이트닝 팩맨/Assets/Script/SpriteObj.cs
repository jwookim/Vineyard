using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpriteObj : MonoBehaviour
{

    public void toProspective()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).rotation = Quaternion.Euler(-45f, transform.rotation.y, transform.rotation.z);
    }

    public void toOrthograghpic()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
    }
}
