using Blazers.Combat;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    public int damage;
    public float speed;
    public Rigidbody2D rb;

    public IDamageable Creator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!rb) { rb = GetComponent<Rigidbody2D>();}
    }

    // Update is called once per frame
    void Update()
    {

        rb.linearVelocity = new(direction.x * speed, direction.y * speed);
        //transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable) && damageable != Creator)
        {
            if (damageable.TryDamage(damage))
            {
                Destroy(gameObject);
            }
        }
    }

    public void Initialize(Transform transform)
    {
        direction = transform.position;

        transform.gameObject.TryGetComponent(out Creator);
    }
}
