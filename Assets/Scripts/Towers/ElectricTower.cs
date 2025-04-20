using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Towers
{
    public class ElectricTower : Tower
    {
        protected override Type damageType{ get; set; } = Type.Electricity;
        [SerializeField]private float shokRaius = 5;
        [SerializeField] private int chainCount = 3;
        [SerializeField]private GameObject electricProjectile;
        [SerializeField]private float projectileSpeed = 2f;
        private void ElectricShot()
        {
            GameObject shot = Instantiate(electricProjectile, transform.position + transform.forward, Quaternion.identity);
            StartCoroutine(ProjectileFly(shot));
        }

        protected override void Atack()  
        {
            ElectricShot();
        }
        

        private IEnumerator ProjectileFly(GameObject projectile)
        {
            Vector3 targetPos = Vector3.zero;
            GameObject target = GetLockedEnemy();
            if (target != null)
            {
                targetPos = target.transform.position;
                while (Vector3.Distance(targetPos, projectile.transform.position) > 0.3f && target != null)
                {
                    targetPos = target.transform.position;
                    projectile.transform.position += (targetPos - projectile.transform.position).normalized * Time.deltaTime * projectileSpeed;
                    projectile.transform.LookAt(targetPos);
                    yield return null;
                }
                MakeChain(target);
                Destroy(projectile);
            }
        }

        private void MakeChain(GameObject target)
        {
            if (target != null)
            {
                List<GameObject> shockedEnemes = new List<GameObject>();
                Vector3 localPos = target.transform.localPosition;
                CauseDamage(target);
                shockedEnemes.Add(target);
                for (int i = 0; i < chainCount; i++)
                {
                    bool shocked = false;
                    Collider[] allOverlappingColliders = Physics.OverlapSphere(localPos, shokRaius);
                    if (allOverlappingColliders.Length > 0)
                    {
                        foreach (var enemyCollider in allOverlappingColliders)
                        {
                            if (enemyCollider.CompareTag("Enemy") && !shockedEnemes.Contains(enemyCollider.gameObject))
                            {
                                CauseDamage(enemyCollider.gameObject);
                                shockedEnemes.Add(enemyCollider.gameObject);
                                shocked = true;
                                break;
                            }
                        }
                    }
                    if (!shocked) break;
                }
            }
        } 
    }
}
