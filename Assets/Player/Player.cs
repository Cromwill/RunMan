using UnityEngine;

//класс отвечающий за действия персонажа во время игрвоого процесса
public class Player : MonoBehaviour
{
    public int ControlType = 1;
    public float speed = 5F;
    public float rotationSpeed = 200F;
    public float jumpForce = 20f;
    public JoyStick joystick;

    float angle;
    CapsuleCollider cd;
    Rigidbody rb;
    bool isAlive = true;
    Animator animator;
    AnimationClip clip;

    Touch touch;
    Vector2 tStart, tEnd;
    float Hm;

    public void Reset()
    {
        speed = 5F;
        rotationSpeed = 100F;
        jumpForce = 20f;
        transform.position = new Vector3(0,5,0);
        transform.rotation = new Quaternion(0,0,0, 0);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cd = GetComponent<CapsuleCollider>();
    }

    void FixedUpdate()
    {
        if (!GameController.Paused)
        {

            if (joystick.placed)
            {
                transform.Rotate(new Vector3(0, joystick.RotateEulers(rotationSpeed), 0));
            }
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
            //rb.AddForce(new Vector3(0, -40, 0), ForceMode.Acceleration);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "DeadZone":
                if (!animator.GetBool("Jump_b"))
                {
                    animator.SetBool("Jump_b", true);
                    rb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
                    animator.SetBool("Grounded", false);
                    speed = 20f;
                    //isAlive = false;
                    cd.enabled = false;
                    //CameraController.Active = false;
                    //DeadZone.Active = false;
                }
                break;
            case "RunArea":
                if (isAlive)
                {
                    animator.SetBool("Jump_b", false);
                    animator.SetBool("Grounded", true);
                    cd.enabled = true;
                    speed = 10f;
                    CCamera.Active = true;
                }
                break;
            case "Base":
                Debug.Log(cd.enabled);
                if (!isAlive)
                    Destroy(gameObject);
                //GameUIControl.LeaveGame();
                break;
        }
    }
}

[System.Serializable]
public class PlayerData
{
    int highScore;
    int bestDistance;

    public PlayerData(int hs, int bd)
    {
        highScore = hs;
        bestDistance = bd;
    }
}