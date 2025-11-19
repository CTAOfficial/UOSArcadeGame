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

    public Projectile Attack()
    {
        Projectile proj = Instantiate(projectile, firePoint.position, firePoint.rotation).GetComponent<Projectile>();
        proj.Initialize(this);
        return proj;
    }
}
