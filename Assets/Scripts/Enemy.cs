using System.Collections.Generic;
using Towers;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int Hp=10;
    private List<Tower> _aggredTowers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Dead()
    {
        foreach (Tower tower in _aggredTowers)
        {
            tower.Unlock();
        }
        print("Blia I sdoh");
    }

    public void GetDamage(int Damage)
    {
        if(Hp-Damage <= 0)
        {
            Dead();
        }
        else Hp -= Damage;
    }

    public void AddTowers(Tower tower)
    {
        if (tower) _aggredTowers.Add(tower);
    }

    public void RemoveTowers(Tower tower)
    {
        _aggredTowers.Remove(tower);
    }
}
