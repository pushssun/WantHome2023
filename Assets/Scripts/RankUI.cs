using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : ResultUI
{
    public Button backButton;
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if(backButton == null)
        {
            backButton = GetComponent<Button>();
        }
        backButton.onClick.AddListener(Back);

        UpdateScore();
    }

    public void UpdateScore()
    {
        string scoreStr = "";
        for(int i=0; i < GameManager.Instance.scoreData.score.Length-1; i++)
        {
            scoreStr += string.Format("{0:N2}", GameManager.Instance.scoreData.score[i]) + "\n";
        }
        score.text = scoreStr;
    }

    public void Back()
    {
        UI.SetActive(false);
        GameManager.Instance.winUI.gameObject.SetActive(true);
    }
}
