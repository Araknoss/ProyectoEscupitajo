using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterDelay : MonoBehaviour
{
    [SerializeField] private float delay = 10f;
    void OnEnable()
    {
        StartCoroutine(DisableAfterDelayCo());
    }

    IEnumerator DisableAfterDelayCo()
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
