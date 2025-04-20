using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Towers
{
    public class FireTower : Tower
    {
        protected override Type damageType{ get; set; } = Type.Fire;
        [SerializeField]private GameObject fire;
        [SerializeField] private int fireTimer = 3;
        private bool bIsFiring = false;
        [SerializeField] private float capsuleRadius = 2;
        
        
        private void FireShot()
        {
            
            bIsFiring = true;
            StartCoroutine(FireTimer());
        }

        protected override void Atack()  
        {
            if(!bIsFiring)
                FireShot();
        }

        private IEnumerator FireTimer()
        {
            fire.SetActive(true);
            for (int i = 0; i < fireTimer; i++)
            {
                Collider[] allOverlappingColliders = Physics.OverlapCapsule(transform.position + transform.forward,
                    transform.position + transform.forward * _radius, capsuleRadius);
                if (allOverlappingColliders.Length > 0)
                    foreach (Collider collider in allOverlappingColliders)
                        if(collider.CompareTag("Enemy"))
                            CauseDamage(collider.gameObject);
                yield return new WaitForSeconds(1);
            }
            bIsFiring = false;
            fire.SetActive(false);
        }
    }
}
