using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBlockFirstSubmit : MonoBehaviour
{
    [SerializeField] private Selectable[] selectables;
    [SerializeField] private float unblockDelay = 0.15f;

    private void OnEnable()
    {
        StartCoroutine(BlockTemporarily());
    }

    private IEnumerator BlockTemporarily()
    {
        SetInteractable(false);
        yield return new WaitForSecondsRealtime(unblockDelay);
        SetInteractable(true);
    }

    private void SetInteractable(bool value)
    {
        for (int i = 0; i < selectables.Length; i++)
        {
            if (selectables[i] != null)
                selectables[i].interactable = value;
        }
    }
}
