using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButtons : MonoBehaviour
{
    private Slider _selfSlider;

    public void ShowPanel(int sliderValue)
    {
        gameObject.SetActive(true);

        if (_selfSlider == null)
            _selfSlider = GetComponent<Slider>();
        _selfSlider.value = sliderValue;
    }
}
