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
        private void Start()
        {
            _transform = gameObject.GetComponent<Transform>();
            _sphere = gameObject.GetComponent<SphereCollider>();
            _radius = _sphere.radius;
        }

        private void FixedUpdate()
        {
            if( _lockedEnemy) _transform.LookAt(_lockedEnemy.transform);
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
                Collider[] allOverlappingColliders = Physics.OverlapSphere(_sphere.transform.localPosition, _radius);
                foreach (Collider c in allOverlappingColliders)
                {
                    Debug.Log(c.name);
                }
            }
        }
        public void Unlock()
        {
            _bIsLocked = false;
            _lockedEnemy = null;
        }

        
    }
}
