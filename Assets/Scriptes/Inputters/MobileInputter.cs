using UnityEngine;

//класс джойстика, просто игроку указывается объект с которого читать данные, может и с него самого и дальше просто вызываются нужные методы
public class MobileInputter : MonoBehaviour, IInputter
{
    [SerializeField] private Player _player;
    
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            PlayerTurn(Camera.main.ScreenToViewportPoint(Input.mousePosition));
        }
    }

    private void PlayerTurn(Vector3 ScreenToViewportPoint)
    {
        RotateDirection direction = ScreenToViewportPoint.x < 0.5f ? RotateDirection.Left : RotateDirection.Right;
        _player.Turn(direction);
    }
}
