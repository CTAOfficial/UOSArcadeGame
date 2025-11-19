using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform firePoint;
    public GameObject tower;
    public GameObject projectile;

    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Move(new(0, 0, 1), speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(new(0, 0, -1), speed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Projectile proj = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Projectile>();
            proj.Initialize(transform);
        }
    }

    void Move(Vector3 vector, float angle)
    {
        transform.RotateAround(tower.transform.position, vector, angle * Time.deltaTime);
    }
}
