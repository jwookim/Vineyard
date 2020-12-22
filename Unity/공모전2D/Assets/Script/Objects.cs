using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    protected Rigidbody2D rigid;
    // Start is called before the first frame update

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        GetComponentInParent<ObjectManager>().objectList.Add(this);
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }


    public virtual void Rotation(float angle)
    {
        /*angle -= 90f;
        angle *= -1f;
        float x = Mathf.Cos(Mathf.Deg2Rad * angle) * rigid.velocity.magnitude;
        float y = Mathf.Sin(Mathf.Deg2Rad * angle) * rigid.velocity.magnitude;

        rigid.velocity = new Vector2(x, y);*/

        Debug.Log(angle);
        Debug.Log(angle.GetType());
        rigid.velocity = Quaternion.Euler(0f, 0f, angle) * rigid.velocity;
    }

    public void ControlGravity(float volume)
    {
        rigid.gravityScale += volume / 2f;
    }
}
