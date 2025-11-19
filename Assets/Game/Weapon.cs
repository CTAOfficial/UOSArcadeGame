using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject User;
    public GameObject projectile;
    public Transform firePoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Projectile proj = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Projectile>();
        proj.Initialize(firePoint.transform);
    }
    public void EnemyAttack(Quaternion rotation)
    {
        Projectile proj = Instantiate(projectile, firePoint.position, Quaternion.identity).GetComponent<Projectile>();
        proj.Initialize(firePoint.transform);
        proj.enemybullet = true;
        proj.transform.rotation = rotation;
    }
}
