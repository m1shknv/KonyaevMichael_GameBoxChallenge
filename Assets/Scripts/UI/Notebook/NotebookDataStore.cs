using System.Collections.Generic;
using UnityEngine;

public class NotebookDataStore : MonoBehaviour
{
    public static NotebookDataStore Instance { get; private set; }

    public List<ItemData> CollectedItems { get; private set; } = new List<ItemData>();
    public List<NotebookEntry> CollectedEntries { get; private set; } = new List<NotebookEntry>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetCollectedItems(List<ItemData> items)
    {
        CollectedItems = new List<ItemData>(items);
    }

    public void SetCollectedEntries(List<NotebookEntry> entries)
    {
        CollectedEntries = new List<NotebookEntry>(entries);
    }

    public void ClearAll()
    {
        CollectedItems.Clear();
        CollectedEntries.Clear();
    }
}
