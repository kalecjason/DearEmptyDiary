using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableButtons : MonoBehaviour {

    // The buttons on the canvas to disable/enable
    [SerializeField]
    private GameObject goodBtn;
    [SerializeField]
    private GameObject badBtn;

    // On start, if not assigned, find the buttons
    void Start()
    {
        if (!goodBtn)
        {
            goodBtn = GameObject.Find("GoodBtn");
        }

        if (!badBtn)
        {
            badBtn = GameObject.Find("BadBtn");
        }
    }

    // Toggle buttons when we first trigger
    void OnTriggerEnter2D(Collider2D col)
    {
        ToggleButtons();
    }

    // When we leave, re-toggle (off)
    void OnTriggerExit2D()
    {
        ToggleButtons();
    }

    // Toggles the buttons
    private void ToggleButtons()
    {
        this.goodBtn.gameObject.SetActive(!this.goodBtn.activeSelf);
        this.badBtn.gameObject.SetActive(!this.badBtn.activeSelf);
    }
}
