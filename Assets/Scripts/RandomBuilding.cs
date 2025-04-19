using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBuilding : MonoBehaviour
{
    public List<GameObject> buildingList = new List<GameObject>();
    public BuildingsGrid mainGrid;

    public void RandomBuy()
    {
        int a = Random.Range(0,buildingList.Count);
        mainGrid.StartPlacingBuilding(buildingList[a].GetComponent<Building>());
    }
}
