using UnityEngine;
using UnityEngine.UI;

public class GrateButtons : MonoBehaviour
{
    [SerializeField] private TabButtons _tabButtons;
    
    public void PlayLevel()
    {

    }

    public void ShowGarage()
    {
        _tabButtons.ShowPanel(-1);
    }

    public void ShowShop()
    {
        _tabButtons.ShowPanel(1);
    }


}
