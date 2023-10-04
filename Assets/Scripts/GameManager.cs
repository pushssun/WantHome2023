using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public enum gameResult
{
    None, Win, Fail,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;
    public PlayerController playerController;
    public EteverseManager manager;
    public GameOverUI gameOverUI;
    public WinUI winUI;
    public RankUI rankUI;
    public GameObject specialUI;
    public Timer timer;
    public ScoreData scoreData;
    public gameResult gameResult;
    public AudioSource winSound;
    public AudioSource failSound;
    public AudioSource ambientSound;
    public GameObject readyUI;
    public GameObject threeText;
    public GameObject twoText;
    public GameObject oneText;
    public GameObject goText;
    public bool saveEnabled;
    public bool isGame;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }

        if (player == null)
        {
            player = GetComponent<Player>();
        }
        if (manager == null)
        {
            manager = GetComponent<EteverseManager>();
        }

        scoreData = LoadData();
        saveEnabled = true;
    }

    private void Start()
    {
        ambientSound.volume = SoundController.Instance.soundVolume;
        StartCoroutine(CountRoutine());
    }

    private void Update()
    {
        if (isGame)
        {
            GameStart();
        }
    }

    private void GameStart()
    {
        playerController.enabled = true;
        manager.enabled = true;
    }

    private IEnumerator CountRoutine()
    {
        threeText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        threeText.SetActive(false);
        twoText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        twoText.SetActive(false);
        oneText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        oneText.SetActive(false);
        goText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        readyUI.SetActive(false);
        isGame = true;
    }

    public void SaveData()
    {
        if (saveEnabled)
        {
            if(scoreData == null)
            {
                scoreData = new ScoreData();
            }
            //초기 Input, sort
            int i = 0;
            for(i = 0; i < scoreData.score.Length-1; i++)
            {
                if (scoreData.score[i] == 0)
                {
                    break;
                }
            }
            if (i < scoreData.score.Length-1)
            {
                scoreData.score[i] = GameManager.Instance.player.score;
                for(int j=0; j < i; j++)
                {
                    for(int k = j+1; k <= i; k++)
                    {
                        if (scoreData.score[j] > scoreData.score[k])
                        {
                            float temp = scoreData.score[j];
                            scoreData.score[j] = scoreData.score[k];
                            scoreData.score[k] = temp;
                        }

                    }
                }
            }
            //초기 기록이 없을 때,정렬
            else
            {
                scoreData.score[scoreData.score.Length - 1] = GameManager.Instance.player.score;
                Array.Sort(scoreData.score);
            }
            File.WriteAllText(Application.persistentDataPath + "/ScoreData.json", JsonUtility.ToJson(scoreData));

        }
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

    //private void OnApplicationPause() 
    //{
    //    SaveData();
    //}
    //private void OnApplicationQuit()
    //{
    //    SaveData();
    //}

    public void GameOver()
    {
        //Debug.Log("Game Over!!");
        isGame = false;
        gameResult = gameResult.Fail;
        ambientSound.Stop();
        failSound.Play();
        gameOverUI.OnUI();
    }

    public void Win()
    {
        gameResult = gameResult.Win;
        ambientSound.Stop();
        winSound.Play();
        isGame = false;
        player.score = timer.time;
        SaveData();
        winUI.SetScoreUI();
        winUI.OnUI();
    }

    public void Special()
    {
        StartCoroutine(SpecialWinRoutine());
        Win();
    }

    private IEnumerator SpecialWinRoutine()
    {

        specialUI.SetActive(true);
        yield return new WaitForSeconds(2);
        specialUI.SetActive(false);
    }
    
}
