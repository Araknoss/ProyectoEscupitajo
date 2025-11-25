using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 direction;

    private void Update()
    {
        gameObject.transform.position += movementSpeed*Time.deltaTime*direction;
    }

    public void SetDirection(Vector3 dir)
    {
        if (dir == Vector3.zero) return;
        direction = dir.normalized;
    }

    public void SetHorizontalDirection(int directionSign)
    {
        if (directionSign == 0) return;
        direction = new Vector3(Mathf.Sign(directionSign), 0f, 0f);
    }

    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
    }
}
