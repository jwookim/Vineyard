using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elixir : Item
{
    protected override void Start()
    {
        base.Start();
        score = 50;
    }
    protected override IEnumerator DirectingGetItem()
    {
        audioSource.Play();

        yield return GameManager.Instance.GetElixir();


        audioSource.Stop();
        gameObject.SetActive(false);
    }
}
