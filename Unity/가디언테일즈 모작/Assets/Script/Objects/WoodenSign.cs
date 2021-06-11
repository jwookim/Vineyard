using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSign : Objects
{
    [SerializeField] string[] content;



    public override void Interaction()
    {
        GameManager.Instance.NoticeText(content);
    }
}
