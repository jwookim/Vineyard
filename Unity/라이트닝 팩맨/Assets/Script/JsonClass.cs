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