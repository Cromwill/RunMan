using System.Linq;
using UnityEngine;

public class BarrierGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _barrierPrefab;

    [Range(0, 100)]
    [SerializeField] private int _useLineChance;
    [Range(0, 100)]
    [SerializeField] private int _barrierCountChance;

    [SerializeField] private int _minBarrierPosition;
    [SerializeField] private int _maxBarrierPosition;

    private const int _lineChance = 100;
    private const int _barrierChance = 100;

    public void GenerateBarriers(float zPosition)
    {
        int linechance = Random.Range(0, _lineChance);
        GameObject[] checkBarriers = new GameObject[_maxBarrierPosition - _minBarrierPosition + 1];
        int checkBarriersIndex = 0;

        if(_useLineChance > linechance)
        {
            for(int i = _minBarrierPosition; i < _maxBarrierPosition; i++)
            {
                int barrierChance = Random.Range(0, _barrierChance);
                if(_barrierCountChance >= barrierChance)
                {
                    checkBarriers[checkBarriersIndex] = Instantiate(_barrierPrefab, new Vector3(i, 0.5f, zPosition), Quaternion.identity);
                }
                else
                {
                    checkBarriers[checkBarriersIndex] = null;
                }
                checkBarriersIndex++;
            }
        }
    }
}
