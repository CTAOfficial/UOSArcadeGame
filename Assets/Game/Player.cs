using Glorp;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public static event Action<Player> OnDeath;

    [Header("Life")]
    public LifeEntity Life;
    public SpriteRenderer hitCircle;

    [Header("Weapon")]
    public Weapon weapon;
    public float weaponRotationSpeed;

    [Header("Shield")]
    public Shield shield;
    public float shieldRotationSpeed;

    bool IsFlashing;


    void Awake()
    {
        if (Instance) { Destroy(gameObject); }
        Instance = this;

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
        if (weapon) { HandleWeapon(); }
        if (shield) { HandleShield(); }
    }
    void OnDestroy()
    {
        Instance = null;
    }

    void HandleWeapon()
    {
        if (Input.GetKey(KeyCode.A) || (Gamepad.current != null && Gamepad.current.leftStick.left.isPressed))
        {
            Move(weapon.transform, new(0, 0, 1), weaponRotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D) || (Gamepad.current != null && Gamepad.current.leftStick.right.isPressed))
        {
            Move(weapon.transform, new(0, 0, -1), weaponRotationSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) || (Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame))
        {
            weapon.TryAttack();
        }
    }
    void HandleShield()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || (Gamepad.current != null && Gamepad.current.rightStick.left.isPressed))
        {
            //Move(shield.transform, new(0, 0, 1), weaponRotationSpeed);
            Move(shield.transform, new(0, 0, 1), weaponRotationSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || (Gamepad.current != null && Gamepad.current.rightStick.right.isPressed))
        {
            //Move(shield.transform, new(0, 0, -1), weaponRotationSpeed);
            Move(shield.transform, new(0, 0, -1), weaponRotationSpeed);
        }
    }


    void OnDamage(LifeEntity entity)
    {
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
