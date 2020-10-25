﻿using UnityEngine;
using System;

[CreateAssetMenu(fileName = "new Score Terms", menuName = "ScoreTerms")]
public class ScoreTerms : ScriptableObject
{
    [SerializeField] private float[] _distance;
    [SerializeField] private int[] _score;

    public int DistanceToScore(float distance)
    {
        int score = 0;

        for(int i = 0; i < _distance.Length; i++)
        {
            score += (int)(distance / _distance[i]) * _score[i];
        }

        return score;
    }
}


