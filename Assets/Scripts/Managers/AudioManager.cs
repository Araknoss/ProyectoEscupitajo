using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Buses")]
    [SerializeField] private string _masterBusPath = "bus:/";
    [SerializeField] private string _musicBusPath = "bus:/Music";
    [SerializeField] private string _sfxBusPath = "bus:/SFX";

    private Bus _masterBus;
    private Bus _musicBus;
    private Bus _sfxBus;

    [Header("Snapshots")]
    private EventInstance snapshotInstance;
    [SerializeField] private EventReference pauseSnapshot;
    private EventInstance pauseSnapshotInstance;

    [Header("EventsReferences")]
    [SerializeField] private EventReference UIhoverSound;
    [SerializeField] private EventReference UIpressSound;
    [SerializeField] private EventReference UIScoreToCoinSound;

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

        _masterBus = RuntimeManager.GetBus(_masterBusPath);
        _musicBus = RuntimeManager.GetBus(_musicBusPath);
        _sfxBus = RuntimeManager.GetBus(_sfxBusPath);
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
    public void PlayHoverSound()
    {
        PlaySound2D(UIhoverSound);
    }

    public void PlayButtonPressSound()
    {
        PlaySound2D(UIpressSound);
    }

    public void PlayScoreToCoinSound()
    {
        //PlaySound2D(UIScoreToCoinSound);
    }



    //SETTINGS 

    public void HandleOnMasterVolumeChanged(Component sender, object data)
    {
        float value = (float)data;

        _masterBus.setVolume(value);
    }

    public void HandleOnMusicVolumeChanged(Component sender, object data)
    {
        float value = (float)data;

        _musicBus.setVolume(value);
    }

    public void HandleOnSFXVolumeChanged(Component sender, object data)
    {
        float value = (float)data;

        _sfxBus.setVolume(value);
    }

    public void HandleOnMuteChanged(Component sender, object data)
    {
        Debug.Log(data);
        Debug.Log(data.GetType());

        bool muted = (bool)data;

        FMODUnity.RuntimeManager.MuteAllEvents(muted);
    }
}

