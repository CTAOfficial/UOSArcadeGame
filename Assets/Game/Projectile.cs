using System.Collections;
using Blazers.Combat;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public Rigidbody2D rb;
    public bool enemybullet = false;
    public IDamageable Creator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!rb) { rb = GetComponent<Rigidbody2D>();}
        StartCoroutine(Cull());
    }

    // Update is called once per frame
    void Update()
    {
        if (enemybullet)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            rb.linearVelocity = new(transform.position.x * speed, transform.position.y * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable) && damageable != Creator)
        {
            if (damageable.TryDamage(damage))
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Cull()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
    public void Initialize(Weapon weapon)
    {
        //direction = transform.position;
        weapon.User.TryGetComponent(out Creator);
    }
    
}
