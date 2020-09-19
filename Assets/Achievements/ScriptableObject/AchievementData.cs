using UnityEngine;

[CreateAssetMenu(fileName = "New AchievementData", menuName = "Achievement Data", order = 51)]
public class AchievementData : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _label;
    [SerializeField] private int _currentLevel;
    [SerializeField] private string _description;
    [SerializeField] private Level[] _levels;
    [SerializeField] private Sprite _icon;

    public int Id => _id;
    public string Label => _label;
    public int CurrentLevel => _currentLevel;
    public string Description => _description;
    public Level[] Levels => _levels;
    public Sprite Icon => _icon;

    public void AddLevel()
    {
        if (_currentLevel < _levels.Length - 1)
            _currentLevel++;
    }
}