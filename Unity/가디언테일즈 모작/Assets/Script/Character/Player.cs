using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const int PartyMax = 4;
    Character[] characters;
    Inventory inventory;

    private void Awake()
    {
        characters = new Character[PartyMax];
    }
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
    }


    private void CharSort()
    {
        for (int i = 0; i < PartyMax - 1; i++)
        {
            if (characters[i] == null)
            {
                for (int j = i + 1; j < PartyMax; j++)
                {
                    if (characters[j] != null)
                    {
                        characters[i] = characters[j];
                        characters[j] = null;
                    }
                }
            }
        }
    }
}
