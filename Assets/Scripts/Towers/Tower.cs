using System;
using UnityEngine;

namespace Towers
{
    public class Tower : MonoBehaviour
    {
        private float _radius;
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
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy") && !_bIsLocked)
            {
                _bIsLocked = true;
                _lockedEnemy = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _lockedEnemy)
            {
                Unlock();
            }
        }
        public void Unlock()
        {
            float minDistance = float.MaxValue;
            GameObject nextEnemy = null;
            Collider[] allOverlappingColliders = Physics.OverlapSphere(_sphere.transform.localPosition, _radius);
            
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
                _bIsLocked = true;
                _lockedEnemy = nextEnemy.gameObject;
            }
            else
            {
                _bIsLocked = false;
                _lockedEnemy = null;
            }
        }

        
    }
}
