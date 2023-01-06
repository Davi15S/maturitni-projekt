using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    PointerEventData pointerEventData;
    [SerializeField] GraphicRaycaster m_Raycaster;
    public bool MouseOver()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            foreach (RaycastResult item in results)
            {
                if (item.gameObject.tag == "Tooltip")
                    return true;
            }
            return false;
        }
        else
            return false;
    }
}
