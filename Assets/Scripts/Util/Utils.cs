using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Utils : MonoBehaviour
{
    static GraphicRaycaster graphicRaycaster;
    public static Color InventoryColor = new Color(0.9098f, 0.9176f, 0.9607f, 1f);

    private void Awake()
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
    }

    public static bool IsUIHit()
    {
        PointerEventData pointerEventData = new PointerEventData(null);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        if (results.Count < 1)
            return false;
        else
            return true;
    }
}
