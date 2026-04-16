using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = targetTransform.position;
    }
}
