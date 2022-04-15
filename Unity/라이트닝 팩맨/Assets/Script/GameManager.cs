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
    public int bestScore;
    public int Score;
    public int level;
    public int ExtraLife;

    const float defaultTimescale = 1f;
    public float timeScale;

    private Queue<float> timeTable;

    const float DirectingTime_Elixir = 4f;
    const float DirectingTime_Termination = 3f;

    const int defaultKillScore = 200;
    int killScore;

    [SerializeField] GameObject Camera_3D, Camera_2D, Camera_direct;

    CameraName actCamera;

    Coroutine actingCoroutine;

    AudioSource audioSource;
    [SerializeField] AudioClip mainBGM, feverBGM;


    private int mapSizeX;
    public int Max_x { get { return mapSizeX; } }

    private int mapSizeY;
    public int Max_y { get { return mapSizeY; } }
    bool[,] map;
    private List<Tile> Tiles;
    private Stack<Tile> dumpTiles;

    Vector3 StartingPoint;


    [SerializeField] private List<Item> Items;

    [SerializeField] GameObject TilePrefab;
    [SerializeField] GameObject CoinPrefab;
    [SerializeField] GameObject ElixirPrefab;

    public Vector3 ShipPosition
    {
        get
        { 
            return InvaderShip.transform.position;
        }
        private set
        {
            ;
        }
    }
    Text ScoreBoard;
    Text BestScoreBoard;
    GameObject LifeBoard;

    GameObject Curtain;

    [SerializeField]GameObject Player;
    [SerializeField]GameObject[] Enemies;

    [SerializeField] GameObject InvaderShip;

    const float FeverLimit = 10f;
    float FeverTime;

    bool isPlaying;
    float PlayTime;
    MODE standardMode;
    // Start is called before the first frame update

    private void Awake()
    {
        timeTable = new Queue<float>();
        Tiles = new List<Tile>();
        dumpTiles = new Stack<Tile>();

        //Enemies = new GameObject[4];
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ScoreBoard = GameObject.Find("Canvas").transform.Find("ScoreBoard").Find("Score").GetComponent<Text>();
        BestScoreBoard = GameObject.Find("Canvas").transform.Find("BestScore").Find("Score").GetComponent<Text>();
        LifeBoard = GameObject.Find("Canvas").transform.Find("LifeBoard").gameObject;
        Curtain = GameObject.Find("Canvas").transform.Find("Curtain").gameObject;
        FirstSetting();
    }

    // Update is called once per frame
    void Update()
    {
        TimeProgress();
    }


    void TimeProgress()
    {
        if (isPlaying)
        {
            TimeTableCheck();
            FeverCheck();
            CameraMove();
            InputKey();
        }
    }

    private void FirstSetting()
    {
        UpdateMap();
        actCamera = CameraName.Camera_3D;
        level = 1;
        Score = 0;
        ExtraLife = 3;

        if (File.Exists(Application.streamingAssetsPath + "/bestscore.json"))
        {
            string json = File.ReadAllText(Application.streamingAssetsPath + "/bestscore.json");
            bestScore = int.Parse(json);
            BestScoreBoard.text = json;
        }

        PartialInit();
    }



    private void StageInit()
    {
        PartialInit();
        foreach(var item in Items)
        {
            item.gameObject.SetActive(true);
        }
    }
    private void PartialInit()
    {
        timeScale = 0f;

        checkLevelTable();

        ChangeBGM(mainBGM);
        audioSource.Play();

        actingCoroutine = null;

        PlayTime = 0f;
        standardMode = MODE.SCATTER;
        FeverTime = 0f;

        Player.GetComponent<Lana>().Initialization();
        Player.transform.position = StartingPoint;
        CameraMove();

        comebackInvader();

        UpdateLifeBoard();

        Curtain.SetActive(false);


        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        GameObject text = GameObject.Find("Canvas").transform.Find("Ready").gameObject;

        text.GetComponent<Text>().text = "Ready";
        text.SetActive(true);
        yield return new WaitForSeconds(1f);
        text.GetComponent<Text>().text = "Start!";
        yield return new WaitForSeconds(0.5f);

        text.SetActive(false);

        timeScale = defaultTimescale;
        isPlaying = true;
    }

    void checkLevelTable()
    {
        timeTable.Clear();

        string json = File.ReadAllText(Application.dataPath + "/leveldata.json");

        LevelData levelData = JsonUtility.FromJson<LevelData>(json);

        for (int i = 0; i < levelData.data.Length; i++)
        {
            if (levelData.data[i].level < level)
                continue;
            else
            {
                foreach (var tb in levelData.data[i].table)
                    timeTable.Enqueue(tb);
                break;
            }
        }
    }
    void UpdateMap()
    {
        mapSizeX = 28;
        mapSizeY = 31;
        map = new bool[mapSizeY, mapSizeX];

        float backgroundSize = Camera_2D.GetComponent<Camera>().orthographicSize;
        Camera_2D.GetComponent<Camera>().orthographicSize = (float)mapSizeY / 2f;
        Camera_2D.transform.position = new Vector3((float)mapSizeX / 2f, ((float)mapSizeY - 1f) / 2f, Camera_2D.transform.position.z);
        Camera_2D.transform.GetChild(0).localScale *= Camera_2D.GetComponent<Camera>().orthographicSize / backgroundSize;

        StartingPoint = new Vector3(1f, 3f, -0.5f);

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

        InvaderShip.transform.position = new Vector3(Mathf.Ceil(mapSizeX / 2f), Mathf.Ceil(mapSizeY / 2f), ShipPosition.z);

        for (int y = (int)ShipPosition.y - 1; y <= (int)ShipPosition.y + 1; y++)
        {
            for (int x = (int)ShipPosition.x - 1; x <= (int)ShipPosition.x + 1; x++)
            {
                if ((y == (int)ShipPosition.y + 1 || y == (int)ShipPosition.y) && x == (int)ShipPosition.x)
                    continue;

                map[y, x] = true;
            }
        }


        GameObject coin = Instantiate(CoinPrefab);
        coin.transform.position = new Vector3(5f, 5f, coin.transform.position.z);
        Items.Add(coin.GetComponent<Item>());
        coin = Instantiate(CoinPrefab);
        coin.transform.position = new Vector3(16f, 15f, coin.transform.position.z);
        Items.Add(coin.GetComponent<Item>());

        GameObject elixir = Instantiate(ElixirPrefab);
        elixir.transform.position = new Vector3(15f, 15f, coin.transform.position.z);
        Items.Add(elixir.GetComponent<Item>());
        elixir = Instantiate(ElixirPrefab);
        elixir.transform.position = new Vector3(6f, 5f, coin.transform.position.z);
        Items.Add(elixir.GetComponent<Item>());

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
        else if (x > mapSizeX - 1)
            x -= mapSizeX;

        if (y < 0)
            y += mapSizeY;
        else if (y > mapSizeY - 1)
            y -= mapSizeY;


        return map[y, x];
    }

    public bool mapCheck(Vector3 vector)
    {
        int x = Mathf.RoundToInt(vector.x);
        int y = Mathf.RoundToInt(vector.y);

        if (x < 0)
            x += mapSizeX;
        else if (x > mapSizeX - 1)
            x -= mapSizeX;

        if (y < 0)
            y += mapSizeY;
        else if (y > mapSizeY - 1)
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

                actingCoroutine = StartCoroutine(TerminationElixir());
            }
        }
    }

    void TimeTableCheck()
    {
        if (timeTable.Count <= 0f)
            return;

        PlayTime += Time.deltaTime * timeScale;

        if(PlayTime >= timeTable.Peek())
        {
            timeTable.Dequeue();
            ModeSwap();
            PlayTime = 0f;
        }
    }

    void ModeSwap()
    {
        if (standardMode == MODE.CHASE)
            standardMode = MODE.SCATTER;
        else if (standardMode == MODE.SCATTER)
            standardMode = MODE.CHASE;

        foreach(var enemy in Enemies)
            enemy.GetComponent<Enemy>().ChangeOrder(standardMode);
    }

    void InputKey()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !Camera_direct.activeSelf)
        {
            SwapCamera();
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

    void CameraMove()
    {
        if (actCamera == CameraName.Camera_3D)
            Camera_3D.transform.position = Player.transform.position + new Vector3(0f, -5f, -4.5f);
    }

    void SwapCamera(CameraName name)
    {
        switch(name)
        {
            case CameraName.Camera_3D:
                Camera_3D.SetActive(true);
                Camera_2D.SetActive(false);
                if (!Camera_direct.activeSelf)
                    Camera_3D.GetComponent<AudioSource>().Play();
                Camera_direct.SetActive(false);
                actCamera = name;

                Player.GetComponent<SpriteObj>().toProspective();
                foreach (var obj in Enemies)
                {
                    obj.GetComponent<SpriteObj>().toProspective();
                }
                foreach (var obj in Items)
                {
                    obj.GetComponent<SpriteObj>().toProspective();
                }

                break;
            case CameraName.Camera_2D:
                Camera_3D.SetActive(false);
                Camera_2D.SetActive(true);
                if (!Camera_direct.activeSelf)
                    Camera_2D.GetComponent<AudioSource>().Play();
                Camera_direct.SetActive(false);
                actCamera = name;

                Player.GetComponent<SpriteObj>().toOrthograghpic();
                foreach (var obj in Enemies)
                {
                    obj.GetComponent<SpriteObj>().toOrthograghpic();
                }
                foreach (var obj in Items)
                {
                    obj.GetComponent<SpriteObj>().toOrthograghpic();
                }
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

    void comebackInvader()
    {
        InvaderShip.GetComponent<InvaderShip>().Clear();

        foreach (var invader in Enemies)
        {
            InvaderShip.GetComponent<InvaderShip>().StorageInvader(invader);
        }
    }

    bool ClearCheck()
    {
        foreach (var item in Items)
        {
            if (item.activate)
                return false;
        }

        return true;
    }

    public void increaseScore(int score)
    {
        int prevRatio = Score / 10000;
        Score += score;
        ScoreBoard.text = Score.ToString();
        if (Score / 10000 > prevRatio)
            IncreaseLife();

        if (Score > bestScore)
        {
            bestScore = Score;
            BestScoreBoard.text = bestScore.ToString();
        }

        if (ClearCheck())
            StartCoroutine(StageClear());
    }

    public void killVillain()
    {
        increaseScore(killScore);
        killScore *= 2;
    }

    public void HitLana()
    {
        Player.gameObject.GetComponent<Lana>().Collapse();
        timeScale = 0f;

        StartCoroutine(StageFailed());
    }

    public MODE getMode()
    {
        return standardMode;
    }

    public Coroutine GetElixir()
    {
        if (ClearCheck())
            return null;

        killScore = defaultKillScore;

        foreach(var villain in Enemies)
        {
            villain.GetComponent<Enemy>().Suprise();
        }

        if(FeverTime > 0f)
        {
            FeverTime = FeverLimit;
            return null;
        }
        return actingCoroutine = StartCoroutine(DirectGetElixir());
    }

    void CancelDirection()
    {
        if (actingCoroutine == null)
            return;

        StopCoroutine(actingCoroutine);

        if (actCamera == CameraName.Directing_Camera)
            SwapCamera();

        GameObject.Find("Directing Screen").transform.Find("Get Item").gameObject.SetActive(false);
        GameObject.Find("Directing Screen").transform.Find("Time out").gameObject.SetActive(false);
    }

    IEnumerator DirectGetElixir()
    {
        timeScale = 0f;

        SwapCamera(CameraName.Directing_Camera);

        audioSource.Stop();

        float limit = DirectingTime_Elixir;
        GameObject Props = GameObject.Find("Directing Screen").transform.Find("Get Item").gameObject;
        GameObject Bg_Effect = Props.transform.Find("get_item_bg").gameObject;
        Bg_Effect.transform.rotation = Quaternion.identity;
        Props.SetActive(true);
        while(limit > 0f)
        {
            Bg_Effect.transform.Rotate(Vector3.back * 30f * Time.deltaTime);
            limit -= Time.deltaTime;
            yield return null;
        }

        Player.GetComponent<Lana>().DrinkElixir();
        SwapCamera();
        timeScale = defaultTimescale;
        Props.SetActive(false);

        ChangeBGM(feverBGM);
        FeverTime = FeverLimit;

        actingCoroutine = null;
    }

    IEnumerator TerminationElixir()
    {
        timeScale = 0f;

        SwapCamera(CameraName.Directing_Camera);
        ChangeBGM(null);

        float limit = DirectingTime_Termination;
        GameObject Props = GameObject.Find("Directing Screen").transform.Find("Time out").gameObject;

        Props.SetActive(true);
        while (limit > 0f)
        {
            limit -= Time.deltaTime;
            yield return null;
        }

        foreach (var villain in Enemies)
        {
            villain.GetComponent<Enemy>().CalmDown();
        }

        Player.GetComponent<Lana>().RemoveElixir();
        SwapCamera();
        timeScale = defaultTimescale;
        ChangeBGM(mainBGM);
        Props.SetActive(false);

        actingCoroutine = null;
    }



    IEnumerator StageClear()
    {
        timeScale = 0f;

        Player.GetComponent<Lana>().Victory();

        CancelDirection();
        yield return StartCoroutine(DrawCurtain());

        if (level < 100)
        {
            level++;

            StageInit();
        }
        else
            GameOver();
    }

    IEnumerator StageFailed()
    {
        DecreaseLife();

        timeScale = 0f;

        CancelDirection();
        yield return StartCoroutine(DrawCurtain());

        if (ExtraLife >= 0)
            PartialInit();
        else
            GameOver();
    }

    void IncreaseLife()
    {
        if (ExtraLife < 5)
            ExtraLife++;

        UpdateLifeBoard();
    }

    void DecreaseLife()
    {
        ExtraLife--;

        UpdateLifeBoard();
    }


    void UpdateLifeBoard()
    {
        GameObject tmp;
        for (int i = 0; i < 5; i++)
        {
            tmp = LifeBoard.transform.GetChild(i).gameObject;

            if (ExtraLife > i)
                tmp.SetActive(true);
            else
                tmp.SetActive(false);
        }
    }


    void GameOver()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/bestscore.json", bestScore.ToString());
    }


    IEnumerator DrawCurtain()
    {
        yield return new WaitForSeconds(2f);

        audioSource.Stop();

        Curtain.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        Curtain.SetActive(true);
        while (Curtain.GetComponent<Image>().color.a < 1)
        {
            Curtain.GetComponent<Image>().color += new Color(0f, 0f, 0f, 1f) * Time.deltaTime;
            yield return null;
        }


        yield return new WaitForSeconds(2f);
    }


}
