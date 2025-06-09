using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.yellow;

    private Text uiText; 

    private void Awake()
    {
        uiText = GetComponentInChildren<Text>();
    }

    public void OnPointerEnter()
    {
        if (uiText != null) uiText.color = hoverColor;
    }

    public void OnPointerExit()
    {
        if (uiText != null) uiText.color = normalColor;
    }
}