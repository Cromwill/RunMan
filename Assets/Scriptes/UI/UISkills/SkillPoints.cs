using UnityEngine;
using UnityEngine.UI;

public class SkillPoints : MonoBehaviour
{
    [SerializeField] private SkillData _skillData;
    [SerializeField] private Slider _slider;
    [SerializeField] private SkillType _skillType;

    private Skills _skills;

    public int CurrentValue { get; private set; }
    public SkillType SkillType => _skillType;
    public string SkillKey => _skillData.skillKey;
    public int SkillValue => (int)_slider.value;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ValueChange);
        if (_skills == null)
            _skills = GetComponentInParent<Skills>();

        SetValue(SaveDataStorage.LoadSkills(_skillData.skillKey));

        Score score1 = new Score(30, 10);
        Score score2 = new Score(20, 20);
    }

    public void ValueChange(float value)
    {
        CurrentValue =  _skillData.GetPrice((int)_slider.value);
        _skills.ShowTotal();
    }

    public void SetValue(int value)
    {
        _slider.value = value;
        CurrentValue = value;
        _skills.ShowTotal();
    }
}
