//using System;
using UnityEngine;
using System.Collections;

//[Serializable]
public class Weapon {
    // UI
    private string _name;
    public bool _isUnlocked;

    //private Text _txt;
    //private Text _btnTxt;

    // stat
    private float _cur;
    private float _max;
    private float _step;

    // cost
    private float _cost;
    private float _mod;

    public Weapon(string name, float cur, float max, float step, int cost, float mod)
    {
        this._name = name;
        //this._txt = txt;
        //this._btnTxt = btnTxt;

        this._cur = cur;
        this._max = max;
        this._step = step;

        this._cost = cost;
        this._mod = mod;
    }

    public string GetName()
    {
        return this._name;
    }
    public float GetCur()
    {
        return this._cur;
    }
    public float GetMax()
    {
        return this._max;
    }
    public float GetStep()
    {
        return this._step;
    }
    public float GetCost()
    {
        return this._cost;
    }
    public float GetMod()
    {
        return this._mod;
    }

    public bool UpgradeStat()
    {
        // Is stat already max lvl
        if ((this._step > 0 && this._cur < this._max) || (this._step < 0 && this._cur > this._max))
        {
            this._cur += this._step;
            this._cost += this._mod;
            //this.UpdateLabel();

            return true;
        }

        return false;
    }
    /*
    public void UpdateLabel()
    {
        this._txt.text = this._name + ": " + this._cur.ToString("0.00");
        this._btnTxt.text = "Costs: " + this._cost.ToString("0.0");
    }*/
}
