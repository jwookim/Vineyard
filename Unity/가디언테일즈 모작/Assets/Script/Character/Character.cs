using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DIRECT
{
    DIR_FRONT,
    DIR_BACK,
    DIR_LEFT,
    DIR_RIGHT
}

public abstract class Character : MonoBehaviour
{
    const float DustGenTime = 0.07f;

    const float DefaultScale = 1.0f;
    const float RunScale = 1.5f;

    [SerializeField] private Animator animator;
    private GameObject[] CharDir = new GameObject[4];

    private DIRECT curDir;

    private Vector2 dir;

    private bool isRun;

    protected Weapon weapon;

    protected Action Attack;

    protected float Speed;
    protected virtual void Awake()
    {
        CharDir[(int)DIRECT.DIR_FRONT] = gameObject.transform.Find("Front").gameObject;
        CharDir[(int)DIRECT.DIR_BACK] = gameObject.transform.Find("Back").gameObject;
        CharDir[(int)DIRECT.DIR_LEFT] = gameObject.transform.Find("Left").gameObject;
        CharDir[(int)DIRECT.DIR_RIGHT] = gameObject.transform.Find("Right").gameObject;

        curDir = DIRECT.DIR_FRONT;
        isRun = false;

        weapon = null;

        dir = Vector2.zero;

        Speed = 10f;

        Attack = null;
    }
    protected virtual void Start()
    {
    }

    protected virtual void OnEnable()
    {
        for(int i=0; i<4; i++)
        {
            if (i == (int)curDir)
                CharDir[i].SetActive(true);
            else
                CharDir[i].SetActive(false);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Control();

        Move();
    }


    protected void Move()
    {
        float scale = DefaultScale;

        if (isRun)
            scale = RunScale;

        animator.SetInteger("State", (int)dir.magnitude);
        transform.position += new Vector3(dir.x, 0f, dir.y) * Speed * scale * Time.deltaTime;
    }

    /*protected void Run()
    {
        animator.SetInteger("State", (int)dir.magnitude);

        transform.position += new Vector3(dir.x, 0f, dir.y) * Speed * Time.deltaTime;
    }*/

    protected void Control()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (dir.x == 0f || dir.y != 0f)
                Turn(DIRECT.DIR_BACK);
            dir.y = 1f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (dir.x == 0f || dir.y != 0f)
                Turn(DIRECT.DIR_FRONT);
            dir.y = -1f;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (dir.y > 0f)
            {
                dir.y = 0f;

                if(dir.x != 0f)
                {
                    if (dir.x > 0f)
                        Turn(DIRECT.DIR_RIGHT);
                    else
                        Turn(DIRECT.DIR_LEFT);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (dir.y < 0f)
            {
                dir.y = 0f;

                if (dir.x != 0f)
                {
                    if (dir.x > 0f)
                        Turn(DIRECT.DIR_RIGHT);
                    else
                        Turn(DIRECT.DIR_LEFT);
                }
            }
        }



        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (dir.y == 0f || dir.x != 0f)
                Turn(DIRECT.DIR_LEFT);
            dir.x = -1f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (dir.y == 0f || dir.x != 0f)
                Turn(DIRECT.DIR_RIGHT);
            dir.x = 1f;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (dir.x < 0f)
            {
                dir.x = 0f;

                if (dir.y != 0f)
                {
                    if (dir.y > 0f)
                        Turn(DIRECT.DIR_BACK);
                    else
                        Turn(DIRECT.DIR_FRONT);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (dir.x > 0f)
            {
                dir.x = 0f;

                if (dir.y != 0f)
                {
                    if (dir.y > 0f)
                        Turn(DIRECT.DIR_BACK);
                    else
                        Turn(DIRECT.DIR_FRONT);
                }
            }
        }

        /*dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));*/
        dir.Normalize();



        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRun = true;
            StartCoroutine("makeDust");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRun = false;
            StopCoroutine("makeDust");
        }
    }

    protected void Turn(DIRECT direct)
    {
        if (curDir != direct)
        {
            CharDir[(int)curDir].SetActive(false);
            CharDir[(int)direct].SetActive(true);
            curDir = direct;
        }
    }


    protected void SwordAttack()
    {

    }

    protected void Skill()
    {
        if (weapon != null)
            ;
    }


    IEnumerator makeDust()
    {
        while (true)
        {
            if (dir != Vector2.zero)
                ObjectPoolManger.Instance.GenerateDust(CharDir[(int)curDir].transform.Find("Shadow").position);
            yield return new WaitForSeconds(DustGenTime);
        }
    }    
}
