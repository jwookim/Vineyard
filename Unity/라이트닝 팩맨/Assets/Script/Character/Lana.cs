using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lana : Character
{
    Vector3 nextDir;

    bool controllable;
    bool isFever;

    [SerializeField] SkeletonDataAsset Lc_front;
    [SerializeField] SkeletonDataAsset Lc_back;
    [SerializeField] SkeletonDataAsset Lc_side;

    GameObject victoryMecanim;

    [SerializeField] AudioClip Scream, Clear;

    protected override void Awake()
    {
        base.Awake();
        victoryMecanim = transform.Find("Victory Mecanim").gameObject;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Initialization();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (controllable)
        {
            Control();
            base.Update();
        }
    }

    private void Control()
    {
        Vector3 dir;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            dir = Vector3.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            dir = Vector3.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            dir = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            dir = Vector3.right;
        else
            return;

        if (dir == curDir * -1f)
            TurnBack();
        else
            nextDir = dir;
    }

    protected override void ChangeSkel(DIRECT direct)
    {
        if (!isFever)
            base.ChangeSkel(direct);
        else
        {
            switch (direct)
            {
                case DIRECT.FRONT:
                    ChangeSkel(Lc_front);
                    break;
                case DIRECT.BACK:
                    ChangeSkel(Lc_back);
                    break;
                case DIRECT.SIDE:
                    ChangeSkel(Lc_side);
                    break;
            }
        }
    }

    protected override void Turn(Vector3 dir)
    {
        base.Turn(dir);
        nextDir = curDir;
    }

    public void Collapse()
    {
        controllable = false;
        StartCoroutine(CollapseCoroutine());
    }

    IEnumerator CollapseCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        ChangeSkel(DIRECT.SIDE);
        GetComponent<AudioSource>().clip = Scream;
        GetComponent<AudioSource>().Play();
        animator.SetBool("Collapse", true);
    }

    public override void Initialization()
    {
        Turn(Vector3.right);
        base.Initialization();
        curDir = Vector3.zero;
        nextDir = Vector3.zero;

        isFever = false;
        controllable = true;
        ChangeSkel(DIRECT.SIDE);

        victoryMecanim.SetActive(false);

        animator.SetBool("isFever", false);
        animator.SetBool("Move", false);
        animator.SetBool("Collapse", false);
        animator.SetBool("Victory", false);

        animator.Play("Idle_side", 0, 0f);
        animator.Play("Idle_side", 1, 0f);
    }


    protected override void DicisionDir()
    {
        base.DicisionDir();

        if (!GameManager.Instance.mapCheck(transform.position + nextDir))
            Turn(nextDir);
        /*else if (GameManager.Instance.mapCheck(transform.position + curDir))
            Turn(Vector3.zero);*/

    }


    public Vector3 getFrontCoord(int distance = 0)
    {
        return transform.position + curDir * distance;
    }


    public void DrinkElixir()
    {
        isFever = true;

        if (curDir == Vector3.up)
            ChangeSkel(DIRECT.BACK);
        else if (curDir == Vector3.down)
            ChangeSkel(DIRECT.FRONT);
        else
            ChangeSkel(DIRECT.SIDE);

        Speed = runSpeed;

        animator.SetBool("isFever", true);
    }

    public void RemoveElixir()
    {
        isFever = false;

        if (curDir == Vector3.up)
            ChangeSkel(DIRECT.BACK);
        else if (curDir == Vector3.down)
            ChangeSkel(DIRECT.FRONT);
        else
            ChangeSkel(DIRECT.SIDE);

        Speed = defaultSpeed;

        animator.SetBool("isFever", false);
    }

    public void Victory()
    {
        ChangeSkel(DIRECT.SIDE);
        animator.SetBool("Victory", true);
        if (!isFever)
            victoryMecanim.SetActive(true);

        GetComponent<AudioSource>().clip = Clear;
        GetComponent<AudioSource>().Play();

        controllable = false;

    }

    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        
    }*/
}
