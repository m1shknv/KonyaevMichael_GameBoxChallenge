using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [SerializeField] private InventorySlot[] slots;
    [SerializeField] private ItemDetailsPanel detailsPanel;

    private void Awake()
    {
        Instance = this;
        detailsPanel.gameObject.SetActive(false);
    }

    public void Refresh(IReadOnlyList<ItemData> items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].SetItem(items[i]);
            }
            else
            {
                slots[i].SetItem(null);
            }
        }

        detailsPanel.gameObject.SetActive(false);
    }

    public void ShowItemDetails(ItemData item)
    {
        detailsPanel.gameObject.SetActive(true);
        detailsPanel.Display(item);
    }
}
