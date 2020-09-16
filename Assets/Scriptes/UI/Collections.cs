using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collections : MonoBehaviour
{
    [SerializeField] private GameObject _runners;
    [SerializeField] private GameObject _companions;
    [SerializeField] private Button _runnersButton;
    [SerializeField] private Button _companionsButton;

    [SerializeField] private Color _currentColor;


    private Image _currentImage;

    public void OpenRunners()
    {
        SetActiveObject(_runners, _companions);
        SilectButtonViewer(_runnersButton);
    }

    public void OpenCompanions()
    {
        SetActiveObject(_companions, _runners);
        SilectButtonViewer(_companionsButton);
    }

    public void ChooseRunners()
    {
        SaveDataStorage.SaveCurrentRunners();
        Debug.Log("save runners");
    }

    private void SetActiveObject(GameObject activeObject, params GameObject[] inactiveObject)
    {
        activeObject.SetActive(true);

        foreach (var inactive in inactiveObject)
        {
            inactive.SetActive(false);
        }
    }

    private void SilectButtonViewer(Button currentButton)
    {
        if (_currentImage != null)
            _currentImage.color = Color.white;

        _currentImage = currentButton.GetComponent<Image>();
        _currentImage.color = _currentColor;

    }

}
