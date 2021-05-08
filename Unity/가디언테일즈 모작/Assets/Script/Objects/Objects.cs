using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objects : MonoBehaviour
{
    protected const float gravityScale = 9.81f;
    protected bool Interactable;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void OnEnable()
    {
        StopAllCoroutines();
        CancelInvoke();
    }

    protected virtual void OnDisable()
    {

    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Collision();
    }*/

    public virtual void Collision()
    {
    }

    protected void Destroy()
    {
        gameObject.SetActive(false);
    }
}
