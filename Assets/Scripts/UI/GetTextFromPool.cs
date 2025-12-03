using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class GetTextFromPool : MonoBehaviour
{
    [SerializeField] private Pooler _pooler;
   
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float yOffset;
    
    public void SpawnText(Component sender, object data)
    {        
        GameObject pooledObject = _pooler?.GetPooledObject();
        if (pooledObject != null)
        {
            pooledObject.transform.position = spawnPosition.position + new Vector3(0, yOffset, 0);

            TrickScoreTextUI trickScoreTextUI = pooledObject.GetComponentInChildren<TrickScoreTextUI>();
            trickScoreTextUI?.SetTrickScoreText(data);



        }
    }
    
}
