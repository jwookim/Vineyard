using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PushableObject : Objects
{
    const float MoveDistance = 1f;
    const float MoveSpeed = 2f;

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

        switch(dir)
        {
            case DIRECT.DIR_FRONT:
                vector = new Vector3(0f, 0f, -1f);
                break;
            case DIRECT.DIR_BACK:
                vector = new Vector3(0f, 0f, 1f);
                break;
            case DIRECT.DIR_LEFT:
                vector = new Vector3(-1f, 0f, 0f);
                break;
            case DIRECT.DIR_RIGHT:
                vector = new Vector3(1f, 0f, 0f);
                break;
            default:
                vector = Vector3.zero;
                break;
        }

        while (!Physics.BoxCast(transform.position, transform.lossyScale * Standard.halfExtentsScale, vector, Quaternion.identity, Standard.CollisionRange) && distance > 0f)
        {
            tmp = distance - Time.deltaTime * MoveSpeed * GameManager.Instance.TimeScale;

            if (tmp > 0)
                transform.Translate(vector * Time.deltaTime * MoveSpeed * GameManager.Instance.TimeScale);
            else
                transform.Translate(vector * distance);

            distance = tmp;

            yield return null;
        }
    }
}
