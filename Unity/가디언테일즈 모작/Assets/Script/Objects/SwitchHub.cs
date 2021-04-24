using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHub : MonoBehaviour
{
    [SerializeField] List<Switch> switches;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool Check()
    {
        bool check = true;
        foreach (var sw in switches)
        {
            if (sw != null)
            {
                if (!sw.onOff)
                {
                    check = false;
                    break;
                }
            }
        }

        return check;
    }
}
