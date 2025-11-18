using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public GameObject[] chunkPrefabs;
    public Transform playerOrCamera;
    public float distanceToSpawn = 10f;

    private Transform currentEnd;

    void Start()
    {
        GameObject firstChunk = Instantiate(chunkPrefabs[0], Vector3.zero, Quaternion.identity, transform);
        currentEnd = firstChunk.transform.Find("NextSpawnPoint");

        SpawnNextChunk();
        SpawnNextChunk();
    }

    void Update()
    {
        if (currentEnd.position.y - playerOrCamera.position.y < distanceToSpawn)
        {
            SpawnNextChunk();
        }
    }

    void SpawnNextChunk()
    {
        GameObject prefab = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

        GameObject chunk = Instantiate(prefab, currentEnd.position, Quaternion.identity, transform);

        currentEnd = chunk.transform.Find("NextSpawnPoint");
    }
}
