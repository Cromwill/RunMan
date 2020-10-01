
using UnityEngine;

[CreateAssetMenu(fileName = "new Booster", menuName = "Booster SO")]
public class Booster : ScriptableObject
{
    [SerializeField] private BoosterType _boosterType;
    [SerializeField] private float _changingValue;
    [SerializeField] private float _price;

    public BoosterType Type => _boosterType;
    public float Value => _changingValue;
    public float Price => _price;
}

public enum BoosterType
{
    Speed,
    Damage,
    Item,
    Score
}
