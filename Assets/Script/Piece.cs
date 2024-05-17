using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Piece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TypePiece type;
    public Image image;
    [HideInInspector] public Transform affterTranform;
    public void OnBeginDrag(PointerEventData eventData)
    {
        affterTranform = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(affterTranform);
        image.raycastTarget = true;
    }

}
public enum TypePiece
{
    Blue,
    Green,
    Orange,
    Pink,
    Red,
    Yellow,
    None
}