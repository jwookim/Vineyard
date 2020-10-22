using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator animator;
    public float hp;
    public float speed;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        hp = 5f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void Attack()
    {

    }

    protected virtual void Move()
    {

    }

    public virtual void Damage()
    {
        if (hp > 0f)
            hp -= 0.5f;
    }
}
