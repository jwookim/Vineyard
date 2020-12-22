using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /*private static Inventory instance;
    public static Inventory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Inventory>();

                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<Inventory>();
                    obj.name = "GameController";
                }
            }

            return instance;
        }
    }


    public Sprite[] sprites;
    public GameObject itemPrefab;
    [SerializeField] private Transform inventoryObj;
    [SerializeField] private GameObject bg;
    [SerializeField] private List<Slot> slots;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        inventoryObj = GameObject.FindGameObjectWithTag("Inventory").transform;
        bg = inventoryObj.parent.gameObject;
        slots = new List<Slot>();

        for (int i = 0; i < inventoryObj.childCount; i++)
        {
            Slot slot = inventoryObj.GetChild(i).GetComponent<Slot>();
            slots.Add(slot);
        }

        bg.SetActive(false);
    }

    public void CreateItem(int index)
    {
        foreach (var slot in slots)
        {
            if (slot.transform.childCount == 0)
            {
                bg.SetActive(true);
                GameObject obj = Instantiate(itemPrefab, slot.transform);
                Item item = obj.GetComponent<Item>();
                item.image.sprite = sprites[index];
                bg.SetActive(false);
                break;
            }
        }
    }*/

}