using UnityEngine;

public class RunnersAvatar : MonoBehaviour
{
    [SerializeField] private bool _isPresentation;

    private void Update()
    {
        if (_isPresentation)
            transform.Rotate(0, 5, 0, Space.World);
    }
}
