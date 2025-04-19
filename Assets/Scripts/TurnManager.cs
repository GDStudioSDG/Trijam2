using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turn;
    [SerializeField] private GameObject mainBase;
    [SerializeField] private GameObject ingibitor;

    public static Action NextTurn;
    public static Action ResUpdate;


    public static void SendNextTurn()
    {
        if (NextTurn != null) NextTurn.Invoke();
        if (ResUpdate != null) ResUpdate.Invoke();

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {SendNextTurn(); turn++; }
    }

    public void GameStart(int playersCount)
    {
        baseSpawn(1);
        GameObject Res = GameObject.FindWithTag("Player1");
        Res.transform.Find("WoodFon").gameObject.SetActive(true);
        Res.transform.Find("WaterFon").gameObject.SetActive(true);
        Res.transform.Find("StoneFon").gameObject.SetActive(true);
        Res.transform.Find("PlayseBuilding").gameObject.SetActive(true);
    }

    private void baseSpawn(int numberOfBase)
    {
        GameObject point = GameObject.FindWithTag(numberOfBase.ToString());
        BuildingsGrid Grid = point.GetComponent<BuildingsGrid>();
        BaseClass buildingGrid = mainBase.GetComponent<BaseClass>();
        Vector2Int Gridsize = Grid.GridSize;
        Vector3 position = point.transform.position + new Vector3Int(Mathf.RoundToInt(Gridsize.x / 2- buildingGrid.Size.x / 2), 0, Mathf.RoundToInt(Gridsize.y / 2 -buildingGrid.Size.y / 2));
        BaseClass MainBuild = Instantiate(mainBase,position,Quaternion.identity).GetComponent<BaseClass>();
        Grid.PlaceFlyingBuilding(Mathf.RoundToInt(position.x) - Mathf.RoundToInt(point.transform.position.x), Mathf.RoundToInt(position.z) - Mathf.RoundToInt(point.transform.position.z), MainBuild);
        GameObject.FindWithTag("Player1").transform.Find("PlayseBuilding").GetComponent<RandomBuilding>().mainGrid = Grid;

        Building ingBuildingGrid = ingibitor.GetComponent<Building>();
        Vector3 ingPosition =position + new Vector3(Mathf.RoundToInt(buildingGrid.Size.x - ingBuildingGrid.Size.x/2),0, buildingGrid.Size.y);
        Building build = Instantiate(ingibitor, ingPosition, Quaternion.identity).GetComponent<Building>();
        Grid.PlaceFlyingBuilding(Mathf.RoundToInt(ingPosition.x) - Mathf.RoundToInt(point.transform.position.x), Mathf.RoundToInt(ingPosition.z) - Mathf.RoundToInt(point.transform.position.z), build);

        MainBuild.ingibitors.Add(build);

        ingPosition = position + new Vector3(Mathf.RoundToInt(-1- ingBuildingGrid.Size.x / 2), 0, buildingGrid.Size.y);
        build = Instantiate(ingibitor, ingPosition, Quaternion.identity).GetComponent<Building>();
        Grid.PlaceFlyingBuilding(Mathf.RoundToInt(ingPosition.x) - Mathf.RoundToInt(point.transform.position.x), Mathf.RoundToInt(ingPosition.z) - Mathf.RoundToInt(point.transform.position.z), build);
        MainBuild.ingibitors.Add(build);

        ingPosition = position + new Vector3(Mathf.RoundToInt(buildingGrid.Size.x - ingBuildingGrid.Size.x / 2), 0, -buildingGrid.Size.y);
        build = Instantiate(ingibitor, ingPosition, Quaternion.identity).GetComponent<Building>();
        Grid.PlaceFlyingBuilding(Mathf.RoundToInt(ingPosition.x) - Mathf.RoundToInt(point.transform.position.x), Mathf.RoundToInt(ingPosition.z) - Mathf.RoundToInt(point.transform.position.z), build);
        MainBuild.ingibitors.Add(build);

        ingPosition = position + new Vector3(Mathf.RoundToInt(-1 - ingBuildingGrid.Size.x / 2), 0, -buildingGrid.Size.y);
        build = Instantiate(ingibitor, ingPosition, Quaternion.identity).GetComponent<Building>();
        Grid.PlaceFlyingBuilding(Mathf.RoundToInt(ingPosition.x) - Mathf.RoundToInt(point.transform.position.x), Mathf.RoundToInt(ingPosition.z) - Mathf.RoundToInt(point.transform.position.z), build);
        MainBuild.ingibitors.Add(build);

    }
}
