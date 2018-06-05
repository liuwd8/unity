using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
    public Canvas canvas;
    public GameObject healthPrefab;

    public float healthPanelOffset = 2f;
    public GameObject healthPanel;
    private Slider healthSlider;

    // Use this for initialization
    void Start () {
        healthPanel = Instantiate(healthPrefab) as GameObject;
        healthPanel.transform.SetParent(canvas.transform, false);
        healthSlider = healthPanel.GetComponentInChildren<Slider>();
        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthPanel.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthPanel.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
    }
}
