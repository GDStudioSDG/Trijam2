using System.Collections.Generic;
using Towers;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, ITowerInteract
{
    [SerializeField] int Hp=10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Dead()
    {
        print("Blia I sdoh");
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if(Hp-damage <= 0)
        {
            Dead();
        }
        else Hp -= damage;
    }
    
}
