using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DebugTools : MonoBehaviour
{
    public GameEvent onSliderValueChanged;

    [SerializeField] private Slider chunkSpeedSlider;

    public void SetChunkSpeed()
    {
        onSliderValueChanged.Raise(this, chunkSpeedSlider.value);
    }
}
