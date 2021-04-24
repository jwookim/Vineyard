using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LiftableObject : Objects
{
    const float ThrowSpeed = 3f;
    Character lifting;
    // Start is called before the first frame update
    protected override void Start()
    {
        lifting = null;
        Interactable = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Transfer();
    }

    void Transfer()
    {
        if(lifting!=null)
        {
            transform.position = lifting.transform.position + new Vector3(0f, 1f, 0.5f);
        }
    }

    public bool Lifting(Character ch)
    {
        if (Interactable)
        {
            GetComponent<Rigidbody>().useGravity = false;
            lifting = ch;
            return true;
        }

        return false;
    }

    public void Throw(DIRECT dir, float Speed)
    {
        GetComponent<Rigidbody>().useGravity = true;
        lifting = null;
        switch(dir)
        {
            case DIRECT.DIR_FRONT:
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -Speed) * ThrowSpeed;
                break;
            case DIRECT.DIR_BACK:
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, Speed) * ThrowSpeed;
                break;
            case DIRECT.DIR_LEFT:
                GetComponent<Rigidbody>().velocity = new Vector3(-Speed, 0f, 0f) * ThrowSpeed;
                break;
            case DIRECT.DIR_RIGHT:
                GetComponent<Rigidbody>().velocity = new Vector3(Speed, 0f, 0f) * ThrowSpeed;
                break;
        }
    }
}
