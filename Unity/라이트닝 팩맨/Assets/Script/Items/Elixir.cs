using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elixir : Item
{
    const int ElixirScore = 50;
    protected override void Start()
    {
        base.Start();
        score = ElixirScore;
    }
    protected override IEnumerator DirectingGetItem()
    {
        audioSource.Play();

        yield return GameManager.Instance.GetElixir();


        audioSource.Stop();
        gameObject.SetActive(false);
    }
}
