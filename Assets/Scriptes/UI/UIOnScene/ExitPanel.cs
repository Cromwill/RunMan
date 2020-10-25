﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] private Text _scoreValue;
    [SerializeField] private Text _distanceValue;

    [SerializeField] private Button _exit;
    [SerializeField] private Button _replay;
    [SerializeField] private Button _back;

    private void Start()
    {
        _back.onClick.AddListener(ClosePanel);
        _exit.onClick.AddListener(Exit);
        _replay.onClick.AddListener(Replay);
    }

    public void OpenPanel(bool isGameOver)
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        _back.gameObject.SetActive(!isGameOver);
    }

    public void ShowDatas(float score, float distance)
    {
        _scoreValue.text = score.ToString("0.0#");
        _distanceValue.text = distance.ToString("0.0#");
    }

    private void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void Replay()
    {
        SceneManager.LoadScene(1);
    }

    private void ClosePanel()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}