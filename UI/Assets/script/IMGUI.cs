using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGUI : MonoBehaviour {
    public float healthPanelOffset = 2f;
    private void OnGUI()
    {
        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Rect rect = new Rect(screenPos.x -50,screenPos.y,100,100);
        GUI.HorizontalScrollbar(rect, 0, 75, 0, 100);
    }
}
