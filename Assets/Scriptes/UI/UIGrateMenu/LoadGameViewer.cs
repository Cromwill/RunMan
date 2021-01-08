using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameViewer : MonoBehaviour
{
    [SerializeField] private Text _progressViewer;

    public void ShowProgress(int progress)
    {
        _progressViewer.text = progress.ToString() + " %";

        if(progress >= 100)
        {
            StartCoroutine(CloaseLoadPanel());
        }
    }

    private IEnumerator CloaseLoadPanel()
    {
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
