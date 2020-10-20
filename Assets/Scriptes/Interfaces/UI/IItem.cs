using UnityEngine;
public interface IItem
{
    Sprite GetItemViewer { get; }
    string GetItemName { get; }
    string GetItemDescription { get; }
    CurrencyType CurrencyType { get; }
    ItemType ItemType { get; }
}

public enum ItemType
{
    AllTime,
    OneTime
}

