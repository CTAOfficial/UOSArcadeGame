using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.GraphicsBuffer;


namespace Glorp.Enemies
{
    public class Enemy : LifeEntity
    {
        public Weapon weapon;
        public Transform Target;
        //public float Range;
        public float fireCooldown;
        public float Range;
        public float Speed;
        public float rotationSpeed = 5;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (Player.Instance) { Target = Player.Instance.transform; }
            OnCreated?.Invoke(this);
            GameManager.OnGameEnd += Die;

            if (weapon)
            {
                weapon.cooldown = fireCooldown;
            }
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.DrawLine(transform.position, transform.forward);

            if (CheckTarget())
            {
                LookAtTarget();

                float distance = Vector3.Distance(transform.position, Target.position);
                if (distance > Range)
                {
                    Move();
                    return;
                }

                if (CheckTargetIsInView()) { TryAttack(); }
                
            }
        }

        bool CheckTarget()
        {
            if (!Target) { return false; }
            return true;
        }
        void LookAtTarget()
        {
            //Vector3 vec = Target.position - transform.position;
            //transform.rotation = Quaternion.LookRotation(vec);

            float angle = Mathf.Atan2(Target.position.y - transform.position.y, Target.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        bool CheckTargetIsInView()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, Range, transform.position);
            
            for (int i = 0; hits.Length > i; i++)
            {
                RaycastHit2D hit = hits[i];

                if (hit.transform == Target)
                {
                    Debug.DrawLine(transform.position, hit.transform.position);
                    Debug.Log("bog");
                    return true;
                }
            }

            
            Debug.Log("mog");
            return false;
        }

        void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
        void TryAttack()
        {
            if (weapon && weapon.CanFire)
            {
                weapon.TryAttack();
            }
        }


        private void OnDrawGizmosSelected()
        {
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Range);

            /*if (Target)
            {
                Debug.DrawLine(transform.position, Target.position);
            }*/
        }
    }

    
}