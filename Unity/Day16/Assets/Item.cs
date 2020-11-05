using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform myParent;
    public Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }
    void OnEnable()
    {
        myParent = transform.parent;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.parent.parent.parent);
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(myParent);
        transform.position = myParent.position;
        image.raycastTarget = true;
    }
}