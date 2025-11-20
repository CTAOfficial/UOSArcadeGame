using Glorp;
using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Life")]
    public LifeEntity Life;
    public SpriteRenderer hitCircle;

    [Header("Weapon")]
    public Weapon weapon;
    public float weaponRotationSpeed;

    [Header("Weapon")]
    public Shield shield;
    public float shieldRotationSpeed;

    bool IsFlashing;

    public static event Action<Player> OnDeath;

    void Awake()
    {
        Life.IsPlayer = true;

        Life.OnDamaged += OnDamage;
        Life.OnDeath += (_) => OnDeath?.Invoke(this);
    }
    void Start()
    {
        if (weapon && weapon.User != gameObject) { weapon.User = gameObject; }
    }
    void Update()
    {
        HandleWeapon();
        HandleShield();
        
    }

    void HandleWeapon()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Move(weapon.transform, new(0, 0, 1), weaponRotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Move(weapon.transform, new(0, 0, -1), weaponRotationSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            weapon.Attack();
        }
    }
    void HandleShield()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(shield.transform, new(0, 0, 1), weaponRotationSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(shield.transform, new(0, 0, -1), weaponRotationSpeed);
        }
    }


    void OnDamage(LifeEntity entity)
    {
        //.. hearts
        StartCoroutine(FlashCircle());
    }


    IEnumerator FlashCircle()
    {
        if (IsFlashing) { yield return null; }

        IsFlashing = true;
        hitCircle.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        hitCircle.color = Color.white;
        IsFlashing = false;
    }
    void Move(Transform target, Vector3 vector, float angle)
    {
        target.transform.RotateAround(transform.position, vector, angle * Time.deltaTime);
    }
}
