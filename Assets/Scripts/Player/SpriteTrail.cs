using System.Collections.Generic;
using UnityEngine;

public class SpriteAfterImageTrail : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer mainSpriteRenderer;
    [SerializeField] private SpriteGhost ghostPrefab;

    [Header("Trail Settings")]
    [SerializeField] private float spawnInterval = 0.08f;
    [SerializeField] private Color ghostColor = new Color(1f, 1f, 1f, 0.6f);
    [SerializeField] private int sortingOrderOffset = -1;
    [SerializeField] private int poolSize = 3;

    private float timer;
    private Queue<SpriteGhost> pool = new Queue<SpriteGhost>();

    [SerializeField] private bool ghostEnabled = false;

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            SpriteGhost ghost = Instantiate(ghostPrefab, gameObject.transform);
            ghost.gameObject.SetActive(false);
            pool.Enqueue(ghost);
        }
    }

    private void Reset()
    {
        mainSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && ghostEnabled)
        {
            timer = 0f;
            SpawnGhost();
        }
    }

    private void SpawnGhost()
    {
        if (mainSpriteRenderer == null || ghostPrefab == null || mainSpriteRenderer.sprite == null)
            return;

        SpriteGhost ghost = pool.Dequeue();

        ghost.transform.SetParent(null);

        ghost.Initialize(
            mainSpriteRenderer.sprite,
            mainSpriteRenderer.transform.position,
            mainSpriteRenderer.transform.rotation,
            mainSpriteRenderer.transform.lossyScale,
            mainSpriteRenderer.flipX,
            mainSpriteRenderer.flipY,
            ghostColor,
            mainSpriteRenderer.sortingOrder + sortingOrderOffset
        );

        pool.Enqueue(ghost);
    }

    public void HandleOnJump(Component sender, object data)
    {
        if(data is bool)
        {
            ghostEnabled= (bool)data;
        }                 
    }
}