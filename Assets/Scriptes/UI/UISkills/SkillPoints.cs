using UnityEngine;
using UnityEngine.UI;

public class SkillPoints : MonoBehaviour
{
    [SerializeField] private SkillData _skillData;
    [SerializeField] private Slider _slider;

    private Skills _skills;
    private int _savedSkillValue;

    public int Coast { get; private set; }
    public string SkillKey => _skillData.skillKey;
    public int SkillValue => (int)_slider.value;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ValueChange);
        if (_skills == null)
            _skills = GetComponentInParent<Skills>();
        _savedSkillValue = SaveDataStorage.LoadSkills(SkillKey);
        DataReset();
    }

    public void ValueChange(float value)
    {
        Coast = _skillData.GetPrice((int)_slider.value);
        if (_slider.value < _savedSkillValue)
            Coast *= -1;
        else if (_slider.value == _savedSkillValue)
            Coast = 0;

        _skills.ShowTotal();
    }

    public void DataReset()
    {
        _slider.value = SaveDataStorage.LoadSkills(SkillKey);
        ValueChange(0);
    }

    public void DataSave()
    {
        SaveDataStorage.SaveSkills(_skillData.skillKey, SkillValue);
        _savedSkillValue = SkillValue;
        ValueChange(0);
    }
}
