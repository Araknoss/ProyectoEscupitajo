using UnityEngine;

public class SpriteGhost : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float lifeTime = 0.3f;

    private float timer;
    private Color initialColor;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Sprite sprite, Vector3 position, Quaternion rotation, Vector3 scale, bool flipX, bool flipY, Color color, int sortingOrder)
    {
        // 🔴 IMPORTANTE: copiar el sprite en este instante
        spriteRenderer.sprite = sprite;

        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;

        spriteRenderer.flipX = flipX;
        spriteRenderer.flipY = flipY;
        spriteRenderer.color = color;
        spriteRenderer.sortingOrder = sortingOrder;

        initialColor = color;
        timer = 0f;

        gameObject.SetActive(true);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float t = timer / lifeTime;
        float alpha = Mathf.Lerp(initialColor.a, 0f, t);

        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;

        if (timer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }
}
