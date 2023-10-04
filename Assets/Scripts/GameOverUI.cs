using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : ResultUI
{
    public Button restartButton;

    protected override void Start()
    {
        base.Start();
        if (restartButton == null)
        {
            restartButton = GetComponent<Button>();

        }

        restartButton.onClick.AddListener(RestartGame);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
