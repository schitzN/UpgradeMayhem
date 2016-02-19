using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Text _healthText;

    // CURRENT
    private float _curHealth;


    // Use this for initialization
    void Start()
    {
        this._curHealth = (int)StatsManager.Instance.GetStat("Health").GetCur();
        this._healthText = GameObject.Find("Health").GetComponent<Text>();
		
        this.UpdateLabel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HitPlayer(float dmg)
    {
        this._curHealth -= dmg;
        this._healthText.text = "Health: " + this._curHealth + " / " + (int)StatsManager.Instance.GetStat("Health").GetCur();
    }

    public void UpdateLabel()
    {
        this._healthText.text = "Health: " + this._curHealth + " / " + (int)StatsManager.Instance.GetStat("Health").GetCur();
    }
}
