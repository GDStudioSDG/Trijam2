using UnityEngine;
using UnityEngine.Splines;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

public class EnemyShedule : MonoBehaviour
{
    [SerializeField] List<SplineContainer> Splines;
    [SerializeField] GameObject PEnemy;
    [SerializeField] static int Time = 0;
    int RandomLine;
    bool[] LinesActive = new bool[4];
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private int RandomVichet()
    {
        int RandNum = Random.Range(0, 4);
        if (LinesActive[RandNum])
        {
            return RandomVichet();
        }
        else
        {
            LinesActive[RandNum] = true;
            return RandNum;
        }
    }
    public static int GetTime()
    {
        return Time;
    }
    IEnumerator Tik()
    {
        while (true)
        {
            //print(Time);
            if (Time == 0)
            {
                RandomVichet();
            }
            if (Time == 30)
            {
                RandomVichet();
            }
            if (Time == 60)
            {
                RandomVichet();
            }
            if (Time == 70)
            {
                RandomVichet();
            }
            if (Time % 3 == 0)
            {
                int i = 0;
                foreach (bool lineStat in LinesActive)
                {
                    if (lineStat)
                    {
                        GameObject Enemy = Instantiate(PEnemy);
                        Enemy.GetComponent<SplineAnimate>().Container = Splines[i];
                        //print(Random.Range(0, 4));
                        Enemy.GetComponent<SplineAnimate>().Play();
                        int RandType = Random.Range(0, 4);
                        Enemy.GetComponent<Enemy>().SetEnemyType((Type)RandType);
                    }
                    i++;
                }  
            }
            
                Time++;
            yield return new WaitForSeconds(1f);
        }
    }
    private void Start()
    {
        StartCoroutine(Tik());
    }
}
