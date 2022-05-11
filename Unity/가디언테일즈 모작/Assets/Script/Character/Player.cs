using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const int LeaderNum = 0;
    const int PartyMax = 4;
    //[SerializeField] Character Leader;
    Character[] party;
    Inventory inventory;

    public bool controlable;

    private void Awake()
    {
        party = new Character[PartyMax];

    }
    // Start is called before the first frame update
    private void Start()
    {
        party[LeaderNum] = FindObjectOfType<Character>();
        controlable = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (controlable)
            Control();
        
    }


    private void CharSort()
    {
        for (int i = 0; i < PartyMax - 1; i++)
        {
            if (party[i] == null)
            {
                for (int j = i + 1; j < PartyMax; j++)
                {
                    if (party[j] != null)
                    {
                        party[i] = party[j];
                        party[j] = null;
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

        party[LeaderNum].MoveControl(dir);


        if (Input.GetKeyDown(KeyCode.LeftControl))
            party[LeaderNum].KeyDownAttack();
        
        if (Input.GetKeyUp(KeyCode.LeftControl))
            party[LeaderNum].KeyUpAttack();

        if (Input.GetKey(KeyCode.LeftShift))
            party[LeaderNum].doRun();

        if (Input.GetKeyUp(KeyCode.LeftShift))
            party[LeaderNum].doWalk();


        if (Input.GetKeyDown(KeyCode.Space))
            party[LeaderNum].Interaction();

        if (Input.GetKeyUp(KeyCode.Space))
            party[LeaderNum].InteractionCancel();

        if (party[LeaderNum].gameObject.activeSelf)
            GameManager.Instance.CameraMove(party[LeaderNum].gameObject);
    }

    public void Placement(Vector3 pos)
    {
        foreach(var ch in party)
        {
            if(ch != null)
            {
                ch.transform.position = pos;
            }
        }
    }
}
