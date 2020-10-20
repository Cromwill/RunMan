using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Booster _booster;

    private Button _buyButton;
    private Text _priceViewer;

    public event Action<IItem> ItemChosen;

    private void OnEnable()
    {
        if (_priceViewer == null)
            _priceViewer = GetComponentInChildren<Text>();
        if (_buyButton == null)
            _buyButton = GetComponentInChildren<Button>();

        _buyButton.onClick.AddListener(OnItemChosen);
        _priceViewer.text = _booster.GetItemPrice.ToString("0.##") + " $";
    }

    private void OnItemChosen()
    {
        ItemChosen?.Invoke(_booster);
    }
}
