using UnityEngine;

public class CCamera : MonoBehaviour
{
    public Transform target;
    public static bool Active = true;


    void LateUpdate()
    {
        if (Active)
        {
            transform.position = new Vector3(target.transform.position.x, 0, target.transform.position.z);
            //transform.rotation = Quaternion.Lerp(transform.rotation, runner.transform.rotation, 0.5f);
        }
    }
}
