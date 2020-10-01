using UnityEngine;
using UnityEngine.UI;

public class Collections : PanelActivator
{
    [SerializeField] private Image[] _buttonImages;
    [SerializeField] private Color _selectedColor;

    public void ChooseRunners()
    {
        //SaveDataStorage.SaveCurrentRunners();
        Debug.Log("save runners");
    }

    public void SelectedButton(Image currentImage)
    {
        currentImage.color = _selectedColor;

        foreach(var image in _buttonImages)
        {
            if (image != currentImage)
                image.color = Color.white;
        }
    }
}
