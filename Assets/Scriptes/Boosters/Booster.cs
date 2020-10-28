using UnityEngine;

[CreateAssetMenu(fileName = "new Booster", menuName = "Booster SO")]
public class Booster : ScriptableObject, IItem, IBuyableObject
{
    [SerializeField] private BoosterType _boosterType;
    [SerializeField] private float _changingValue;
    [SerializeField] private float _price;
    [Multiline]
    [SerializeField] private string _descriptions;
    [SerializeField] private string _itemName;
    [SerializeField] private CurrencyType _currencyType;
    [SerializeField] private Sprite _itemViewer;
    [SerializeField] private ItemType _itemType;

    public BoosterType Type => _boosterType;
    public float Value => _changingValue;
    public float GetItemPrice => _price;
    public Sprite GetItemViewer => _itemViewer;
    public string GetItemName => _itemName;
    public string GetItemDescription => _descriptions;
    public CurrencyType CurrencyType => _currencyType;
    public ItemType ItemType => _itemType;
    public float Price => _price;
    public int Id => 0;
    string IBuyableObject.Type => _boosterType.ToString();
}

public enum BoosterType
{
    Speed,
    Damage,
    Item,
    Score
}
