using UnityEngine;

public class PlayerScoreCounter : MonoBehaviour
{
    [SerializeField] private PlayerScoreViewer _scoreViewer;

    private int _money;
    private int _coins;

    private void Awake()
    {
        var score = SaveDataStorage.LoadScore();
        _money = score.Money;
        _coins = score.Coins;

        _scoreViewer.ShowScore(score);
    }

    public bool ReduceScore(Score score)
    {
        if (score.Money <= _money && score.Coins <= _coins)
        {
            _money -= score.Money;
            _coins -= score.Coins;

            Score changedScore = new Score(_money, _coins);
            SaveDataStorage.SaveScore(changedScore);
            _scoreViewer.ShowScore(changedScore);
            return true;
        }
        else
            return false;
    }

    public void SaveBuyableObject(IBuyableObject buyable)
    {
        SaveDataStorage.SaveBuyableObject(buyable);
    }

    public void AddMoney(int value)
    {
        _money += value;
        ReduceScore(new Score(0, 0));
    }

    public void Clean()
    {
        PlayerPrefs.DeleteAll();
    }
}
