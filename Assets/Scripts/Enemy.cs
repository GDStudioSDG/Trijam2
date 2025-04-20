using System.Collections.Generic;
using System.Net;
using Towers;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, ITowerInteract
{
    [SerializeField]protected Type enemyType = Type.Earth;
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

    public void TakeDamage(int damage, Type damageType)
    {
        int damageToDeal = damage / 2;
        switch (enemyType)
        {
            case Type.Earth:
                if (damageType == Type.Water)
                    damageToDeal = damage;
                break;
            
            case Type.Water:
                if(damageType == Type.Electricity)
                    damageToDeal = damage;
                break;
            
            case Type.Fire:
                if(damageType == Type.Earth)
                    damageToDeal = damage;
                break;
            
            case Type.Electricity:
                if(damageType == Type.Fire)
                    damageToDeal = damage;
                break;
        }
        
        if(Hp-damageToDeal <= 0)
        {
            Dead();
        }
        else Hp -= damageToDeal;
    }
    
}
