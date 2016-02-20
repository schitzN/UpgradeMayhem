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

    // WEAPONS
    private List<Weapon> _meleeWeapons = new List<Weapon>();
    private List<Weapon> _rangedWeapons = new List<Weapon>();

    private int _availableSlots = 1;
    private int _activeSlot = 0;
    private List<Weapon> _activeSlots = new List<Weapon>();
    private GameObject _weaponSlotsContainer;
    private Sprite[] _spriteSheet;

    // Use this for initialization
    void Start()
    {
        // prepare
        this._spriteSheet = Resources.LoadAll<Sprite>("GUI_Sprites");
        this._weaponSlotsContainer = GameObject.Find("WeaponSlots");

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

        // add melee weapons
        this._meleeWeapons.Add(new Weapon("Fists", 0.5f, 2.5f, 0.25f, 1, 1));
        this._meleeWeapons[0]._isUnlocked = true;
        this._meleeWeapons.Add(new Weapon("Baseball Bat", 1, 5, 0.25f, 1, 1));
        this._meleeWeapons.Add(new Weapon("Sword", 1, 10, 0.5f, 1, 1));

        // add ranged weapons
        this._rangedWeapons.Add(new Weapon("Stone", 0.5f, 2.5f, 0.25f, 1, 1));
        this._rangedWeapons.Add(new Weapon("Pistol", 1, 5, 0.25f, 1, 1));

        // setup weapons
        for (int i = 0; i < this._availableSlots; i++)
            this.AddWeaponSlot();

        this._weaponSlotsContainer.transform.GetChild(0).GetComponent<DropableElement>().SetCurrentItem("Fists");
        this.ClickedWeaponSlot(0);

        this._moneyText = GameObject.Find("MoneyLabel").GetComponent<Text>();

        this.UpdateAllLabels();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    public void ClickedWeaponSlot(int idx)
    {
        for(int i = 0; i < this._weaponSlotsContainer.transform.childCount - 1; i++)
        {
            if(i == idx)
            {
                this._weaponSlotsContainer.transform.GetChild(i).GetComponent<Button>().interactable = false;
                this._activeSlot = i;
                // TODO: Calculate new dmg for damageLabel
            }
            else
                this._weaponSlotsContainer.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    public void AddWeaponSlot()
    {
        GameObject slot = Instantiate(Resources.Load<GameObject>("UI/Weaponslot"));
        slot.transform.SetParent(this._weaponSlotsContainer.transform, false);
        slot.transform.SetSiblingIndex(this._weaponSlotsContainer.transform.childCount - 2);
        slot.GetComponent<Button>().onClick.AddListener(delegate { StatsManager.Instance.ClickedWeaponSlot(slot.transform.GetSiblingIndex()); });

        this._activeSlots.Add(null);
    }

    public void UpdateWeaponSlot(int index, string name)
    {
        Weapon wpn = null;

        foreach(Weapon w in this._meleeWeapons)
            if(w.GetName().Equals(name))
                wpn = w;

        if(wpn == null)
            foreach (Weapon w in this._rangedWeapons)
                if (w.GetName().Equals(name))
                    wpn = w;

        if (this._activeSlots[index] != null)
            this._activeSlots.RemoveAt(index);

        this._activeSlots.Insert(index, wpn);
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

    public Weapon GetMeleeWeapon(string name)
    {
        foreach (Weapon s in this._meleeWeapons)
            if (s.GetName() == name)
                return s;

        return null;
    }

    public Weapon GetRangedWeapon(string name)
    {
        foreach (Weapon s in this._rangedWeapons)
            if (s.GetName() == name)
                return s;

        return null;
    }

    public void UpgradeStat(GameObject btn, string name)
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

        TabsManager.Instance.UpdateUpgradeableBtn(btn, s);
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
    public List<Weapon> GetMeleeWeapons() { return this._meleeWeapons; }
    public List<Weapon> GetRangedWeapons() { return this._rangedWeapons; }

    public Sprite GetSprite(string name) {
        foreach (Sprite s in this._spriteSheet)
            if (s.name == name)
                return s;

        return null;
    }
}
