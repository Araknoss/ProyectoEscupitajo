using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private float delayDuration = 1f;
    //[SerializeField] private string MainMenuSceneName = "MainMenu";

    [SerializeField] private Player rewiredPlayer;
    [SerializeField] private int playerId=0;

    [SerializeField] private bool mainMenu = false;

    [SerializeField] private Animator transitionAnimator;

    private void Start()
    {
        rewiredPlayer = ReInput.players.GetPlayer(playerId);
    }
    private void Update()
    {
#if UNITY_EDITOR
        if(rewiredPlayer.GetButtonDown("Restart") && !mainMenu)
        {
            ResetLevel();
        }
#endif
    }
    public void ResetLevel()
    {
        LoadByIndexAfterDelay(SceneManager.GetActiveScene().buildIndex);        
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        LoadByIndexAfterDelay(nextSceneIndex);
    }
    public void LoadByIndexAfterDelay(int sceneIndex)
    {
        //SceneManager.LoadScene(sceneIndex);
        StartCoroutine(LoadSceneAfterDelayCo(sceneIndex));
    }
    private IEnumerator LoadSceneAfterDelayCo(int sceneIndex)
    {
        if(transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Out");
        }              
        yield return new WaitForSecondsRealtime(delayDuration);
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadSceneAfterDelayCo(0));        
    }
}
