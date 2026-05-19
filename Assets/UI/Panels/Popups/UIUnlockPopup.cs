using MoreMountains.Feedbacks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class UIUnlockPopup : UIPopup
{
    [Header("Bindings")]
    [SerializeField] private TextMeshProUGUI trickNameText;
    [SerializeField] private Image trickImage;

    [Header("Feedback")]
    [SerializeField] private MMF_Player unlockFeedback;

    protected override void OnShow()
    {
        base.OnShow();
        unlockFeedback?.PlayFeedbacks();
    }

    protected override void OnHide()
    {
        base.OnHide();        
    }

    // Llamar para configurar y mostrar el popup desde código
    public void ShowUnlock(Trick trick)
    {
        if (trick == null) return;
        if (trickNameText != null) trickNameText.text = trick.trickName;       
        if (trickImage != null) trickImage.sprite = trick.sprite;
        Show(); // método de UIPanel -> activa el gameObject y llama a OnShow             
    }   

    //// Método para enganchar como listener de GameEvent (GameEventListener -> response)
    //public void HandleOnTrickUnlocked(Component sender, object data)
    //{
    //    if (data is Trick unlockedTrick)
    //    {
    //        ShowUnlock(unlockedTrick);
    //    }
    //}
}
