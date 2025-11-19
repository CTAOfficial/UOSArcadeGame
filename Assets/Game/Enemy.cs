using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace Blazers.Enemies
{
    public class Enemy : LifeEntity
    {
        public Weapon weapon;
        public Transform Target;
        //public float Range;
        public float fireCooldown;
        public float Range;
        public float Speed;
        public bool canFire;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Target = FindFirstObjectByType<Player>().transform;
        }

        // Update is called once per frame
        void Update()
        {
            Debug.DrawLine(transform.forward, Vector3.one);

            if (CheckTarget())
            {
                float distance = Vector3.Distance(transform.position, Target.position);
                if (distance > Range)
                {
                    Move();
                    return;
                }
                if (canFire) { TryAttack(); }
                
            }
        }

        bool CheckTarget()
        {
            if (!Target) { return false; }
            return true;
        }
        void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
        void TryAttack()
        {
            if ((weapon)&&(canFire))
            {
                var vec = Target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(vec);
              

                // weapon.transform.RotateAround(transform.position, );

                //float angle = Mathf.Atan2(Target.position.y - transform.position.y, Target.position.x - transform.position.x) * Mathf.Rad2Deg;
                //Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
                //transform.rotation = targetRotation; //Quaternion.RotateTowards(transform.rotation, targetRotation, 100);

                weapon.EnemyAttack(lookRotation);
                canFire = false;
                StartCoroutine(Cooldown());
            }
        }
        IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(fireCooldown);
            canFire = true;
        }

        private void OnDrawGizmosSelected()
        {
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, Range);

            if (Target)
            {
                Debug.DrawLine(transform.position, Target.position);
            }
        }
    }

    
}