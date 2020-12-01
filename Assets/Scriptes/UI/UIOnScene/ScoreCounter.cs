﻿using UnityEngine;

public class ScoreCounter : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private ScoreTerms _scoreTerms;
    [SerializeField] private ScoreVieweronlevel _scoreViewer;
    [SerializeField] private BoosterType _type;

    private Vector3 _oldPosition;
    private float _scoreMultiplier = 1;

    public int score => (int)(_scoreTerms.DistanceToScore(distance) * _scoreMultiplier);
    public float distance { get; private set; }
    public int money => _scoreTerms.ScoreToMoney(score);
    public BoosterType BoosterType => _type;

    public void Initialization(params Booster[] boosters)
    {
        _oldPosition = PositionColculation(transform.position);

        if (boosters != null)
        {
            foreach (Booster booster in boosters)
                UsedBooster(booster);
        }
    }

    public void UsedSkill(SkillData skill, int count)
    {
    }
    public void DistanceColculate()
    {
        Vector3 currentPosition = PositionColculation(transform.position);
        distance += Vector3.Distance(currentPosition, _oldPosition);
        _oldPosition = currentPosition;

        if (_scoreViewer != null)
            _scoreViewer.ShowDistance(distance);

        AddScore(score);
    }

    public void AddScore(int value)
    {
        if (_scoreViewer != null)
            _scoreViewer.ShowScore(value);
    }

    private void UsedBooster(Booster booster)
    {
        _scoreMultiplier += booster.Value;
    }

    private Vector3 PositionColculation(Vector3 objectPosition) => new Vector3(objectPosition.x, 0, objectPosition.z);
}

