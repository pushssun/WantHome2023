using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public GameObject UI;
    public Button homeButton;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(homeButton == null)
        {
            homeButton = GetComponent<Button>();

        }  

        homeButton.onClick.AddListener(BackToMenu);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void OnUI()
    {
        UI.SetActive(true);
    }
}
