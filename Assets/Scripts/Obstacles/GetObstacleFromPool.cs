using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObstacleFromPool : MonoBehaviour
{
    [SerializeField] private Pooler _pooler;
    [SerializeField] private Transform initialTransform;
    [SerializeField] private float bulletForce;


    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GameObject pooledObject = _pooler?.GetPooledObject();
            if (pooledObject != null)
            {
                pooledObject.transform.position = initialTransform.position;
                pooledObject.GetComponent<Rigidbody>().AddForce(Vector3.up * bulletForce, ForceMode.Impulse);                
            }            
        }
    }
}
