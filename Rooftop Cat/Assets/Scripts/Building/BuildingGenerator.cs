using System.Collections;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public GameObject buildingsParentObject;
    public GameObject baseBuildingBlock;
    public float buildingWidthMin;
    public float buildingWidthMax;
    public float buildingHeightMin;
    public float buildingHeightMax;
    public float gapMin;
    public float gapMax;

    public float initialGap;
    public float buildingYPosition;

    public Building[] GenerateBuildings(float playerMaxJump, int numberOfBuildings)
    {
        Building[] buildings = new Building[numberOfBuildings];
        float currentHeight = 0;

        for (int i = 0; i < numberOfBuildings; i++)
        {
            float buildingWidth = Random.Range(buildingWidthMin, buildingWidthMax);
            float buildingHeight = Random.Range(buildingHeightMin, buildingHeightMax);
            float gap = Random.Range(gapMin, gapMax);

            if (currentHeight + buildingHeight + gap > playerMaxJump)
            {
                // If the building is too high for the player to jump over, generate a smaller building
                buildingHeight = playerMaxJump - currentHeight - gap;
            }

            buildings[i] = new Building(buildingWidth, buildingHeight);

            currentHeight += buildingHeight + gap;
        }

        return buildings;
    }

    public void CreateInitialGap(GameObject buildingBlock)
    {
        var building = Instantiate(buildingBlock, new Vector3(initialGap + Random.Range(gapMin, gapMax), buildingYPosition, 0.0f), Quaternion.identity);
    }

    public void Start()
    {
        CreateInitialGap(baseBuildingBlock);
    }
}

public class Building
{
    private float width;
    private float height;

    public Building(float width, float height)
    {
        this.width = width;
        this.height = height;
    }

    // Other properties and methods here...
}