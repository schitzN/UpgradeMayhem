﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Text _healthText;
    private int _curHealth;

    // Use this for initialization
    void Start()
    {
        this._healthText = GameObject.Find("Health").GetComponent<Text>();
		
        this.UpdateLabel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HitPlayer(int dmg)
    {
        this._curHealth -= dmg;
        this._healthText.text = "Health: " + this._curHealth + " / " + (int)StatsManager.Instance.GetStat("Health").GetCur();
    }

    public void UpdateLabel()
    {
        this._healthText.text = "Health: " + this._curHealth + " / " + (int)StatsManager.Instance.GetStat("Health").GetCur();
    }
}
