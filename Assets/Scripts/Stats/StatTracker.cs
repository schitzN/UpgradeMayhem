using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StatTracker : MonoBehaviour
{
    // MONEY
    private float _money = 0;
    private Text _moneyText;

    // STATS
    private List<Stat> _stats = new List<Stat>();

    // Use this for initialization
    void Start()
    {
        // add stats
        this._stats.Add(new Stat("Health", GameObject.Find("HealthLabel").GetComponent<Text>(), GameObject.Find("BtnHealthText").GetComponent<Text>(), 1, 10, 1, 1, 1));
        this._stats.Add(new Stat("BpS", GameObject.Find("BpSLabel").GetComponent<Text>(), GameObject.Find("BtnBpSText").GetComponent<Text>(), 0.5f, 10f, 0.05f, 1, 1));
        this._stats.Add(new Stat("Damage", GameObject.Find("DamageLabel").GetComponent<Text>(), GameObject.Find("BtnDamageText").GetComponent<Text>(), 1f, 10f, 0.25f, 1, 1));
        this._stats.Add(new Stat("Recoil", GameObject.Find("RecoilLabel").GetComponent<Text>(), GameObject.Find("BtnRecoilText").GetComponent<Text>(), 0, 1f, 0.05f, 1, 1));
        this._stats.Add(new Stat("BulletSpeed", GameObject.Find("BulletSpeedLabel").GetComponent<Text>(), GameObject.Find("BtnBulletSpeedText").GetComponent<Text>(), 5f, 30f, 0.5f, 1, 1));
        this._stats.Add(new Stat("MagSize", GameObject.Find("MagSizeLabel").GetComponent<Text>(), GameObject.Find("BtnMagSizeText").GetComponent<Text>(), 5, 100, 1, 1, 1));
        this._stats.Add(new Stat("ReloadTime", GameObject.Find("ReloadTimeLabel").GetComponent<Text>(), GameObject.Find("BtnReloadTimeText").GetComponent<Text>(), 2f, -0.5f, -0.05f, 1, 1));
        this._stats.Add(new Stat("Sight", GameObject.Find("SightLabel").GetComponent<Text>(), GameObject.Find("BtnSightText").GetComponent<Text>(), 0.2f, 1f, 0.01f, 1, 1));

        //
        this._moneyText = GameObject.Find("MoneyLabel").GetComponent<Text>();
        this.UpdateAllLabels();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    public float getMoney()
    {
        return this._money;
    }

    public Stat GetStat(string name)
    {
        foreach (Stat s in this._stats)
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
        foreach (Stat s in this._stats)
            s.UpdateLabel();

        this._moneyText.text = "Money: " + this._money;
    }
}
