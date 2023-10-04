using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : ResultUI
{
    public TextMeshProUGUI score;
    public Button scoreButton;

    protected override void Start()
    {
        base.Start();
        if(scoreButton == null)
        {
            scoreButton = GetComponent<Button>();
        }
        scoreButton.onClick.AddListener(OnScoreUI);
    }

    public void OnScoreUI()
    {
        UI.gameObject.SetActive(false);
        GameManager.Instance.rankUI.gameObject.SetActive(true);
    }

    public void SetScoreUI()
    {
        if(GameManager.Instance.scoreData.score[0] == GameManager.Instance.player.score)
        {
            score.text = "Best Score!!!\nTime : " + string.Format("{0:N2}", GameManager.Instance.player.score);
        }
        else
        {
            score.text = "Time : " + string.Format("{0:N2}", GameManager.Instance.player.score);

        }
    }
}
