using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DropableElement : MonoBehaviour, IDropHandler
{
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
                return transform.GetChild(0).gameObject;

            return null;
        }
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        this.SetCurrentItem(DragHelper.itemBeingDragged.name);
    }
    #endregion

    public void SetCurrentItem(string name)
    {
        StatsManager.Instance.UpdateWeaponSlot(this.transform.GetSiblingIndex(), name);
        this.transform.FindChild("Image").GetComponent<Image>().sprite = StatsManager.Instance.GetSprite("Ico" + name);
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}