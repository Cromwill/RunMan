using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private ScoreTerms _scoreTerms;
    private Vector3 _oldPosition;
    private ScoreVieweronlevel _scoreViewer;

    public int score => _scoreTerms.DistanceToScore(distance);
    public float distance { get; private set; }

    public int money => _scoreTerms.ScoreToMoney(score);

    public void Initialization(ScoreVieweronlevel scoreViewer)
    {
        _oldPosition = PositionColculation(transform.position);
        _scoreViewer = scoreViewer;
    }

    public void DistanceColculate()
    {
        Vector3 currentPosition = PositionColculation(transform.position);
        distance += Vector3.Distance(currentPosition, _oldPosition);
        _oldPosition = currentPosition;

        _scoreViewer.ShowDistance(distance);

        AddScore(score);
    }

    public void AddScore(int value)
    {
        _scoreViewer.ShowScore(value);
    }

    private Vector3 PositionColculation(Vector3 objectPosition)
    {
        return new Vector3(objectPosition.x, 0, objectPosition.z);
    }
}

