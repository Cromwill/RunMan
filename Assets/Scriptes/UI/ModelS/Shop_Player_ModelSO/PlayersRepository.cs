using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Players Repository", menuName = "Repository")]
public class PlayersRepository : ScriptableObject
{
    [SerializeField] GameObject[] _playerAvatars;

    private int _currentIndex = 0;

    public GameObject GetAvatar(int index)
    {
        if (index < _playerAvatars.Length || index >= 0)
            _currentIndex = index;

        return _playerAvatars[_currentIndex];
    }

    public GameObject GetNextAvatar() => GetAvatar(_currentIndex + 1);
    public GameObject GetPrevAvatar() => GetAvatar(_currentIndex - 1);
}
