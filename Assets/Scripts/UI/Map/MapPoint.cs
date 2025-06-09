using UnityEngine;
using UnityEngine.UI;

public class MapPoint : MonoBehaviour
{
    [SerializeField] private GameObject neutralVisual;
    [SerializeField] private GameObject activeVisual;
    [SerializeField] private GameObject inactiveVisual;

    private MapManager mapManager;

    public void Initialize(MapManager manager)
    {
        mapManager = manager;
        SetNeutral(); 
    }

    public void OnClick()
    {
        mapManager.SelectPoint(this);
        AudioManager.Instance?.PlayButtonClick();
    }

    public void SetActiveVisual()
    {
        activeVisual.SetActive(true);
        inactiveVisual.SetActive(false);
        neutralVisual.SetActive(false);
    }

    public void SetInactiveVisual()
    {
        activeVisual.SetActive(false);
        inactiveVisual.SetActive(true);
        neutralVisual.SetActive(false);

        var button = GetComponent<Button>();
        if (button)
            button.interactable = false;
    }

    public void SetNeutral()
    {
        activeVisual.SetActive(false);
        inactiveVisual.SetActive(false);
        neutralVisual.SetActive(true);
    }
}
