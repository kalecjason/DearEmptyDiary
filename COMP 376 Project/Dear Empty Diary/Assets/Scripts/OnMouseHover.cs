using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    // The Text component which changes color on hover
    private Text buttonText;
    // The colors on hover and outside of hover
    private Color hoverColor, originalColor;

    void Start()
    {
        buttonText = this.GetComponent<Text>();
        // We can also customize the color with rgba as such: new Color(r, g, b, a))
        //hoverColor = new Color(188.0f, 39.0f, 39.0f, 255.0f);
        hoverColor = new Color(188.0f/255f, 39.0f/255f, 39.0f/255f);
        originalColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        buttonText.color = originalColor;
    }
}
