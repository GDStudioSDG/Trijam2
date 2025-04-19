using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    [SerializeField] private GameObject Shot;
    private bool delay= false;
    public bool active = false;
    [SerializeField] private float ShotPower = 0;
    // Start is called before the first frame update
    void Start()
    {
        SphereCollider sphere = gameObject.AddComponent<SphereCollider>();
        sphere.radius = 10;
        sphere.isTrigger = true;
    }
    IEnumerator Wait()
    {
        delay = true;
        yield return new WaitForSeconds(1);
        delay = false;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (!delay && collision.gameObject.tag == "Enemy")
        {
            if (active)
            {
                GameObject Ammo = Instantiate(Shot, transform.position + transform.up*2, this.transform.rotation);
                Ammo.AddComponent<Rigidbody>().AddForce(((collision.transform.position-(transform.position + transform.up*2))* ShotPower).normalized *50, ForceMode.Impulse);
                Destroy(Ammo, 5);
                StartCoroutine(Wait());
            }
        }
    }

}
