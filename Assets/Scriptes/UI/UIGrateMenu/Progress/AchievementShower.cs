using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementShower : MonoBehaviour
{
    [SerializeField] private Achievement _achievement;
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _progressShower;

    private void OnEnable()
    {
        if (_slider != null)
            Show();
    }

    private void Show()
    {
        if(_achievement.ID == 1)
        {

        }

        int value = (int)_achievement.GetCurrentValue();

        for (int i = 0; i < _achievement.Condition.Length; i++)
        {
            if (value < _achievement.Condition[i])
            {
                _progressShower.text = value + " / " + (int)_achievement.Condition[i];
                _slider.value = 1 / _achievement.Condition[i] * value;
                return;
            }
        }

    }
}
