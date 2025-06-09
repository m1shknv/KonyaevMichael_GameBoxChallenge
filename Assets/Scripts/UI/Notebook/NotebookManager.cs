using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] private Transform leftPageContainer;
    [SerializeField] private Transform rightPageContainer;
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private int maxEntriesPerPage = 10;

    private List<NotebookEntry> _entries = new List<NotebookEntry>();
    private List<NotebookEntryUI> _entryUIs = new List<NotebookEntryUI>();
    private NotebookEntryUI _selectedEntryUI;
    private List<NotebookEntryUI> _mergedPairs = new List<NotebookEntryUI>();
    private bool _isProcessingClick = false;
    private Dictionary<string, List<NotebookEntryUI>> _mergedChains = new Dictionary<string, List<NotebookEntryUI>>();

    private void Start()
    {
        _entries = new List<NotebookEntry>(NotebookDataStore.Instance.CollectedEntries);
        DisplayEntries();
    }

    public void DisplayEntries()
    {
        ClearPages();

        for (int i = 0; i < _entries.Count; i++)
        {
            var container = i < maxEntriesPerPage ? leftPageContainer : rightPageContainer;
            var entryGO = Instantiate(entryPrefab, container);
            var ui = entryGO.GetComponent<NotebookEntryUI>();
            ui.Setup(_entries[i], this);
            _entryUIs.Add(ui);
        }
    }

    private void ClearPages()
    {
        foreach (Transform child in leftPageContainer) Destroy(child.gameObject);
        foreach (Transform child in rightPageContainer) Destroy(child.gameObject);

        _entryUIs.Clear();
        _selectedEntryUI = null;
        _mergedPairs.Clear();
    }

    public void HandleEntrySelection(NotebookEntryUI clickedUI)
    {
        if (_isProcessingClick || clickedUI == null) return;
        _isProcessingClick = true;

        if (_selectedEntryUI == clickedUI)
        {
            _selectedEntryUI.SetActive(false);
            _selectedEntryUI = null;
            _isProcessingClick = false;
            return;
        }

        if (_selectedEntryUI != null)
        {
            TryMergeEntries(clickedUI);
            _isProcessingClick = false;
            return;
        }

        _selectedEntryUI = clickedUI;
        _selectedEntryUI.SetActive(true);
        _isProcessingClick = false;
    }

    private void TryMergeEntries(NotebookEntryUI targetUI)
    {
        if (_selectedEntryUI == null || targetUI == null) return;

        var selectedEntry = _selectedEntryUI.GetEntry();
        var targetEntry = targetUI.GetEntry();

        if (selectedEntry.person != targetEntry.person || !selectedEntry.IsComplete || !targetEntry.IsComplete)
        {
            _selectedEntryUI.SetActive(false);
            _selectedEntryUI = targetUI;
            _selectedEntryUI.SetActive(true);
            return;
        }

        string personName = selectedEntry.person;

        if (!_mergedChains.ContainsKey(personName))
        {
            _mergedChains[personName] = new List<NotebookEntryUI>();
        }

        if (!_mergedChains[personName].Contains(_selectedEntryUI))
        {
            _mergedChains[personName].Add(_selectedEntryUI);
        }
        if (!_mergedChains[personName].Contains(targetUI))
        {
            _mergedChains[personName].Add(targetUI);
        }

        Color chainColor = NotebookEntryUI.GenerateColorFromName(personName);

        foreach (var entry in _mergedChains[personName])
        {
            entry.SetChainColor(chainColor);
            entry.SetInChain(true);
        }

        int selectedIndex = _entryUIs.IndexOf(_selectedEntryUI);
        int targetIndex = _entryUIs.IndexOf(targetUI);

        if (selectedIndex >= 0 && targetIndex >= 0 && selectedIndex != targetIndex)
        {
            int newPosition = selectedIndex + 1;

            if (targetIndex != newPosition)
            {
                try
                {
                    _entryUIs.RemoveAt(targetIndex);
                    if (newPosition > _entryUIs.Count) newPosition = _entryUIs.Count;
                    _entryUIs.Insert(newPosition, targetUI);
                }
                catch
                {
                    DisplayEntries();
                    return;
                }
            }
        }

        RebuildUI();
        _selectedEntryUI.SetActive(false);
        _selectedEntryUI = null;
    }

    private void RebuildUI()
    {
        foreach (var entry in _entryUIs)
        {
            entry.SetInChain(false);
        }

        foreach (var chain in _mergedChains.Values)
        {
            if (chain.Count > 0)
            {
                Color chainColor = NotebookEntryUI.GenerateColorFromName(chain[0].GetEntry().person);
                foreach (var entry in chain)
                {
                    if (_entryUIs.Contains(entry))
                    {
                        entry.SetChainColor(chainColor);
                        entry.SetInChain(true);
                    }
                }
            }
        }

        for (int i = 0; i < _entryUIs.Count; i++)
        {
            var container = i < maxEntriesPerPage ? leftPageContainer : rightPageContainer;
            _entryUIs[i].transform.SetParent(container);
            _entryUIs[i].transform.SetSiblingIndex(i % maxEntriesPerPage);
        }

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(leftPageContainer as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rightPageContainer as RectTransform);
    }
}