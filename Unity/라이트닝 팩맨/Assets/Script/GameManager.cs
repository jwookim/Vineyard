using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

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
    const int maxLevel = 20;

    public int bestScore;
    public int Score;
    public int level;
    public int ExtraLife;

    const float defaultTimescale = 1f;
    public float timeScale;

    bool escActivate;

    private Queue<float> timeTable;
    [SerializeField] private int CoinLimit;

    const float DirectingTime_Elixir = 4f;
    const float DirectingTime_Termination = 3f;

    const int defaultKillScore = 200;
    int killScore;

    [SerializeField] GameObject Camera_3D, Camera_2D, Camera_direct;

    CameraName actCamera;

    Coroutine actingCoroutine;

    AudioSource audioSource;
    [SerializeField] AudioClip[] mainBGM;
    [SerializeField] AudioClip feverBGM;

    int BGM_Num;


    private int mapSizeX;
    public int Max_x { get { return mapSizeX; } }

    private int mapSizeY;
    public int Max_y { get { return mapSizeY; } }
    bool[,] map;
    private List<Tile> Tiles;
    private Stack<Tile> dumpTiles;

    Vector3 StartingPoint;


    private List<Item> Items;
    public int wholeCoin;
    int coinCount;


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

    GameObject ElixirGuage;

    [SerializeField]GameObject Player;
    [SerializeField]GameObject[] Enemies;

    GameObject InvaderShip;

    const float FeverLimit = 10f;
    float currentLimit;
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
        Items = new List<Item>();
    }
    void Start()
    {
        Player.SetActive(false);
        foreach (var enemy in Enemies)
            enemy.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        ScoreBoard = GameObject.Find("Canvas").transform.Find("Interface").Find("ScoreBoard").Find("Score").GetComponent<Text>();
        BestScoreBoard = GameObject.Find("Canvas").transform.Find("Interface").Find("BestScore").Find("Score").GetComponent<Text>();
        LifeBoard = GameObject.Find("Canvas").transform.Find("Interface").Find("LifeBoard").gameObject;
        Curtain = GameObject.Find("Canvas").transform.Find("Interface").Find("Curtain").gameObject;
        ElixirGuage = GameObject.Find("Canvas").transform.Find("Interface").Find("Guage").gameObject;
        StartCoroutine(FirstSetting());
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

    private IEnumerator FirstSetting()
    {
        escActivate = false;
        wholeCoin = 0;
        Curtain.SetActive(true);
        UpdateMap();
        yield return new WaitForSeconds(0.5f);
        UpdateSprite();
        actCamera = CameraName.Camera_3D;
        BGM_Num = 0;
        level = 1;
        Score = 0;
        ExtraLife = 2;

        Player.SetActive(true);

        if (File.Exists(Application.streamingAssetsPath + "/bestscore.json"))
        {
            string json = File.ReadAllText(Application.streamingAssetsPath + "/bestscore.json");
            bestScore = int.Parse(json);
            BestScoreBoard.text = json;
        }

        StartCoroutine(StageInit());
    }



    private IEnumerator StageInit()
    {
        BGM_Num = (level - 1) / (maxLevel / 4);
        coinCount = 0;
        Enemies[(int)EnemyName.Invader_soldier].GetComponent<Invader_soldier>().Discharge();

        PartialInit();

        yield return null;

        foreach(var item in Items)
        {
            item.gameObject.SetActive(true);
        }
    }
    private void PartialInit()
    {
        timeScale = 0f;
        isPlaying = false;

        checkLevelTable();

        actingCoroutine = null;

        GameObject.Find("Canvas").transform.Find("Interface").Find("StageBoard").Find("Stage").GetComponent<Text>().text = level.ToString();

        ElixirGuage.SetActive(false);

        PlayTime = 0f;
        standardMode = MODE.SCATTER;
        FeverTime = 0f;
        currentLimit = Mathf.Max(FeverLimit - ((level - 1) / (float)maxLevel * FeverLimit), 1f);

        audioSource.pitch = 1f + (coinCount / (wholeCoin / 6) * 0.2f);

        Player.transform.position = StartingPoint;
        Player.GetComponent<Lana>().Initialization();
        CameraMove();

        comebackInvader();

        UpdateLifeBoard();

        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {

        Curtain.SetActive(false);
        GameObject text = GameObject.Find("Canvas").transform.Find("Interface").Find("Ready").gameObject;

        text.GetComponent<Text>().text = "Ready";
        text.SetActive(true);
        transform.Find("SoundEffect").GetComponent<SoundEffectManager>().ReadySign();
        yield return new WaitForSeconds(1f);
        text.GetComponent<Text>().text = "Start!";
        transform.Find("SoundEffect").GetComponent<SoundEffectManager>().StartSign();
        yield return new WaitForSeconds(0.5f);

        ChangeBGM(mainBGM[BGM_Num]);
        audioSource.Play();

        text.SetActive(false);

        timeScale = defaultTimescale;
        isPlaying = true;
    }

    void checkLevelTable()
    {
        timeTable.Clear();

        string json = File.ReadAllText(Application.streamingAssetsPath + "/leveldata.json");

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

        json = File.ReadAllText(Application.streamingAssetsPath + "/leveldata2.json");

        LevelData2 levelData2 = JsonUtility.FromJson<LevelData2>(json);

        CoinLimit = levelData2.data[level - 1];
    }
    void UpdateMap()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/default_mapData.json");

        MapData mapData = JsonUtility.FromJson<MapData>(json);



        mapSizeX = mapData.x;
        mapSizeY = mapData.y;
        map = new bool[mapSizeY, mapSizeX];

        float backgroundSize = Camera_2D.GetComponent<Camera>().orthographicSize;
        Camera_2D.GetComponent<Camera>().orthographicSize = mapSizeY / 2f;
        Camera_2D.transform.position = new Vector3(mapSizeX / 2f, (mapSizeY - 1f) / 2f, Camera_2D.transform.position.z);
        Camera_2D.transform.GetChild(0).localScale *= Camera_2D.GetComponent<Camera>().orthographicSize / backgroundSize;

        StartingPoint = new Vector3(mapData.playerPos.x, mapData.playerPos.y, mapData.playerPos.z);

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
        foreach(var obj in mapData.obstacles)
        {
            tmp = Instantiate(Resources.Load<GameObject>("Prefab/Obstacle/" + obj.name));

            tmp.transform.position = new Vector3(obj.position.x, obj.position.y, obj.position.z);
            tmp.transform.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, obj.rotation.z);
            tmp.transform.localScale = new Vector3(obj.scale.x, obj.scale.y, obj.scale.z);

            tmp.transform.SetParent(GameObject.Find("Map").transform.Find("Obstacles"));
        }


        foreach (var obj in mapData.objects)
        {
            tmp = Instantiate(Resources.Load<GameObject>("Prefab/" + obj.name));

            tmp.transform.position = new Vector3(obj.position.x, obj.position.y, obj.position.z);
            tmp.transform.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, obj.rotation.z);
            tmp.transform.localScale = new Vector3(obj.scale.x, obj.scale.y, obj.scale.z);

            tmp.transform.SetParent(GameObject.Find("Map").transform.Find("Objects"));

            if (obj.name == "Coin")
                wholeCoin++;

            if (obj.name == "Coin" || obj.name == "Elixir")
                Items.Add(tmp.GetComponent<Item>());
        }


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
                    tmp = Instantiate(Resources.Load<GameObject>("Prefab/Tile"));
                    tmp.transform.parent = GameObject.Find("Map").transform.Find("Tiles");
                }

                tmp.transform.position = new Vector3(x, y, 0f);
                Tiles.Add(tmp.GetComponent<Tile>());
            }
        }
        InvaderShip = GameObject.Find("Map").transform.Find("Objects").Find("Invader Ship").gameObject;
        InvaderShip.transform.position = new Vector3(mapData.shipPos.x, mapData.shipPos.y, mapData.shipPos.z);

    }

    private void UpdateSprite()
    {
        foreach(var tile in Tiles)
        {
            if (tile.transform.position.x == 0 || tile.transform.position.x == mapSizeX - 1 || tile.transform.position.y == 0 || tile.transform.position.y == mapSizeY - 1)
                map[(int)tile.transform.position.y, (int)tile.transform.position.x] = !tile.PassCheck();
            else
                map[(int)tile.transform.position.y, (int)tile.transform.position.x] = tile.WallCheck();
        }

        for (int y = (int)ShipPosition.y - 2; y <= (int)ShipPosition.y + 2; y++)
        {
            for (int x = (int)(ShipPosition.x - 3.5f); x <= (int)(ShipPosition.x + 3.5f); x++)
            {
                if ((y == (int)ShipPosition.y + 2f || y == (int)ShipPosition.y + 1f || y == (int)ShipPosition.y) && x == (int)(ShipPosition.x + 0.5f) /*|| (x == (int)(ShipPosition.x - 0.5f) && y == (int)ShipPosition.y + 2f)*/)
                    continue;

                map[y, x] = true;
            }
        }



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
            ElixirGuage.transform.Find("Bar").GetComponent<Image>().fillAmount = FeverTime / currentLimit;

            if (FeverTime <= 0f)
            {
                FeverTime = 0f;


                if (actingCoroutine != null)
                    CancelDirection();
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
            transform.Find("SoundEffect").GetComponent<SoundEffectManager>().SwapCamera();
        }


        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escActivate)
                StartCoroutine(EscapeGame());
            else
                SceneManager.LoadScene("MenuScene");
        }
    }

    void ChangeBGM(AudioClip audio)
    {
        if(audioSource.clip != audio)
        {
            audioSource.Stop();
            audioSource.clip = audio;
            audioSource.Play();

            if (audio == feverBGM)
                audioSource.pitch = 1f;
            else
                audioSource.pitch = 1f + (coinCount / (wholeCoin / 6) * 0.2f);
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
        int prevRatio;

        if (score == 10)
        {
            prevRatio = coinCount / (wholeCoin / 6);

            coinCount++;

            if (coinCount / (wholeCoin / 6) != prevRatio && FeverTime == 0f)
            {
                audioSource.Pause();
                audioSource.pitch += 0.2f;
                audioSource.UnPause();
            }

            if (wholeCoin - coinCount == CoinLimit)
                Enemies[(int)EnemyName.Invader_soldier].GetComponent<Invader_soldier>().EmergencyCall();
        }


        prevRatio = Score / 10000;
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
        if (!isPlaying)
            return;

        Player.gameObject.GetComponent<Lana>().Collapse();

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
            FeverTime = currentLimit;
            return null;
        }
        if (actingCoroutine != null)
            CancelDirection();
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
        isPlaying = false;

        SwapCamera(CameraName.Directing_Camera);

        audioSource.Stop();
        transform.Find("SoundEffect").GetComponent<SoundEffectManager>().FeverMusic();
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
        isPlaying = true;
        Props.SetActive(false);

        ChangeBGM(feverBGM);
        FeverTime = currentLimit;
        ElixirGuage.SetActive(true);

        actingCoroutine = null;
    }

    IEnumerator TerminationElixir()
    {
        timeScale = 0f;
        isPlaying = false;
        ElixirGuage.SetActive(false);

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
        isPlaying = true;
        ChangeBGM(mainBGM[BGM_Num]);
        Props.SetActive(false);

        actingCoroutine = null;
    }



    IEnumerator StageClear()
    {
        timeScale = 0f;
        isPlaying = false;

        audioSource.Stop();
        Player.GetComponent<Lana>().Victory();
        transform.Find("SoundEffect").GetComponent<SoundEffectManager>().ClearSign();

        CancelDirection();
        yield return StartCoroutine(DrawCurtain());

        if (level < 20)
        {
            if (level % 5 == 0)
            {
                SwapCamera(CameraName.Directing_Camera);
                GameObject Interface = GameObject.Find("Canvas").transform.Find("Interface").gameObject;
                Interface.SetActive(false);
                SceneManager.LoadScene("EventScene" + level / 5, LoadSceneMode.Additive);
                
                while (SceneManager.sceneCount > 1)
                    yield return null;
                Interface.SetActive(true);
                SwapCamera();
            }

            level++;

            StartCoroutine(StageInit());
        }
        else
            GameClear();
    }

    IEnumerator StageFailed()
    {
        DecreaseLife();

        timeScale = 0f;
        isPlaying = false;

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

        SceneManager.LoadScene("FailScene");
    }

    void GameClear()
    {
        File.WriteAllText(Application.streamingAssetsPath + "/bestscore.json", bestScore.ToString());

        SceneManager.LoadScene("ClearScene");
    }


    IEnumerator DrawCurtain()
    {
        yield return new WaitForSeconds(2f);

        audioSource.Stop();

        Curtain.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        Curtain.SetActive(true);
        transform.Find("SoundEffect").GetComponent<SoundEffectManager>().DrawCurtain();
        while (Curtain.GetComponent<Image>().color.a < 1)
        {
            Curtain.GetComponent<Image>().color += new Color(0f, 0f, 0f, 1f) * Time.deltaTime;
            yield return null;
        }


        yield return new WaitForSeconds(2f);
    }


    IEnumerator EscapeGame()
    {
        escActivate = true;
        GameObject EscapeInfo = GameObject.Find("Canvas").transform.Find("Escape").gameObject;
        EscapeInfo.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        while (EscapeInfo.GetComponent<RectTransform>().sizeDelta.x < 1100f)
        {
            EscapeInfo.GetComponent<RectTransform>().sizeDelta += new Vector2(1500f, 0f) * Time.deltaTime;
            yield return null;
        }

        EscapeInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(1100f, EscapeInfo.GetComponent<RectTransform>().sizeDelta.y);

        EscapeInfo.transform.Find("Text").gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        EscapeInfo.transform.Find("Text").gameObject.SetActive(false);
        while (EscapeInfo.GetComponent<RectTransform>().sizeDelta.x > 100f)
        {
            EscapeInfo.GetComponent<RectTransform>().sizeDelta -= new Vector2(1500f, 0f) * Time.deltaTime;
            yield return null;
        }
        EscapeInfo.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, EscapeInfo.GetComponent<RectTransform>().sizeDelta.y);
        yield return new WaitForSeconds(0.2f);
        EscapeInfo.SetActive(false);
        escActivate = false;
    }
}
