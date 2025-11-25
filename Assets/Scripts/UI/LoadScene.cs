using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private float delayDuration = 1f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }
    public void LoadByIndexAfterDelay(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        StartCoroutine(LoadSceneAfterDelayCo(sceneIndex));
    }
    private IEnumerator LoadSceneAfterDelayCo(int sceneIndex)
    {
        yield return new WaitForSeconds(delayDuration);
        SceneManager.LoadScene(sceneIndex);
    }
}
