using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    private List<ItemData> items = new List<ItemData>();
    private List<NotebookEntry> entries = new List<NotebookEntry>();

    public IReadOnlyList<ItemData> Items => items;
    public IReadOnlyList<NotebookEntry> Entries => entries;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool AddItem(ItemData item)
    {
        if (!items.Exists(i => i.itemName == item.itemName))
        {
            items.Add(item);

            AddEntry(new NotebookEntry
            {
                person = item.finder,
                place = item.location,
                eventDescription = item.itemName,
                date = item.discoveryDate
            });

            Debug.Log($"Добавлен предмет: {item.itemName}");
            InventoryUI.Instance?.Refresh(items);
            return true;
        }

        return false;
    }


    public void ClearItems() => items.Clear();

    public void AddEntry(NotebookEntry entry)
    {
        entries.Add(entry);
        Debug.Log($"Добавлена запись: {entry.person}, {entry.place}");
    }

    public void ClearEntries() => entries.Clear();
}
