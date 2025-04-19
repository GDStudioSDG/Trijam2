using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);


    private Building[,] grid;
    private Building flyingBuilding;
    private Camera mainCamera;

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];

        mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        flyingBuilding = Instantiate(buildingPrefab);
    }

    private void Update()
    {
        if (flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = true;

                if (x < transform.position.x || x > transform.position.x + GridSize.x - flyingBuilding.Size.x) available = false;
                if (y < transform.position.z || y > transform.position.z + GridSize.y - flyingBuilding.Size.y) available = false;

                if (available && IsPlaceTaken(x- Mathf.RoundToInt(transform.position.x), y- Mathf.RoundToInt(transform.position.z))) available = false;

                flyingBuilding.transform.position = new Vector3(x, 0, y);
                flyingBuilding.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(x - Mathf.RoundToInt(transform.position.x), y - Mathf.RoundToInt(transform.position.z));
                }
            }
        }
    }


    private void SmejTest()
    {
        flyingBuilding.GetComponent<Collider>().enabled = false;
        Vector3 Centre = new Vector3(flyingBuilding.transform.position.x + flyingBuilding.Size.x / 2f-0.5f, 0f, flyingBuilding.transform.position.z + flyingBuilding.Size.y / 2f-0.5f);
        Vector3 HalfScale = new Vector3(flyingBuilding.Size.x/2f+0.5f,0,flyingBuilding.Size.y/2f+0.5f);
        Collider[] hitColliders = Physics.OverlapBox(Centre, HalfScale, Quaternion.identity);
        //for(int i = 0; i < hitColliders.Length; i++) print(hitColliders[i]);
        flyingBuilding.GetComponent<Building>().LevelUpOnStay(hitColliders);
        flyingBuilding.GetComponent<Collider>().enabled = true;
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                if (grid[placeX + x, placeY + y] != null) return true;//если место занято
            }
        }

        return false;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                grid[placeX + x, placeY + y] = flyingBuilding;
            }
        }
        flyingBuilding.SetNormal();
        SmejTest();
        //flyingBuilding.GetComponent<TowerShooter>().active = true;
        flyingBuilding = null;
        /*
smej = false;
for (int x = 0; x < flyingBuilding.Size.x; x++)
{
    if (grid[placeX + x, placeY + flyingBuilding.Size.y + 1] != null) smej = true;
    if (grid[placeX + x, placeY - 1] != null) smej = true;
}
for (int y = 0; y < flyingBuilding.Size.y; y++)
{
    if (grid[placeX + flyingBuilding.Size.x + 1, placeY + y] != null) smej = true;
    if (grid[placeX + 1, placeY + y] != null) smej = true;
}

if (smej == true) { print("avalible"); }
*/
    }
    public void PlaceFlyingBuilding(int placeX, int placeY,Building building)
    {
        for (int x = 0; x < building.Size.x; x++)
        {
            for (int y = 0; y < building.Size.y; y++)
            {
                grid[placeX + x, placeY + y] = building;
            }
        }
    }
}
