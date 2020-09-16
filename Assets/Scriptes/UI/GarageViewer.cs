using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GarageViewer : MonoBehaviour
{
    [SerializeField] private GameObject _collection;
    [SerializeField] private GameObject _skills;
    [SerializeField] private GameObject _progress;

    public void ShowCollection()
    {
        gameObject.SetActive(true);
        SetActiveObject(_collection, _skills, _progress);
    }

    public void ShowSkills()
    {
        gameObject.SetActive(true);
        SetActiveObject(_skills, _collection, _progress);
    }

    public void ShowProgress()
    {
        gameObject.SetActive(true);
        SetActiveObject(_progress, _skills, _collection);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    private void SetActiveObject(GameObject activeObject, params GameObject[] inactiveObject)
    {
        activeObject.SetActive(true);

        foreach(var inactive in inactiveObject)
        {
            inactive.SetActive(false);
        }
    }

}
