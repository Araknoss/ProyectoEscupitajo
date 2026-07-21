using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string levelName;
    public List<ChunkPooler> chunkPoolers;
    public float chunkTreshold;
    public float chunksPerPooler;

    [Header("Chunks únicos del nivel")]
    [Tooltip("Chunk especial que siempre se spawnea al empezar el nivel")]
    public GameObject firstChunk;

    [Tooltip("Chunk especial que siempre se spawnea justo antes de pasar al siguiente nivel")]
    public GameObject lastChunk;
}
