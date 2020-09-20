using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelViewer : MonoBehaviour
{
    [SerializeField] private GameObject _avatar;
    [SerializeField] private PlayersRepository _playersRepository;
    [SerializeField] private Animator _toggleAvatarAnimator;
    [SerializeField] private Vector3 _instantiatePosition;

    private GameObject _newAvatar;
    
    public void ToggleActiveObject()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void SetNextAvatar()
    {
        _toggleAvatarAnimator.Play("ClearAvatar");
        _newAvatar = _playersRepository.GetNextAvatar();
    }

    public void SetPrevAvatar()
    {
        _toggleAvatarAnimator.Play("ClearAvatar");
        _newAvatar = _playersRepository.GetPrevAvatar();
    }

    public void ChengeAvatar()
    {
        Destroy(_avatar.gameObject);
        _avatar = Instantiate(_newAvatar, _toggleAvatarAnimator.GetComponent<Transform>());
        _avatar.transform.localPosition = _instantiatePosition;
    }
}
