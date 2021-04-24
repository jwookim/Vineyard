using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const int PartyMax = 4;
    [SerializeField] Character Leader;
    Character[] characters;
    Inventory inventory;

    private void Awake()
    {

        characters = new Character[PartyMax - 1];
    }
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Control();
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

    private void Control()
    {
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            dir.y += 1f;

        if (Input.GetKey(KeyCode.DownArrow))
            dir.y -= 1f;

        if (Input.GetKey(KeyCode.LeftArrow))
            dir.x -= 1f;

        if (Input.GetKey(KeyCode.RightArrow))
            dir.x += 1f;

        Leader.MoveControl(dir);

        if (Input.GetKey(KeyCode.LeftShift))
            Leader.doRun();

        if (Input.GetKeyUp(KeyCode.LeftShift))
            Leader.doWalk();


        if (Input.GetKeyDown(KeyCode.Space))
            Leader.Interaction();
    }
}
