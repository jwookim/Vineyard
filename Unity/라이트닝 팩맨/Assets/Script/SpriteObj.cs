using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpriteObj : MonoBehaviour
{

    public void toProspective()
    {
        transform.GetChild(0).rotation = Quaternion.Euler(transform.rotation.x, -45f, transform.rotation.z);
    }

    public void toOrthograghpic()
    {
        transform.GetChild(0).rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
    }
}
