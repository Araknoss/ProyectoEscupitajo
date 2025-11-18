using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 targetPosition;

    [Header("Components")]
    [SerializeField] private Rigidbody2D body;

    private void Awake()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }
        
        targetPosition = body.position;
    }

    private void Update()
    {
        UpdateMouseTarget();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void UpdateMouseTarget()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;

        targetPosition = mouseWorldPos;
    }

    private void MoveToTarget()
    {        
        Vector2 newPosition = Vector2.MoveTowards(body.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        body.MovePosition(newPosition);
    }
}
