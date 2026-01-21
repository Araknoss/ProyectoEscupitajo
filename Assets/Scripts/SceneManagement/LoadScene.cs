using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private float delayDuration = 1f;
    [SerializeField] private string MainMenuSceneName = "MainMenu";
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();

        }
    }    
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }
}
