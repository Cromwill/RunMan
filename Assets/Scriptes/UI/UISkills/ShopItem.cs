using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Booster _booster;

    private Text _priceViewer;

    private void OnEnable()
    {
        if (_priceViewer == null)
            _priceViewer = GetComponentInChildren<Text>();
        _priceViewer.text = _booster.Price.ToString("0.##") + " $";
    }
}
