using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] GameObject mark;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Enter : {other.name}");
        if(other.tag == "Player")
        {
            mark.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Exit : {other.name}");

        if (other.tag == "Player")
        {
            mark.SetActive(false);
        }
    }
}
