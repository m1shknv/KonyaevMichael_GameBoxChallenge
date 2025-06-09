using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsPanel : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text dateText;
    [SerializeField] private Text finderText;
    [SerializeField] private Text locationText;

    public void Display(ItemData item)
    {
        nameText.text = item.itemName;
        dateText.text = item.discoveryDate.ToString("dd.MM.yyyy");
        finderText.text = string.IsNullOrEmpty(item.finder) ? "-" : item.finder;
        locationText.text = string.IsNullOrEmpty(item.location) ? "-" : item.location;
    }
}
