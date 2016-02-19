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
                GameObject cont = this.CreateUIElement("Container" + name, "UI/Content4Columns", this._contentContainer.transform);
                Transform content = cont.transform.FindChild("Viewport/Content");

                for(int i = 0; i < StatsManager.Instance.GetStats().Count; i++)
                {
                    GameObject ele = this.CreateUIElement(StatsManager.Instance.GetStats()[i].GetName(), "UI/ElementUpgradeables", content.FindChild("Panel" + (i % content.childCount)));
                    ele.transform.FindChild("Label").GetComponent<Text>().text = StatsManager.Instance.GetStats()[i].GetName() + ": " + StatsManager.Instance.GetStats()[i].GetCur();
                    ele.transform.FindChild("Button/BtnText").GetComponent<Text>().text = StatsManager.Instance.GetStats()[i].GetCost() +"";
                    ele.transform.FindChild("Button").GetComponent<Button>().onClick.AddListener(delegate { StatsManager.Instance.UpgradeStat(ele.name); });
                }
                //cont.transform.FindChild("Panel1")
                break;
            default:
                break;
        }
    }

    private GameObject CreateUIElement(string name, string prefabPath, Transform parent)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>(prefabPath));
        go.name = name;

        go.transform.SetParent(parent, false);

        return go;
    }
}
