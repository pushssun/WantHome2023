using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private GameObject[] heartContainers;
    private Image[] heartFills;

    public Transform heartParent;
    public GameObject heartContainerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        heartContainers = new GameObject[(int)GameManager.Instance.player.maxHP];
        heartFills = new Image[(int)GameManager.Instance.player.hp];

        GameManager.Instance.player.OnHealthChanged += Player_OnHealthChanged;//�̺�Ʈ ����
        InstantiateHeartContainers(); //heartcontainer ����
    }

    private void Player_OnHealthChanged()
    {
        SetFilledHeart();
    }

    private void SetFilledHeart()
    {
        for(int i = 0; i < heartFills.Length; i++)
        {
            if (i < GameManager.Instance.player.hp)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }
    }
    
    private void InstantiateHeartContainers()
    {
        //maxHP��ŭ health ����
        for(int i = 0; i < GameManager.Instance.player.maxHP; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
}
