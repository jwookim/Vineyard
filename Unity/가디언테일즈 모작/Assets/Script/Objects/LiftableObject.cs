using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LiftableObject : Objects
{
    protected const float Elasticity = 0.5f;
    protected const float Drag = 0.2f;
    const float ThrowSpeed = 7f;
    const float LiftHeight = 0.8f;
    Character lifting;

    [SerializeField] protected AudioClip Audio_Collision;

    protected override void Awake()
    {
        base.Awake();
        gameObject.layer = LayerMask.NameToLayer("Liftable");
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
            transform.position = lifting.transform.position + new Vector3(0f, LiftHeight, 0f);
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
        //Debug.Log(Speed);
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
        bool check;
        RaycastHit hit;

        Vector3 HorizontalExtents = transform.lossyScale * Standard.halfExtentsScale;
        if (velocity.x != 0)
            HorizontalExtents.x = 0f;
        else if (velocity.z != 0)
            HorizontalExtents.z = 0f;

        Vector3 VerticalExtents = transform.lossyScale * Standard.halfExtentsScale;
        VerticalExtents.y = 0f;

        while (count > 0)
        {
            check = false;
            transform.Translate(velocity * Time.deltaTime * GameManager.Instance.TimeScale);

            velocity -= velocity * Drag * Time.deltaTime * GameManager.Instance.TimeScale;   // 감속
            velocity.y -= Standard.gravityScale * Time.deltaTime * GameManager.Instance.TimeScale;    // 중력 가속

            /*foreach(var ob in Physics.OverlapBox(transform.position, transform.lossyScale * 0.6f))
            {
                if (ob.transform == transform)
                    continue;

                if(ob.tag == "Object")
                    ob.GetComponent<Objects>().Collision();
            }*/
            if (velocity.y < 0f)
            {
                if (Physics.Raycast(transform.position, Vector3.down, out hit, Standard.CollisionRange + GetHalfSize(), LayerMask.GetMask("Ground") | LayerMask.GetMask("Stairs")))
                {
                    Collision();
                    transform.position += new Vector3(0f, transform.lossyScale.y / 2f - hit.distance, 0f);
                    if (--count > 0)
                    {
                        velocity.y *= -1f;
                        velocity *= Elasticity;

                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Stairs"))
                            transform.position += new Vector3(0f, 0.5f, 0f);

                    }

                    audioSource.clip = Audio_Collision;
                    audioSource.Play();
                }
            }

            if(Physics.BoxCast(transform.position, HorizontalExtents, Direct, out hit ,Quaternion.identity, Standard.CollisionRange + GetHalfSize()) && hit.transform.gameObject.layer != LayerMask.NameToLayer("Stairs") && hit.transform.gameObject.layer != LayerMask.NameToLayer("Cliff"))
            {
                if(hit.transform.tag == "Object")
                    check = hit.transform.GetComponent<Objects>().Collision();
                Collision();

                if (!check)
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

            yield return null;
        }

        Init();
    }
}
