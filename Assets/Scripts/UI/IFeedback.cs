using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interfaz que obliga a implementar feedback usando FEEL (MoreMountains.Feedbacks).
/// </summary>
public interface IFeedback
{
    /// <summary>
    /// Reproduce los feedbacks asociados.
    /// </summary>
    void PlayFeedback();
}

