using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCapsule : MonoBehaviour
{
    public Item item;
    private void Awake()
    {
        item = new Shoes();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {

    }

    private void Update()
    {

    }

    public void InsertItem(Item it)
    {
        item = it;
        GetComponent<SpriteRenderer>().sprite = item.sprite;
    }

    public Item TakeItem()
    {
        Item ritem = item;
        item = null;
        gameObject.SetActive(false);
        return ritem;
    }
}