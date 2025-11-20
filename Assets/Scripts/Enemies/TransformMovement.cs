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

}
