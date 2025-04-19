using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResursGenerator : MonoBehaviour
{
    [SerializeField] private GameObject wood;

    void Start()
    {
        for (float x = 0; x < 10; x++)
        {
            for(float y = 0; y < 10; y++)
            {
                if (Mathf.RoundToInt(Mathf.PerlinNoise(x * 0.2f, y * 0.2f))==1)
                {
                    Instantiate(wood,new Vector3(x, 0.5f,y),Quaternion.identity);
                }
            }
        }
    }

}
