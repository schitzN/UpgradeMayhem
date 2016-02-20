using UnityEngine;
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
        Debug.Log("DROP");
        if (!item)
        {
            //DragHelper.itemBeingDragged.transform.SetParent(transform);
            //ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasChanged());
        }
        Debug.Log(this.transform.GetSiblingIndex() + ", " + DragHelper.itemBeingDragged.name);
        StatsManager.Instance.UpdateWeaponSlot(this.transform.GetSiblingIndex(), DragHelper.itemBeingDragged.name);
    }
    #endregion
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}