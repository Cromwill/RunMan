using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement/add Achievement")]
public class Achievement : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [Multiline]
    [SerializeField] private string _description;
    [SerializeField] private bool _isRecord;

    [SerializeField] private float[] _conditions;

    public int ID => _id;
    public string Name => _name;
    public string Description => _description;
    public bool IsRecord => _isRecord;

    public float[] Condition => _conditions;

    public void AddValue(float value)
    {
        AchievementDataStorage.AddAchievement(this, value);
    }

    public float GetCurrentValue()
    {
        return AchievementDataStorage.GetAchievementValue(this);
    }
}
