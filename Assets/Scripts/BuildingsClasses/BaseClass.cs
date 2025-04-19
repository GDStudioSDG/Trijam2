using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : Building
{
    public List<Building> ingibitors = new List<Building>();
    private void Start()
    {
        TurnManager.NextTurn += NextTurn;
        //Res = GameObject.FindWithTag("Player1").GetComponent<Resurses>();
    }

    private void NextTurn()
    {
        foreach (Building building in ingibitors)
        {
            if (building != null)
            {
                if (Hp+1 <= MaxHp) Hp++; ;
            }
        }
    }
}
