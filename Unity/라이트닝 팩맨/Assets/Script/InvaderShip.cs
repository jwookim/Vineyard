using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderShip : MonoBehaviour
{
    const float generateTime = 5f;

    Queue<Enemy> Enemies;


    float Timer;
    private void Awake()
    {
        Enemies = new Queue<Enemy>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        TimeProgress();
    }

    void TimeProgress()
    {
        if (Timer > 0f)
            Timer -= Time.deltaTime * GameManager.Instance.timeScale;
        else
        {
            if (Enemies.Count > 0)
            {
                KickoutInvader();

                Timer = generateTime;
            }
        }
    }

    public void StorageInvader(GameObject invader)
    {
        invader.SetActive(false);
        invader.transform.position = GameManager.Instance.Max_x % 2 == 0 ? GameManager.Instance.ShipPosition + new Vector3(0.5f, 0f, 0f) : GameManager.Instance.ShipPosition;

        Enemies.Enqueue(invader.GetComponent<Enemy>());
    }


    void KickoutInvader()
    {
        if (Enemies.Count <= 0)
            return;

        Enemy invader = Enemies.Dequeue();

        invader.gameObject.SetActive(true);

        invader.Initialization();

    }

    public void Clear()
    {
        while (Enemies.Count > 0)
            KickoutInvader();

        Timer = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<Enemy>().EatenCheck())
                StorageInvader(other.gameObject);
        }
    }
}
