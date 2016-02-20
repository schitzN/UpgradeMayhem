using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TabsManager : Singleton<TabsManager> {
    public GameObject _tabsContainer;
    public GameObject _contentContainer;

	// Use this for initialization
	void Start () {
        // fill tabs
        this.CreateTab("Hero");
        this.CreateTab("Weapons");
        this.CreateTab("Inventory");
        this.CreateTab("ItemShop");
        this.CreateTab("ArtefactShop");
        this.CreateTab("Mercenary");

        // set hero as default tab
        this.ClickTabButton("Hero");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void CreateTab(string name)
    {
        GameObject go = this.CreateUIElement(name, "UI/TabsEle", this._tabsContainer.transform);
        go.GetComponentInChildren<Text>().text = name;
        go.GetComponent<Button>().onClick.AddListener(delegate { this.ClickTabButton(name); });
    }

    public void ClickTabButton(string name)
    {
        // delete old
        if(this._contentContainer.transform.childCount > 0)
            GameObject.Destroy(this._contentContainer.transform.GetChild(0).gameObject);

        // create new
        switch(name)
        {
            case "Hero":
                GameObject cont = this.CreateUIElement("Container" + name, "UI/Content2ColArmory", this._contentContainer.transform);
                Transform content = cont.transform.FindChild("ScrollerLeft/Viewport/Content");

                for(int i = 0; i < StatsManager.Instance.GetStats().Count; i++)
                {
                    GameObject ele = this.CreateUIElement(StatsManager.Instance.GetStats()[i].GetName(), "UI/ElementUpgradeables", content.FindChild("Panel" + (i % content.childCount)));

                    this.UpdateUpgradeableBtn(ele, StatsManager.Instance.GetStats()[i]);

                    ele.GetComponent<Button>().onClick.AddListener(delegate { StatsManager.Instance.UpgradeStat(ele, ele.name); });
                }
                break;
            case "Weapons":
                GameObject cont2 = this.CreateUIElement("Container" + name, "UI/ContentColCol2Col", this._contentContainer.transform);

                // create melee weapons
                Transform contentMelee = cont2.transform.FindChild("Scroller0/Viewport/Content");

                for (int i = 0; i < StatsManager.Instance.GetMeleeWeapons().Count; i++)
                    if (this.initWeaponButton(contentMelee.FindChild("Panel0").transform, StatsManager.Instance.GetMeleeWeapons()[i]))
                        break;

                // create ranged weapons
                Transform contentRanged = cont2.transform.FindChild("Scroller1/Viewport/Content");

                for (int i = 0; i < StatsManager.Instance.GetRangedWeapons().Count; i++)
                    if (this.initWeaponButton(contentRanged.FindChild("Panel0").transform, StatsManager.Instance.GetRangedWeapons()[i]))
                        break;

                break;
            default:
                break;
        }
    }

    private bool initWeaponButton(Transform parent, Weapon w)
    {
        GameObject ele = this.CreateUIElement(w.GetName(), "UI/ElementWeapons", parent);
        ele.transform.FindChild("Label").GetComponent<Text>().text = w.GetName();
        ele.transform.FindChild("Button/BtnText").GetComponent<Text>().text = w.GetCost() + "";
        ele.transform.FindChild("Image").GetComponent<Image>().sprite = StatsManager.Instance.GetSprite("Ico" + ele.name);

        if (w.GetWeaponUnlocked())
        {
            ele.transform.FindChild("Button").gameObject.SetActive(false);
        }
        else
        {
            ele.GetComponent<DragHelper>().enabled = false;
            return true; // only show next weapon to buy
        }

        return false;
    }

    private GameObject CreateUIElement(string name, string prefabPath, Transform parent)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>(prefabPath));
        go.name = name;

        go.transform.SetParent(parent, false);

        return go;
    }

    public void UpdateUpgradeableBtn(GameObject btn, Stat stat)
    {
        btn.transform.FindChild("LabelName").GetComponent<Text>().text = stat.GetName();
        btn.transform.FindChild("LabelCurrent").GetComponent<Text>().text = stat.GetCur() + "";
        btn.transform.FindChild("LabelCost").GetComponent<Text>().text = "$ " + stat.GetCost();
        btn.transform.FindChild("LabelAmount").GetComponent<Text>().text = "+" + stat.GetStep();
    }
}
