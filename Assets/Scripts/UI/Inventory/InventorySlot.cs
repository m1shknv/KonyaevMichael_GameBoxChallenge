using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    private ItemData? itemData;

    public void SetItem(ItemData? item)
    {
        itemData = item;
        if (item.HasValue)
        {
            iconImage.sprite = item.Value.icon;
            iconImage.enabled = true;
        }
        else
        {
            iconImage.sprite = null;
            iconImage.enabled = false;
        }
    }

    public void OnClick()
    {
        if (itemData.HasValue)
        {
            InventoryUI.Instance.ShowItemDetails(itemData.Value);
        }
    }
}
