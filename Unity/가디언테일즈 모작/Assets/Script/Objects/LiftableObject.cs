using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LiftableObject : Objects
{
    protected const float Elasticity = 0.5f;
    protected const float Drag = 0.2f;
    const float ThrowSpeed = 7f;
    Character lifting;
    // Start is called before the first frame update
    protected override void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        Transfer();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Init();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        StopAllCoroutines();
    }

    void Transfer()
    {
        if(lifting!=null)
        {
            transform.position = lifting.transform.position + new Vector3(0f, 1f, 0f);
        }
    }

    private void Init()
    {
        lifting = null;
        Interactable = true;
    }

    public bool Lifting(Character ch)
    {
        if (Interactable)
        {
            lifting = ch;
            Interactable = false;
            return true;
        }

        return false;
    }

    public void Throw(DIRECT dir, float Speed)
    {
        Debug.Log(Speed);
        lifting = null;
        switch(dir)
        {
            case DIRECT.DIR_FRONT:
                StartCoroutine(Throw(new Vector3(0f, 0f, -Speed) * ThrowSpeed));
                break;
            case DIRECT.DIR_BACK:
                StartCoroutine(Throw(new Vector3(0f, 0f, Speed) * ThrowSpeed));
                break;
            case DIRECT.DIR_LEFT:
                StartCoroutine(Throw(new Vector3(-Speed, 0f, 0f) * ThrowSpeed));
                break;
            case DIRECT.DIR_RIGHT:
                StartCoroutine(Throw(new Vector3(Speed, 0f, 0f) * ThrowSpeed));
                break;
        }
    }

    IEnumerator Throw(Vector3 velocity)
    {
        Vector3 Direct = velocity.normalized;
        int count = 2;
        RaycastHit hit;
        while (count > 0)
        {
            transform.Translate(velocity * Time.deltaTime);

            velocity -= velocity * Drag * Time.deltaTime;   // 감속
            velocity.y -= gravityScale * Time.deltaTime;    // 중력 가속

            /*foreach(var ob in Physics.OverlapBox(transform.position, transform.lossyScale * 0.6f))
            {
                if (ob.transform == transform)
                    continue;

                if(ob.tag == "Object")
                    ob.GetComponent<Objects>().Collision();
            }*/

            if(Physics.BoxCast(transform.position, transform.lossyScale * 0.5f, Direct, out hit ,Quaternion.identity, 0.1f))
            {
                if(hit.transform.tag == "Object")
                    hit.transform.GetComponent<Objects>().Collision();
                //Collision();
                Debug.Log("벽" + count);

                if (hit.transform.gameObject.activeSelf)
                {
                    if (count > 1)
                    {
                        count--;
                        Direct *= -1f;
                        velocity.x *= -1f;
                        velocity.z *= -1f;
                        velocity *= Elasticity;
                    }
                    else
                    {
                        velocity.x *= 0f;
                        velocity.z *= 0f;
                    }
                }
            }

            if (Physics.BoxCast(transform.position, transform.lossyScale * 0.5f, Vector3.down, out hit, Quaternion.identity, 0.1f, LayerMask.GetMask("Obstacle")))
            {
                //Collision();
                Debug.Log("땅" + count);
                if (--count > 0)
                {
                    velocity.y *= -1f;
                    velocity *= Elasticity;
                }
                transform.position = new Vector3(transform.position.x, hit.transform.position.y + hit.transform.lossyScale.y * 0.5f + transform.lossyScale.y * 0.5f + 0.1f, transform.position.z);
            }
            yield return null;
        }

        Init();
    }
}
