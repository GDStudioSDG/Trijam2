using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Towers
{
    public class WaterTower : Tower
    {
        protected override Type damageType{ get; set; } = Type.Water;
        [SerializeField]private GameObject water;
        [SerializeField]private float projectileSpeed = 10f;
        private void WaterShot()
        {
            GameObject shot = Instantiate(water, transform.position + transform.forward, Quaternion.identity);
            StartCoroutine(ProjectileFly(shot));
        }

        protected override void Atack()  
        {
            WaterShot();
        }
        

        private IEnumerator ProjectileFly(GameObject projectile)
        {
            GameObject target = GetLockedEnemy();
            Vector3 targetPos =  target.transform.position;
            while (Vector3.Distance(targetPos, projectile.transform.position) > 0.3f && target != null)
            {
                targetPos =  target.transform.position;
                projectile.transform.position += (targetPos - projectile.transform.position).normalized *  Time.deltaTime * projectileSpeed;
                projectile.transform.LookAt(targetPos);
                yield return null;
            }

            CauseDamage(target);
            Destroy(projectile);
        }
    }
}
