using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwitcher : MonoBehaviour
{
    [SerializeField] private ModelViewer _modelViewer;

    private void OnEnable()
    {
        _modelViewer.ToggleActiveObject();
    }

    private void OnDisable()
    {
        _modelViewer.ToggleActiveObject();
    }
}
