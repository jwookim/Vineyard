using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    const float rotateTime = 1f;
    const float rotateSpeed = 3f;
    const int coinScore = 10;

    protected override void Start()
    {
        base.Start();
        score = coinScore;
    }


    
    protected override IEnumerator DirectingGetItem()
    {
        audioSource.Play();

        float limit = rotateTime;
        Quaternion defaultRotation = transform.GetChild(0).rotation;
        Vector3 defaultPosition = transform.GetChild(0).position;
        while (limit > 0f)
        {
            transform.GetChild(0).Rotate(Vector3.up * Time.deltaTime * rotateSpeed * 360f, Space.Self);
            transform.GetChild(0).position += new Vector3(0f, 0.5f, -0.5f) * Time.deltaTime;
            limit -= Time.deltaTime;
            yield return null;
        }

        audioSource.Stop();
        gameObject.SetActive(false);
        transform.GetChild(0).rotation = defaultRotation;
        transform.GetChild(0).position = defaultPosition;
    }
}
