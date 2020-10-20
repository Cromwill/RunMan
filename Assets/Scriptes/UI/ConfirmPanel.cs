﻿using UnityEngine;
using UnityEngine.UI;

public class ConfirmPanel : MonoBehaviour
{
    [SerializeField] private Image _itemViewer;
    [SerializeField] private Text _itemName;
    [SerializeField] private Text _itemDescription;
    [SerializeField] private Image _currencyType;
    [SerializeField] private Text _price;

    [SerializeField] private Sprite[] _currencyTypes;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;

    [SerializeField] private PlayerScoreCounter _scoreCounter;
    [SerializeField] private Sprite[] _buyButtonImage;

    private IItem _currentItem;
    private Animator _selfAnimator;
    public void ShowPanel(IItem item)
    {
        gameObject.SetActive(true);

        _itemViewer.sprite = item.GetItemViewer;
        _itemName.text = item.GetItemName;
        _itemDescription.text = item.GetItemDescription;
        _currencyType.sprite = _currencyTypes[(int)item.CurrencyType];
        _price.text = (item as IBuyableObject).Price.ToString("0.##");
        _currentItem = item;
        Sprite currentButtonSprite = SaveDataStorage.ItemContain(item) ? _buyButtonImage[1] : _buyButtonImage[0];
        _confirmButton.GetComponent<Image>().sprite = currentButtonSprite;
        SetConfirmButtonInteractable();
    }

    public void ClosePanel() => gameObject.SetActive(false);

    public void ProofOfPurchase()
    {

        if (_scoreCounter.ReduceScore(GetItemScore(_currentItem)))
        {
            _scoreCounter.SaveBuyableObject(_currentItem as IBuyableObject); // не забыть поправить
            _confirmButton.GetComponent<Image>().sprite = _buyButtonImage[1];
            if (_selfAnimator == null)
                _selfAnimator = GetComponent<Animator>();
            _selfAnimator.Play("Success");
            SetConfirmButtonInteractable();
        }

    }

    private Score GetItemScore(IItem item)
    {
        int price = (int)(item as IBuyableObject).Price;
        return item.CurrencyType == CurrencyType.Coin ? new Score(0, price) : new Score(price, 0);
    }

    private void SetConfirmButtonInteractable() => _confirmButton.interactable = !SaveDataStorage.ItemContain(_currentItem);
}

public enum CurrencyType
{
    Money = 0,
    Coin = 1
}