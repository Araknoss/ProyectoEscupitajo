using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;

    void Update()
    {
        transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
    }
}
