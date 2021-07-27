using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PushableObject : Objects
{
    const float MoveDistance = 1f;
    const float MoveSpeed = 2f;

    protected override void Awake()
    {
        base.Awake();
        gameObject.layer = LayerMask.NameToLayer("Pushable");
    }

    [SerializeField] AudioClip Audio_Drag;
    public void Push(DIRECT dir)
    {
        StopAllCoroutines();

        StartCoroutine(onPush(dir));
    }

    IEnumerator onPush(DIRECT dir)
    {
        Vector3 vector;
        float distance = MoveDistance;
        float tmp = distance;

        Vector3 Extents = transform.lossyScale * Standard.halfExtentsScale;

        switch(dir)
        {
            case DIRECT.DIR_FRONT:
                vector = new Vector3(0f, 0f, -1f);
                Extents.z = 0f;
                break;
            case DIRECT.DIR_BACK:
                vector = new Vector3(0f, 0f, 1f);
                Extents.z = 0f;
                break;
            case DIRECT.DIR_LEFT:
                vector = new Vector3(-1f, 0f, 0f);
                Extents.x = 0f;
                break;
            case DIRECT.DIR_RIGHT:
                vector = new Vector3(1f, 0f, 0f);
                Extents.x = 0f;
                break;
            default:
                vector = Vector3.zero;
                break;
        }

        if (!Physics.BoxCast(transform.position, Extents, vector, Quaternion.identity, Standard.CollisionRange + GetHalfSize()))
        {
            audioSource.clip = Audio_Drag;
            audioSource.Play();

            while (!Physics.BoxCast(transform.position, Extents, vector, Quaternion.identity, Standard.CollisionRange + GetHalfSize()) && distance > 0f)
            {
                tmp = distance - Time.deltaTime * MoveSpeed * GameManager.Instance.TimeScale;

                if (tmp > 0)
                    transform.Translate(vector * Time.deltaTime * MoveSpeed * GameManager.Instance.TimeScale);
                else
                    transform.Translate(vector * distance);

                distance = tmp;

                yield return null;
            }

            audioSource.Stop();
        }
    }
}
