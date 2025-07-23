using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopBarUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void UpdateScoreTextUI(int scores)
    {
        scoreText.text = scores.ToString();
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void OpenUI(Action OnUIOpened)
    {
        gameObject.SetActive(true);

        OnUIOpened?.Invoke();
    }

    public void CloseUI(Action OnUIClosed)
    {
        gameObject.SetActive(false);
        OnUIClosed?.Invoke();
    }

}
