using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Range(0.0f, 1.0f)]
    public float masterVolume = 1.0f;
    private Bus masterBus;

    [Header("Snapshots")]
    private EventInstance snapshotInstance;
    [SerializeField] private EventReference pauseSnapshot;
    private EventInstance pauseSnapshotInstance;    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }

        masterBus = RuntimeManager.GetBus("bus:/");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetMasterVolume(float volume)
    {
        masterBus.setVolume(volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void PlaySound2D(EventReference eventReference)
    {
        RuntimeManager.PlayOneShot(eventReference);
    }

    public void PlaySound3D(EventReference eventReference, Vector3 position)
    {
        RuntimeManager.PlayOneShot(eventReference, position);
    }

    public EventInstance CreateInstance(EventReference eventReference) //Pasar EventReference, devuelve EventInstance
    {
        return RuntimeManager.CreateInstance(eventReference);
    }

    public void StopSound(EventInstance eventInstance)
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        eventInstance.release();
    }
    public void PauseSound(EventInstance eventInstance, bool setPause)
    {
        eventInstance.setPaused(setPause);
    }

    public void SetGlobalParameter(string name, float value)
    {
        RuntimeManager.StudioSystem.setParameterByName(name, value);
    }

    public void SetParameter(EventInstance eventInstance, string name, float value)
    {
        eventInstance.setParameterByName(name, value);
    }

    public void SetGlobalParametersTo0(string parameter1, string parameter2)
    {
        SetGlobalParameter(parameter1, 0);
        SetGlobalParameter(parameter2, 0);
    }   
    public void PlaySnapshot(EventReference eventReference)
    {
        if (snapshotInstance.isValid())
        {
            StopSound(snapshotInstance);
        }
        snapshotInstance = CreateInstance(eventReference);
        snapshotInstance.start();
    }

    public void StopSnapshot()
    {
        if (snapshotInstance.isValid())
        {
            StopSound(snapshotInstance);
        }
    }

    public void OnGamePause(Component sender, object data)
    {
        if(data is bool isPaused)
        {
            isPaused = (bool)data;
            if (isPaused)
            {
                PlayPauseSnapshot();
            }
            else
            {
                StopPauseSnapshot();
            }
        }
    }
    public void PlayPauseSnapshot()
    {
        pauseSnapshotInstance = CreateInstance(pauseSnapshot);
        pauseSnapshotInstance.start();
    }

    public void StopPauseSnapshot()
    {
        if (pauseSnapshotInstance.isValid())
        {
            StopSound(pauseSnapshotInstance);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopSound(snapshotInstance);
    }
}

