using UnityEngine;

public class Player : MonoBehaviour
{
    public Weapon weapon;

    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (weapon && weapon.User != gameObject) { weapon.User = gameObject; }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Move(new(0, 0, 1), speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Move(new(0, 0, -1), speed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Attack();
        }
    }

    void Move(Vector3 vector, float angle)
    {
        weapon.transform.RotateAround(transform.position, vector, angle * Time.deltaTime);
    }
}
