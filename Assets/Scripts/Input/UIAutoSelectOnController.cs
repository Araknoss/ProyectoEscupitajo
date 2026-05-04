using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Rewired;

public class UIAutoSelectOnController : MonoBehaviour
{
    [SerializeField] private GameObject defaultSelected;

    private bool isSubscribed;

    private void OnEnable()
    {
        if (ReInput.isReady && !isSubscribed)
        {
            ReInput.controllers.AddLastActiveControllerChangedDelegate(OnControllerChanged);
            isSubscribed = true;
            if(ReInput.controllers.GetLastActiveController() != null)
            {
                StartCoroutine(SelectNextFrame(ReInput.controllers.GetLastActiveController().type));
            }
        }
    }

    private void OnDisable()
    {
        if (ReInput.isReady && isSubscribed)
        {
            ReInput.controllers.RemoveLastActiveControllerChangedDelegate(OnControllerChanged);
            isSubscribed = false;
        }
    }

    private void OnControllerChanged(Controller controller)
    {
        if (controller == null) return;              
        StartCoroutine(SelectNextFrame(controller.type));
    }

    private IEnumerator SelectNextFrame(ControllerType controllerType)
    {
        yield return null;

        if (defaultSelected == null || EventSystem.current == null) yield break;
        if (!defaultSelected.activeInHierarchy) yield break;

        if (controllerType == ControllerType.Joystick)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(defaultSelected);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        
    }
}
