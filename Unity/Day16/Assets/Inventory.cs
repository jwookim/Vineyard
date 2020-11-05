using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject itemPrefab;
    [SerializeField] private List<Slot> slots;

    private void Awake()
    {
        slots = new List<Slot>();
    }
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Slot slot = transform.GetChild(i).GetComponent<Slot>();
            slots.Add(slot);
        }
    }
    public void CreateItem()
    {
        foreach (var slot in slots)
        {
            if (slot.transform.childCount == 0)
            {
                GameObject obj = Instantiate(itemPrefab, slot.transform);
                Item item = obj.GetComponent<Item>();
                int index = Random.Range(0, sprites.Length);
                item.image.sprite = sprites[index];
                break;
            }
        }
    }

}
