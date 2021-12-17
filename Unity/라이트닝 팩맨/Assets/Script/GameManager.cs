using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public enum EnemyName
{
    Invader_soldier,
    Invader_knight,
    Invader_magician,
    Invader_assassin
}

enum CameraName
{
    Camera_3D,
    Camera_2D,
    Directing_Camera
}
public class GameManager : Singletone<GameManager>
{
    public int Score;

    const float defualtTimescale = 1f;
    public float timeScale;

    const float DirectingTime_Elixir = 4f;
    const float DirectingTime_Termination = 3f;

    [SerializeField] GameObject Camera_3D, Camera_2D, Camera_direct;

    CameraName actCamera;

    AudioSource audioSource;
    [SerializeField] AudioClip mainBGM, feverBGM;


    private int mapSizeX;
    public int Max_x { get { return mapSizeX; } }

    private int mapSizeY;
    public int Max_y { get { return mapSizeY; } }
    [SerializeField] bool[,] map;
    [SerializeField] private List<Tile> Tiles;
    [SerializeField] private Stack<Tile> dumpTiles;

    public Vector3 ShipPosition { get; private set; }

    [SerializeField] GameObject TilePrefab;

    Text ScoreBoard;

    [SerializeField]GameObject Player;
    [SerializeField]GameObject[] Enemies;

    const float FeverLimit = 10f;
    float FeverTime;
    // Start is called before the first frame update

    private void Awake()
    {
        Tiles = new List<Tile>();
        dumpTiles = new Stack<Tile>();

        //Enemies = new GameObject[4];
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ScoreBoard = GameObject.Find("Canvas").transform.Find("ScoreBoard").Find("Score").GetComponent<Text>();
        Score = 0;
        timeScale = defualtTimescale;
        UpdateMap();

        ChangeBGM(mainBGM);
        SwapCamera(CameraName.Camera_3D);
    }

    // Update is called once per frame
    void Update()
    {
        FeverCheck();
    }


    void UpdateMap()
    {
        mapSizeX = 7;
        mapSizeY = 7;
        map = new bool[mapSizeY, mapSizeX];

        foreach(var ch in Enemies)
        {
            ch.GetComponent<Enemy>().SetScatterPoint(mapSizeX, mapSizeY);
        }

        foreach (var tile in Tiles)
        {
            tile.gameObject.SetActive(false);
            dumpTiles.Push(tile);
        }

        GameObject tmp;
        for (int y = 0; y < mapSizeY; y++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                if (dumpTiles.Count > 0)
                {
                    tmp = dumpTiles.Pop().gameObject;
                    tmp.SetActive(true);
                }
                else
                {
                    tmp = Instantiate(TilePrefab);
                    tmp.transform.parent = GameObject.Find("Map").transform.Find("Tiles");
                }

                tmp.transform.position = new Vector3(x, y, 0f);
                Tiles.Add(tmp.GetComponent<Tile>());

                if (x == 0 || x == mapSizeX - 1 || y == 0 || y == mapSizeY - 1)
                    map[y, x] = !tmp.GetComponent<Tile>().PassCheck();
                else
                    map[y, x] = tmp.GetComponent<Tile>().WallCheck();
            }
        }

        map[2, 2] = true;
        map[4, 2] = true;
        map[2, 4] = true;
        map[4, 4] = true;

        foreach (var tile in Tiles)
        {
            tile.UpdateSprite();
        }
    }

    public bool mapCheck(int x, int y)
    {
        if (x < 0)
            x += mapSizeX;
        else if (x > mapSizeX)
            x -= mapSizeX;

        if (y < 0)
            y += mapSizeY;
        else if (y > mapSizeY)
            y -= mapSizeY;


        return map[y, x];
    }

    public bool mapCheck(Vector3 vector)
    {
        int x = Mathf.RoundToInt(vector.x);
        int y = Mathf.RoundToInt(vector.y);

        if (x < 0)
            x += mapSizeX;
        else if (x > mapSizeX)
            x -= mapSizeX;

        if (y < 0)
            y += mapSizeY;
        else if (y > mapSizeY)
            y -= mapSizeY;

        return map[y, x];
    }

    void FeverCheck()
    {
        if (FeverTime > 0f)
        {
            FeverTime -= Time.deltaTime * timeScale;

            if (FeverTime <= 0f)
            {
                FeverTime = 0f;

                StartCoroutine(TerminationElixir());
            }
        }
    }

    void ChangeBGM(AudioClip audio)
    {
        if(audioSource.clip != audio)
        {
            audioSource.Stop();
            audioSource.clip = audio;
            audioSource.Play();
        }
    }

    void SwapCamera(CameraName name)
    {
        switch(name)
        {
            case CameraName.Camera_3D:
                Camera_3D.SetActive(true);
                Camera_2D.SetActive(false);
                Camera_direct.SetActive(false);

                actCamera = name;
                break;
            case CameraName.Camera_2D:
                Camera_3D.SetActive(false);
                Camera_2D.SetActive(true);
                Camera_direct.SetActive(false);

                actCamera = name;
                break;
            case CameraName.Directing_Camera:
                Camera_3D.SetActive(false);
                Camera_2D.SetActive(false);
                Camera_direct.SetActive(true);
                break;
        }
    }

    void SwapCamera()
    {
        if(Camera_direct.activeSelf)
            SwapCamera(actCamera);
        else
        {
            if (actCamera == CameraName.Camera_3D)
                SwapCamera(CameraName.Camera_2D);
            else
                SwapCamera(CameraName.Camera_3D);
        }
    }

    public Vector3 GetPlayerFrontCoord(int distance = 0)
    {
        return Player.GetComponent<Lana>().getFrontCoord(distance);
    }

    public Vector3 GetInvaderCoord(EnemyName name)
    {
        return Enemies[(int)name].transform.position;
    }

    public void increaseScore(int score)
    {
        Score += score;
        ScoreBoard.text = Score.ToString();
    }

    public Coroutine GetElixir()
    {
        return StartCoroutine(DirectGetElixir());
    }

    IEnumerator DirectGetElixir()
    {
        timeScale = 0f;

        SwapCamera(CameraName.Directing_Camera);

        audioSource.Stop();

        float limit = DirectingTime_Elixir;
        GameObject Props = GameObject.Find("Directing Screen").transform.Find("Get Item").gameObject;
        GameObject Bg_Effect = Props.transform.Find("get_item_bg").gameObject;
        Props.SetActive(true);
        while(limit > 0f)
        {
            Bg_Effect.transform.Rotate(Vector3.back * 30f * Time.deltaTime);
            limit -= Time.deltaTime;
            yield return null;
        }

        SwapCamera();
        timeScale = defualtTimescale;
        Props.SetActive(false);
        Bg_Effect.transform.rotation = Quaternion.identity;

        ChangeBGM(feverBGM);
        FeverTime = FeverLimit;
    }

    IEnumerator TerminationElixir()
    {
        timeScale = 0f;

        SwapCamera(CameraName.Directing_Camera);

        float limit = DirectingTime_Termination;
        GameObject Props = GameObject.Find("Directing Screen").transform.Find("Time out").gameObject;

        Props.SetActive(true);
        while (limit > 0f)
        {
            limit -= Time.deltaTime;
            yield return null;
        }

        SwapCamera();
        timeScale = defualtTimescale;
        ChangeBGM(mainBGM);
        Props.SetActive(false);
    }
}
