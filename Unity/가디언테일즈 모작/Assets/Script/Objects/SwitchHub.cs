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
        if (switches.Count == 0)
            return false;

        foreach (var sw in switches)
        {
            if (sw != null && !sw.onOff)
                return false;
        }

        return true;
    }
}
