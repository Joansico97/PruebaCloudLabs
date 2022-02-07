using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropParentAprove : MonoBehaviour, IDropHandler
{
    public GameObject item;

    public void OnDrop(PointerEventData eventData)
    {
        item = DragHandler.itemDraggin;
        if (item.GetComponent<EstudianteController>().notaEstudiante >= 3)
        {
            DragHandler.itemDraggin.transform.SetParent(transform);
        }
    }

    void Update()
    {
        if (item != null && item.transform.parent != transform)
        {
            item = null;
        }
    }
}
