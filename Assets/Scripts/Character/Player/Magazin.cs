using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Magazin : MonoBehaviour
{
    Text _magText;
    Text _reloadText;

    private float _reloadT = 0;
    private int _bullets;

    // Use this for initialization
    void Start()
    {
        this._bullets = (int)StatsManager.Instance.GetStat("MagSize").GetCur();
        this._magText = GameObject.Find("Magazin").GetComponent<Text>();
        this._reloadText = GameObject.Find("Reload").GetComponent<Text>();

        this.UpdateMagSizeLabel();
    }
	
    // Update is called once per frame
    void Update()
    {
        // mag empty
        if (this._bullets <= 0)
        {
            this._reloadT += Time.deltaTime;
            this._reloadText.text = "Reload: " + (StatsManager.Instance.GetStat("ReloadTime").GetCur() - this._reloadT).ToString("0.00");
            // do reload
            if (this._reloadT >= StatsManager.Instance.GetStat("ReloadTime").GetCur())
            {
                this._bullets = (int)StatsManager.Instance.GetStat("MagSize").GetCur();
                this._reloadT = 0;

                this._reloadText.text = "Reload: 0";
                this._magText.text = "Magazin: " + this._bullets + " / " + (int)StatsManager.Instance.GetStat("MagSize").GetCur();
            }
        }
    }

    public void RemoveBullet()
    {
        this._bullets--;
        this._magText.text = "Magazin: " + this._bullets + " / " + (int)StatsManager.Instance.GetStat("MagSize").GetCur();
    }

    public void UpdateMagSizeLabel()
    {
        this._magText.text = "Magazin: " + this._bullets + " / " + (int)StatsManager.Instance.GetStat("MagSize").GetCur();
    }

    public int GetNumBullets()
    {
        return this._bullets;
    }
}
