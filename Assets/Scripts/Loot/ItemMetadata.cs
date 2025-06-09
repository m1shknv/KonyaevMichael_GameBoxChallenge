using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Metadata", menuName = "Game/Item Metadata")]
public class ItemMetadata : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    public Sprite icon;

    [Header("Discovery Info")]
    public bool useCustomDate = false;
    [ConditionalHide("useCustomDate")] public DateTime customDate;

    public bool includeOwner = false;
    [ConditionalHide("includeOwner")] public string ownerName;

    public bool includeLocation = false;
    [ConditionalHide("includeLocation")] public string location;

    [HideInInspector] public string editorNote;
}

public class ConditionalHideAttribute : PropertyAttribute
{
    public string conditionalSourceField;

    public ConditionalHideAttribute(string conditionalSourceField)
    {
        this.conditionalSourceField = conditionalSourceField;
    }
}
