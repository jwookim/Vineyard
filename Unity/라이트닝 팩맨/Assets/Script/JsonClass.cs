using System.Collections.Generic;

[System.Serializable]
class LevelData
{
    public LevelTable[] data;
}

[System.Serializable]
class LevelTable
{
    public int level;
    public float[] table;
}


[System.Serializable]
class CoinsRemaining
{
    public int level;
    public int count;
}



[System.Serializable]
class LevelData2
{
    public int[] data;
}
///
///
///

[System.Serializable]
class MapData
{
    public int x;
    public int y;
    public jsonVector playerPos;
    public jsonVector shipPos;

    public List<ObjectData> obstacles = new List<ObjectData>();
    public List<ObjectData> objects = new List<ObjectData>();
}

[System.Serializable] 
class ObjectData
{
    public string name;
    public jsonVector position;
    public jsonVector rotation;
    public jsonVector scale;
}

[System.Serializable]
class jsonVector
{
    public float x, y, z;

    public jsonVector(float f_x, float f_y, float f_z)
    {
        x = f_x;
        y = f_y;
        z = f_z;
    }
}
