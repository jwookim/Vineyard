using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    const float rotateTime = 1f;
    const float rotateSpeed = 3f;

    protected override void Start()
    {
        base.Start();
        score = 10;
    }


    
    protected override IEnumerator DirectingGetItem()
    {
        audioSource.Play();

        float limit = rotateTime;
        Quaternion defaultRotation = transform.GetChild(0).rotation;
        while(limit > 0f)
        {
            transform.GetChild(0).Rotate(Vector3.up * Time.deltaTime * rotateSpeed * 360f, Space.Self);
            transform.position += new Vector3(0f, Time.deltaTime, 0f);
            limit -= Time.deltaTime;
            yield return null;
        }

        transform.GetChild(0).rotation = defaultRotation;
        audioSource.Stop();
        gameObject.SetActive(false);
    }
}
