using UnityEngine;

public class EndGameObserver : MonoBehaviour
{
    [SerializeField] private ExitPanel _exitPanel;

    private Player _player;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _player.Deading += GameOver;
    }

    private void GameOver()
    {
        _exitPanel.OpenPanel(true);
        _exitPanel.ShowDatas(_player.scoreCounter.score, _player.scoreCounter.distance);
    }
}
