using System.Collections;
using Glorp.Combat;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public Rigidbody2D rb;
    public IDamageable Creator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.OnGameEnd += () => Destroy(gameObject);

        if (!rb) { rb = GetComponent<Rigidbody2D>();}
        StartCoroutine(Cull());
    }
    void OnDestroy()
    {
        GameManager.OnGameEnd -= () => Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // check if creator exists still and store the tag
        if (collision.TryGetComponent(out IDamageable damageable) && 
            damageable != Creator &&
            damageable.tag != Creator.tag
            )
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
