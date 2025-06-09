using System;
using UnityEngine;

[Serializable]
public struct ItemData
{
    public string itemName;
    public DateTime discoveryDate;
    public Sprite icon;
    public string finder;
    public string location;

    public ItemData(string name, DateTime date, Sprite icon, string finder, string location)
    {
        this.itemName = name;
        this.discoveryDate = date;
        this.icon = icon;
        this.finder = finder;
        this.location = location;
    }
}