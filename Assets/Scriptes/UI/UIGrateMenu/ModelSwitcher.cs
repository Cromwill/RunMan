using UnityEngine;
using UnityEngine.UI;

public class ModelSwitcher : MonoBehaviour
{
    [SerializeField] private ModelViewer _modelViewer;
    [SerializeField] private bool _isShop;
    [SerializeField] private Text _priceViewer;
    [SerializeField] private BuyPanel _buyPanel;

    private bool _isSold;
    private void OnEnable()
    {
        _modelViewer.ToggleActiveObject(_isShop, this);
        if (_isShop)
            _buyPanel.PurchaseСonfirmed += _modelViewer.ChangePrice;
    }

    private void OnDisable()
    {
        if (_isShop)
            _buyPanel.PurchaseСonfirmed -= _modelViewer.ChangePrice;

        _modelViewer.ToggleActiveObject(_isShop, this);
    }

    public void ChangePrice(float price, bool isSold = false)
    {
        _isSold = isSold;
        string priceText = _isSold ? "sold" : price.ToString("0.##") + "$";
        _priceViewer.text = priceText;
    }

    public void BuyAvatar()
    {
        _buyPanel.OpenPanel(_modelViewer.RunnerAvatar, _isSold);
    }
}
