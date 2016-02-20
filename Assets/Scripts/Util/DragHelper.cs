using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHelper : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;

    Vector3 startPosition;
    Transform startParent;
    int startIndex = -1;

    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;

        startPosition = transform.position;
        startParent = transform.parent;
        Debug.Log(startParent);
        if(startParent.childCount > 1)
        {
            for(int i = 0; i < startParent.childCount; i++)
            {
                if(startParent.GetChild(i) == this)
                {
                    Debug.Log(startParent.GetChild(i));
                    this.startIndex = i;
                    break;
                }
            }
        }

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        Debug.Log("BEGIN");
        this.transform.SetParent(GameObject.Find("Canvas").transform);
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        var currentPosition = ((RectTransform)transform).position;
        currentPosition.x += eventData.delta.x;
        currentPosition.y += eventData.delta.y;
        ((RectTransform)transform).position = currentPosition;

        //transform.position = Input.mousePosition;
        Debug.Log("drag " + eventData.position + ", " + ((RectTransform)transform).position);
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        //GetComponent<LayoutElement>().ignoreLayout = false;
        this.transform.SetParent(this.startParent);

        if(this.startIndex != -1)
            this.transform.SetSiblingIndex(this.startIndex);

        Debug.Log(this.startIndex + ", " + this.transform.GetSiblingIndex());

        if (transform.parent == startParent)
        {
            
            transform.position = startPosition;
        }
    }

    #endregion



}