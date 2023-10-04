using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class MenuRank : MonoBehaviour
{
    public ScoreData scoreData;
    public TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        scoreData = LoadData();
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ScoreData LoadData()
    {
        try
        {
            string str = File.ReadAllText(Application.persistentDataPath + "/ScoreData.json");
            ScoreData data = JsonUtility.FromJson<ScoreData>(str);
            return data;
        }
        catch (Exception ex) { }

        return null;
    }

    public void UpdateScore()
    {
        string scoreStr = "";
        for (int i = 0; i < scoreData.score.Length - 1; i++)
        {
            scoreStr += string.Format("{0:N2}", scoreData.score[i]) + "\n";
        }
        score.text = scoreStr;
    }
}
