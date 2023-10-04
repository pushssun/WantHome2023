using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timer;

    public float time;

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGame)
        {
            time += Time.deltaTime;
            timer.text = "Time : " + string.Format("{0:N2}",time);

        }
    }
}
