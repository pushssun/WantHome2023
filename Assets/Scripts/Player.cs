using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHP = 5;
    public int hp;
    public float score;
    public event Action OnHealthChanged;

    private void Awake()
    {
        hp = maxHP;
    }

    public void Heal()
    {
        this.hp ++;
        if(this.hp > maxHP)
        {
            this.hp = maxHP;
        }

        if(OnHealthChanged != null)
        {
            OnHealthChanged();
        }
    }

    public void TakeDamage()
    {
        this.hp --;
        if (this.hp < 0)
        {
            this.hp = 0;
            GameManager.Instance.GameOver();
        }

        if (OnHealthChanged != null)
        {
            OnHealthChanged();
        }
    }
}
