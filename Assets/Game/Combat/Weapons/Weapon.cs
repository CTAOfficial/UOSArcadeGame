using System.Collections;
using Glorp;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class Weapon : MonoBehaviour
{
    public GameObject User;
    public GameObject projectile;
    public Transform firePoint;
    public AudioSource FireSound;
    public float cooldown = 0.5f;

    public bool CanFire { get => _canFire; private set => _canFire = value; }
    bool _canFire = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public bool TryAttack()
    {
        if (CanFire)
        {
            CanFire = false;
            Attack();
            StartCoroutine(Cooldown());
            return true;
        }
        return false;
    }
    public Projectile Attack()
    {
        Projectile proj = Instantiate(projectile, firePoint.position, firePoint.rotation).GetComponent<Projectile>();
        proj.Initialize(this);
        if (FireSound) { FireSound.Play(); }
        return proj;
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        CanFire = true;
    }
}
