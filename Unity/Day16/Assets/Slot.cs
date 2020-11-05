using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Item item = eventData.pointerDrag.GetComponent<Item>();
        if (transform.childCount == 1)
        {                    //slot에 있는 0번째 자식에 있는 item 스크립
            Item existentItem = transform.GetChild(0).GetComponent<Item>();
            //item 스크립에 있는 myParent값을 드레그중인 스크립의 부모
            existentItem.myParent = item.myParent;


            //자식에 있는 item의 부모는 드레그중인 myParent
            existentItem.transform.SetParent(item.myParent);
            existentItem.transform.position = item.myParent.position;

        }

        item.myParent = transform;
        
    }
}
