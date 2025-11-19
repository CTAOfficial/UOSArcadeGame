using Blazers;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Life")]
    public LifeEntity Life;

    [Header("Weapon")]
    public Weapon weapon;
    public float rotationSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (weapon && weapon.User != gameObject) { weapon.User = gameObject; }

        Life.OnDamaged += OnDamage;
    }

    void OnDamage(LifeEntity entity)
    {
        //.. hearts
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Move(new(0, 0, 1), rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Move(new(0, 0, -1), rotationSpeed);
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
