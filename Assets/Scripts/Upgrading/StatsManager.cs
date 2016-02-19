using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StatsManager : Singleton<StatsManager>
{
    // MONEY
    private float _money = 0;
    private Text _moneyText;

    // STATS
    private List<Stat> _heroStats = new List<Stat>();

    // Use this for initialization
    void Start()
    {
        // add stats
        this._heroStats.Add(new Stat("Health", 1, 10, 1, 1, 1));
        this._heroStats.Add(new Stat("Health Regeneration", 1, 10, 1, 1, 1));
        this._heroStats.Add(new Stat("Sight", 0.2f, 1f, 0.01f, 1, 1));
        this._heroStats.Add(new Stat("Strength", 1, 10, 1, 1, 1));
        this._heroStats.Add(new Stat("Accuracy", 1, 10, 1, 1, 1));
        this._heroStats.Add(new Stat("Score Multiplier", 1, 10, 1, 1, 1));
        this._heroStats.Add(new Stat("Armor", 1, 10, 1, 1, 1));
        this._heroStats.Add(new Stat("Gold Multiplier", 1, 10, 1, 1, 1));

        // TODO: Remove weapons from herostats
        this._heroStats.Add(new Stat("BpS", 0.5f, 10f, 0.05f, 1, 1));
        this._heroStats.Add(new Stat("Damage", 1f, 10f, 0.25f, 1, 1));
        this._heroStats.Add(new Stat("Recoil", 0, 1f, 0.05f, 1, 1));
        this._heroStats.Add(new Stat("BulletSpeed", 5f, 30f, 0.5f, 1, 1));
        this._heroStats.Add(new Stat("MagSize", 5, 100, 1, 1, 1));
        this._heroStats.Add(new Stat("ReloadTime", 2f, -0.5f, -0.05f, 1, 1));
        
        
        //
        this._moneyText = GameObject.Find("MoneyLabel").GetComponent<Text>();
        this.UpdateAllLabels();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    public void CreateStat()
    {

    }

    public float getMoney()
    {
        return this._money;
    }

    public Stat GetStat(string name)
    {
        foreach (Stat s in this._heroStats)
            if (s.GetName() == name)
                return s;

        return null;
    }

    public void UpgradeStat(string name)
    {
        Stat s = this.GetStat(name);

        // stat not found
        if (s == null)
            return;// false;

        // can we afford the upgrade
        if (!this.RemoveMoney(s.GetCost()))
            return;// false;

        // do the upgrade
        s.UpgradeStat();
    }

    public void AddMoney(float m)
    {
        this._money += m;
        this._moneyText.text = "Money: " + this._money;
    }

    public bool RemoveMoney(float m)
    {
        if (this._money - m < 0)
            return false;

        this._money -= m;
        this._moneyText.text = "Money: " + this._money;

        return true;
    }

    public void UpdateAllLabels()
    {
        //foreach (Stat s in this._heroStats)
        //    s.UpdateLabel();

        this._moneyText.text = "Money: " + this._money;
    }

    public List<Stat> GetStats() { return this._heroStats; }
}
