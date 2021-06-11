using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singletone<GameManager>
{
    const float PauseSpeed = 0.0001f;
    const float DefaultSpeed = 1f;
    const float TwiceSpeed = 2f;
    public float TimeScale { get; private set; }
    Player playerManager;

    [SerializeField] Notice notice;
    [SerializeField] Covering covering;

    private void Awake()
    {
        playerManager = GetComponent<Player>();
    }

    private void Start()
    {
        InitSpeed();
    }


    public void PauseGame()
    {
        TimeScale = PauseSpeed;
        var tmp = FindObjectsOfType<Animator>();

        playerManager.controlable = false;

        foreach(var anim in tmp)
        {
            anim.speed = PauseSpeed;
        }
    }


    public void InitSpeed()
    {
        TimeScale = DefaultSpeed;
        var tmp = FindObjectsOfType<Animator>();

        playerManager.controlable = true;

        foreach (var anim in tmp)
        {
            anim.speed = DefaultSpeed;
        }
    }


    public void TwiceTimeScale()
    {
        TimeScale = TwiceSpeed;
        var tmp = FindObjectsOfType<Animator>();

        foreach (var anim in tmp)
        {
            anim.speed = TwiceSpeed;
        }
    }


    public void NoticeText(string[] content)
    {
        notice.Modification(content);
    }



    IEnumerator Teleport(Vector3 pos)
    {
        playerManager.controlable = false;
        yield return covering.Activate();

        playerManager.Placement(pos);

        yield return new WaitForSeconds(0.5f);

        yield return covering.StartCoroutine("TurnOn");

        playerManager.controlable = true;
    }
}
