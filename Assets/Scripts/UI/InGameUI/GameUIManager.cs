using System;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private GameUIAnimationHandler gameUIAnimationHandler;

    public static event Action PauseGame;
    public static event Action OnCountDownFinished;

    private void Awake()
    {
        if (pointsText == null)
        {
            Debug.LogWarning($"Forgot assign {pointsText.name}");
        }
    }

    public void PauseOnClick()
    {
        PauseGame?.Invoke();
    }

    public void SetCurrentPoinsToUIText(int points)
    {
        pointsText.text = points.ToString();
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public void OpenUI(Action OnUiOpened)
    {
        OpenUI();
        OnUiOpened?.Invoke();
    }

    public void StartCountDownAnimation()
    {
        gameUIAnimationHandler.ActivateCountDownAnimation();
    }

    public void OnAnimationEventCountDownFinished()
    {
        OnCountDownFinished?.Invoke();
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void CloseUI(Action OnUIClosed) { 
        CloseUI();
        OnUIClosed?.Invoke();
    }


}
