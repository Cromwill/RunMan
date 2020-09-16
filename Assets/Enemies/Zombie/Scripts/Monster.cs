using UnityEngine;

//общий класс монстров для огра и зомби
public class Monster : MonoBehaviour
{
    public int hits;
    public int speed;

    private Transform player;
    

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    
    void FixedUpdate()
    {
        Vector3 pDir = Vector3.Normalize(player.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, pDir, out hit, 10))
        {
            if (hit.collider.tag == "Obstacles")
                pDir -= Vector3.Normalize(hit.collider.bounds.center - transform.position);
        }
        pDir.y = player.transform.position.y;
        
        transform.LookAt(pDir.normalized * speed);
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

}
