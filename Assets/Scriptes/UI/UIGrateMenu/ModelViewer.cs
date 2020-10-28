using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelViewer : MonoBehaviour
{
    [SerializeField] private GameObject _avatar;
    [SerializeField] private PlayersRepository _playersRepository;
    [SerializeField] private Animator _toggleAvatarAnimator;
    [SerializeField] private Vector3 _instantiatePosition;

    private ModelSwitcher _switcher;
    private GameObject _newAvatar;
    private bool _isShop;
    private List<int> _openedAvatars = new List<int>();

    public RunnersAvatar RunnerAvatar => _avatar.GetComponent<RunnersAvatar>();

    public void ToggleActiveObject(bool isShop, ModelSwitcher switcher)
    {
        gameObject.SetActive(!gameObject.activeSelf);
        _isShop = isShop;

        if (gameObject.activeSelf)
            _switcher = switcher;

        if (_isShop)
        {
            if (_avatar == null)
            {
                _avatar = Instantiate(_playersRepository.GetAvatar(0), _toggleAvatarAnimator.GetComponent<Transform>());
                _avatar.transform.localPosition = _instantiatePosition;
            }
            ChangePrice();
        }
        else
        {
            if (_avatar == null)
            {
                _avatar = Instantiate(_playersRepository.GetAvatarFromId(SaveDataStorage.LoadCurrentRunnersId()), _toggleAvatarAnimator.GetComponent<Transform>());
                _avatar.transform.localPosition = _instantiatePosition;
            }
        }
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
        ChangePrice();
    }

    public void ChangePrice()
    {
        var selfAvatars = SaveDataStorage.LoadOpenedRunnersIds(_playersRepository.maxAvatarsCount);
        RunnersAvatar runnersAvatar = _avatar.GetComponent<RunnersAvatar>();
        bool isSold = selfAvatars.Contains(runnersAvatar.Id);
        _switcher.ChangePrice(runnersAvatar.Price, isSold);
    }
}
