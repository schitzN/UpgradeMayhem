using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Weapon {
    [SerializeField]
    private string _name;

    [SerializeField]
    private bool _isUnlocked;

    [SerializeField]
    private float _cost;

    [SerializeField]
    private List<Stat> _stats;

    public string GetName()
    {
        return this._name;
    }

    public float GetCost()
    {
        return this._cost;
    }

    public bool GetWeaponUnlocked() { return this._isUnlocked; }
}
