using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimberClass : Building
{
    public int Cost = 5;

    public override void LevelUpOnStay(Collider[] others)
    {
        for (int i = 0; i < others.Length; i++)
        {
            if (others[i].gameObject.GetComponent<TimberClass>() != null)
            {
                Lv++;
                others[i].gameObject.GetComponent<TimberClass>().Lv++;
            }
        }
    }
    private void Start()
    {
        TurnManager.NextTurn += NextTurn;
        Res = GameObject.FindWithTag("Player1").GetComponent<Resurses>();
    }
    private void NextTurn()
    {
        Res.AddWood(Lv);
    }

}
