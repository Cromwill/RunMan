using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "new Players Repository", menuName = "Repository")]
public class PlayersRepository : ScriptableObject
{
    [SerializeField] GameObject[] _playerAvatars;

    private int _currentIndex = 0;

    public int maxAvatarsCount => _playerAvatars.Length;

    public GameObject GetAvatar(int index)
    {
        if (index < _playerAvatars.Length && index >= 0)
            _currentIndex = index;

        return _playerAvatars[_currentIndex];
    }

    public GameObject GetAvatarFromId(int id)
    {
        return _playerAvatars.Where(a => a.GetComponent<RunnersAvatar>().Id == id).First(); ;
    }

    public GameObject GetNextAvatar() => GetAvatar(_currentIndex + 1);
    public GameObject GetPrevAvatar() => GetAvatar(_currentIndex - 1);
}
