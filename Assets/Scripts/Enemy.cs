using UnityEngine;

public class Enemy : MonoBehaviour
{
    int Hp=10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Dead()
    {
        print("Blia I sdoh");
    }

    void GetDamage(int Damage)
    {
        if(Hp-Damage <= 0)
        {
            Dead();
        }
        else Hp -= Damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
