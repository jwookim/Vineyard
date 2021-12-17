using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Tile : MonoBehaviour
{
    SpriteRenderer UL, U, UR, L, C, R, DL, D, DR;
    Dictionary<string, Sprite> sprites;
    // Start is called before the first frame update
    void Awake()
    {
        UL = transform.Find("UL").GetComponent<SpriteRenderer>();
        U = transform.Find("U").GetComponent<SpriteRenderer>();
        UR = transform.Find("UR").GetComponent<SpriteRenderer>();
        L = transform.Find("L").GetComponent<SpriteRenderer>();
        C = transform.Find("C").GetComponent<SpriteRenderer>();
        R = transform.Find("R").GetComponent<SpriteRenderer>();
        DL = transform.Find("DL").GetComponent<SpriteRenderer>();
        D = transform.Find("D").GetComponent<SpriteRenderer>();
        DR = transform.Find("DR").GetComponent<SpriteRenderer>();

        sprites = new Dictionary<string, Sprite>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDisable()
    {
        transform.position = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        // Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절판 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
        bool isHit = Physics.BoxCast(transform.position + Vector3.forward * 0.5f, transform.lossyScale / 4f, Vector3.back, out hit, Quaternion.identity, 1f, LayerMask.GetMask("Wall"));

        Gizmos.color = Color.red;
        if (isHit)
        {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale/2f);
        }
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * 1f);
        }
    }

    public bool WallCheck()
    {
        return Physics.BoxCast(transform.position + Vector3.forward*0.5f, transform.lossyScale / 4f, Vector3.back, Quaternion.identity, 1f, LayerMask.GetMask("Wall"));
    }

    public bool PassCheck()
    {
        return Physics.BoxCast(transform.position + Vector3.forward * 0.5f, transform.lossyScale / 4f, Vector3.back, Quaternion.identity, 1f, LayerMask.GetMask("Pass"));
    }

    Sprite searchSprite(string name)
    {
        if (sprites.ContainsKey(name))
            return sprites[name];


        string path = Application.dataPath + "/resource/Texture2D/tile/";
        byte[] byteTexture;
        Texture2D texture = new Texture2D(0, 0);
        Sprite sprite;

        try
        {
            byteTexture = File.ReadAllBytes(path + name + ".png");
        }
        catch
        { 
            byteTexture = null;
        }
        texture.LoadImage(byteTexture);
        sprite = Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width, texture.height)), new Vector2(0.5f, 0.5f));

        sprites.Add(name, sprite);

        return sprite;
    }
    public void UpdateSprite()
    {
        Sprite baseSprite;
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        if (GameManager.Instance.mapCheck(x, y))
        {
            baseSprite = searchSprite("tile_grassA");

            UL.sprite = baseSprite;
            U.sprite = baseSprite;
            UR.sprite = baseSprite;
            L.sprite = baseSprite;
            C.sprite = baseSprite;
            R.sprite = baseSprite;
            DL.sprite = baseSprite;
            D.sprite = baseSprite;
            DR.sprite = baseSprite;
            return;
        }


        baseSprite = searchSprite("tile_dirtA");
        C.GetComponent<SpriteRenderer>().sprite = baseSprite;

        bool up = GameManager.Instance.mapCheck(x, y + 1);
        bool down = GameManager.Instance.mapCheck(x, y - 1);
        bool left = GameManager.Instance.mapCheck(x - 1, y);
        bool right = GameManager.Instance.mapCheck(x + 1, y);

        if (up)
        {
            U.sprite = searchSprite("tile_grassU");

            if (left)
                UL.sprite = searchSprite("tile_dirtDR");
            else
                UL.sprite = searchSprite("tile_grassU");

            if (right)
                UR.sprite = searchSprite("tile_dirtDL");
            else
                UR.sprite = searchSprite("tile_grassU");
        }
        else
        {
            U.sprite = baseSprite;

            if (left)
                UL.sprite = searchSprite("tile_grassL");
            else if (GameManager.Instance.mapCheck(x - 1, y + 1))
                UL.sprite = searchSprite("tile_grassUL");
            else
                UL.sprite = baseSprite;

            if (right)
                UR.sprite = searchSprite("tile_grassR");
            else if (GameManager.Instance.mapCheck(x + 1, y + 1))
                UR.sprite = searchSprite("tile_grassUR");
            else
                UR.sprite = baseSprite;
        }

        if (left)
            L.sprite = searchSprite("tile_grassL");
        else
            L.sprite = baseSprite;

        if (right)
            R.sprite = searchSprite("tile_grassR");
        else
            R.sprite = baseSprite;

        if(down)
        {
            D.sprite = searchSprite("tile_grassD");

            if (left)
                DL.sprite = searchSprite("tile_dirtUR");
            else
                DL.sprite = searchSprite("tile_grassD");

            if (right)
                DR.sprite = searchSprite("tile_dirtUL");
            else
                DR.sprite = searchSprite("tile_grassD");
        }
        else
        {
            D.sprite = baseSprite;

            if (left)
                DL.sprite = searchSprite("tile_grassL");
            else if (GameManager.Instance.mapCheck(x - 1, y - 1))
                DL.sprite = searchSprite("tile_grassDL");
            else
                DL.sprite = baseSprite;

            if (right)
                DR.sprite = searchSprite("tile_grassR");
            else if (GameManager.Instance.mapCheck(x + 1, y - 1))
                DR.sprite = searchSprite("tile_grassDR");
            else
                DR.sprite = baseSprite;
        }
    }
}
