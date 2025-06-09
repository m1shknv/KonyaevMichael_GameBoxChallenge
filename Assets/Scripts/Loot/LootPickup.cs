using UnityEngine;

public class LootPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemMetadata metadata;

    public void OnInteract()
    {
        AudioManager.Instance?.PlayLootPickupSound();

        var date = metadata.useCustomDate
            ? metadata.customDate
            : GameDateSystem.Instance.GetCurrentDate();

        var itemData = new ItemData(
            metadata.itemName,
            date,
            metadata.icon,
            metadata.includeOwner ? metadata.ownerName : "",
            metadata.includeLocation ? metadata.location : ""
        );

        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
