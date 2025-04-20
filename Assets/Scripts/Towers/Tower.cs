using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;

namespace Towers
{
    public class Tower : MonoBehaviour
    {
        protected virtual Type damageType { get; set; } = Type.None;
        [SerializeField] protected float attackSpeed = 1;
        [SerializeField]protected int towerDamage = 10;
        protected float _radius;
        private Transform _transform;
        private SphereCollider _sphere;
        private bool _bIsLocked = false;
        private GameObject _lockedEnemy;
        [SerializeField]private float rotationSpeed = 5;
        private void Start()
        {
            _transform = gameObject.GetComponent<Transform>();
            _sphere = gameObject.GetComponent<SphereCollider>();
            _radius = _sphere.radius;
        }


        private void FixedUpdate()
        {
            if (_lockedEnemy)
            {
                var targetRotation = Quaternion.LookRotation(_lockedEnemy.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                Unlock();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy") && !_bIsLocked)
            {
                Lock(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _lockedEnemy)
            {
                Unlock();
            }
        }

        private void Lock(GameObject otherGameObject)
        {
            _bIsLocked = true;
            _lockedEnemy = otherGameObject;
            StartCoroutine(AutoAtackCourotine());
        }
        public void Unlock()
        {
            float minDistance = float.MaxValue;
            GameObject nextEnemy = null;
            Collider[] allOverlappingColliders = Physics.OverlapSphere(_sphere.transform.localPosition, _radius);
            if(allOverlappingColliders.Length > 0)
                foreach (Collider c in allOverlappingColliders)
                {
                    if(!c.gameObject.CompareTag("Enemy")) continue;
                    float distance = (c.transform.position - _transform.position).magnitude;
                    if (minDistance > distance)
                    {
                        minDistance = distance;
                        nextEnemy = c.gameObject;
                    }
                }
            if (nextEnemy)
            {
                Lock(nextEnemy.gameObject);
            }
            else
            {
                _bIsLocked = false;
                _lockedEnemy = null;
            }
        }

        protected void CauseDamage()
        {
            if (_lockedEnemy)
            {
                _lockedEnemy.GetComponent<ITowerInteract>().TakeDamage(towerDamage, damageType);
                Debug.Log("Damaged");
            }
        }

        protected void CauseDamage(GameObject other)
        {
            if (other)
            {
                other.GetComponent<ITowerInteract>().TakeDamage(towerDamage, damageType);
                Debug.Log("Damaged");
            }
        }

        protected GameObject GetLockedEnemy()
        {
            return _lockedEnemy;
        }

        protected virtual void Atack()
        {
            
        }
        private IEnumerator AutoAtackCourotine()
        {
            while (_bIsLocked)
            {
                Atack();
                yield return new WaitForSeconds(1 / attackSpeed);
            }
        }
    }
}
