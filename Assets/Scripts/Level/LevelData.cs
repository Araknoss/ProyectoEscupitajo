using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public List<ChunkPooler> chunkPoolers;
    public float chunkTreshold;
    public float chunksPerPooler;
}
