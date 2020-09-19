using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AchievementNotification : MonoBehaviour
{
    [SerializeField] private Achievements _achievements;
    [SerializeField] private float _appearanceTime;
    [SerializeField] private float _disappearanceTime;
    [SerializeField] private float _displayTime;
    [Header("Background")]
    [SerializeField] private CanvasGroup _backgroundGroup;
    [Header("Achievement")]
    [SerializeField] private Image _achievementImage;
   // [SerializeField] private TMP_Text _achievementLabel;
   // [SerializeField] private TMP_FontAsset _font;
    [Header("Effect")]
    [SerializeField] private ParticleSystem _fireworks;

    private void OnEnable()
    {
        _achievements.AchievementReached += OnAchievementReached;
    }

    private void OnDisable()
    {
        _achievements.AchievementReached -= OnAchievementReached;
    }

    private void OnAchievementReached(AchievementData achievement)
    {
       // _achievementImage.sprite = achievement.Icon;
       // _achievementLabel.text = achievement.Label;
       // _achievementLabel.font = _font;

        ShowAchievement(achievement);
    }

    private void ShowAchievement(AchievementData achievement)
    {
        var currentScale = Time.timeScale;

      //  DOTween.To(() => _backgroundGroup.alpha, x => _backgroundGroup.alpha = x, 1, _appearanceTime).SetUpdate(true);
      //  DOTween.To(() => _backgroundGroup.alpha, x => _backgroundGroup.alpha = x, 0, _disappearanceTime).SetDelay(_displayTime).SetUpdate(true);
    }
}
