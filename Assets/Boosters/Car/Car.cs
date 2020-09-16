using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform wheelFD, wheelFP;
    Rigidbody rb;
    public float speed = 50f;
    public float steerAngle = 25f;
    public float steerSpeed = 30f;
    public JoyStick joystick;
    bool aligned = true;
    public int fuel = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartMove();
    }
    private void FixedUpdate()
    {
        if (joystick.placed)
        {
            if (aligned) aligned = false;
            float eulers = joystick.RotateEulers(steerSpeed);
            Debug.Log(eulers);
            transform.Rotate(new Vector3(0, eulers, 0));
            Quaternion quat = new Quaternion();
            quat.eulerAngles = new Vector3(0, 90 + eulers * steerAngle, 0);
            wheelFD.localRotation = quat;
            wheelFP.localRotation = quat;
        }
        else
        {
            Align();
        }
        rb.velocity = transform.forward * speed;
        
    }

    public void StartMove()
    {
        StartCoroutine(FuelRate());
    }
    public IEnumerator FuelRate()
    {

        while (speed != 0)
        {
            yield return new WaitForSeconds(0.6f);
            fuel--;
            if (fuel == 0)
            {
                speed = 0;
            }
        }
    }
    void Align()
    {
        if (!aligned)
        {
            aligned = true;
            Quaternion quat = new Quaternion();
            quat.eulerAngles = new Vector3(0, 90, 0);
            wheelFD.localRotation = quat;
            wheelFP.localRotation = quat;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Ground")
        {
            speed = 0;
        }
    }
}
