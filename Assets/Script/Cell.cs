using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IDropHandler
{
    public TypePiece type;
    public int x { get; private set; }
    public int y { get; private set; }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject drop = eventData.pointerDrag;
            Piece piece = drop.GetComponent<Piece>();
            piece.affterTranform = transform;
            type = piece.type;
        }

    }

    public void Init(int _x, int _y)
    {
        this.x = _x;
        this.y = _y;
        type = TypePiece.None;

    }
    private void LateUpdate()
    {
        if (transform.childCount != 0)
        {
            // Debug.Log(type.ToString());
            return;
        }
        type = TypePiece.None;
    }

}
