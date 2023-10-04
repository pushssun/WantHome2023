using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;
    public GameObject image;

    public Animator ContentPanel;
    public Animator gearImage;

    // Start is called before the first frame update
    void Start()
    {
        if(startButton == null)
        {
            startButton = GetComponent<Button>();    

        }
        if(exitButton == null)
        {
            exitButton = GetComponent<Button>();

        }

        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);

        StartCoroutine(SetActiveImageRoutine());
    }

    public IEnumerator SetActiveImageRoutine()
    {
        yield return new WaitForSeconds(1);
        image.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ToggleMenu()
    {
        bool isHidden = ContentPanel.GetBool("isHidden");
        ContentPanel.SetBool("isHidden", !isHidden);

        gearImage.SetBool("isHidden", !isHidden);
    }
}
