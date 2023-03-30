using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingGeneratorChatGPT : MonoBehaviour
{


    public GameObject buildingsParentObject;

    public GameObject previousBuildingConstructed;

    [SerializeField] private int collectionSize;
    [SerializeField] private float initialGap;
    [SerializeField] private float buildingYPosition;

    [SerializeField] private float heightBase;
    [SerializeField] private float heightVariation;

    [SerializeField] private List<GameObject> buildingGOList = new List<GameObject>();

    private BuildingSpecs[] buildingSpecsArray;
    private float[] buildingHeightArray;
    private string[] directionArray;

    BuildingType[] buildingTypes = new BuildingType[]
    {
            BuildingType.Basic,
            BuildingType.AllSlope,
            BuildingType.NegativeSlope,
            BuildingType.PositiveSlope,
            BuildingType.Corridor
    };



    private void Awake()
    {
        buildingSpecsArray = new BuildingSpecs[collectionSize];
        directionArray = new string[collectionSize];
        buildingHeightArray = new float[collectionSize];
        GenerateBuildingDirection();
        GenerateBuildingSection();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateBuildingDirection();
            GenerateBuildingSection();
        }
    }


    private string GetRandomBuildingHeight(bool hasReachedLimit, float limitValue)
    {
        int randomNumber = hasReachedLimit switch
        {
            true when limitValue == 2 => Random.Range(2, 4),
            true when limitValue == -2 => Random.Range(1, 3),
            _ => Random.Range(1, 4),
        };

        return randomNumber switch
        {
            1 => "Up",
            2 => "Mid",
            3 => "Down",
            _ => string.Empty
        };
    }


    private void GenerateBuildingDirection()
    {
        int heightCounter = 0;
        bool hasReachedLimit = false;

        for (int i = 0; i < collectionSize; i++)
        {
            string currentDirection = i == 0 ? "Mid" : GetRandomBuildingHeight(hasReachedLimit, heightCounter);

            if (currentDirection == "Up")
            {
                heightCounter++;
                hasReachedLimit = heightCounter == 2;
            }
            else if (currentDirection == "Down")
            {
                heightCounter--;
                hasReachedLimit = heightCounter == -2;
            }
            else
            {
                hasReachedLimit = false;
            }

            directionArray[i] = currentDirection;
            buildingHeightArray[i] = (heightCounter * heightVariation) + heightBase;

            //Debug.Log($"Direction: {directionArray[i]}");
            //Debug.Log($"Height: {buildingHeightArray[i]}");
        }
    }

    public void GenerateBuildingSection()
    {
        for (int i = 0; i < collectionSize; i++)
        {
            BuildingType currentType = BuildingType.Basic;
            float gap = -1;

            if (i == collectionSize - 1)
            {
                buildingSpecsArray[i] = new BuildingSpecs(buildingHeightArray[i], 5, currentType);
                CreateBuilding(buildingSpecsArray[i], previousBuildingConstructed, 5);
                continue;
            }

            if (directionArray[i] == directionArray[i + 1])
            {
                currentType = (BuildingType)Random.Range(0, 5);
                gap = Random.Range(1, 4) + 1;
            }
            else if (directionArray[i] == "Up" || (directionArray[i] == "Mid" && directionArray[i + 1] == "Down"))
            {
                currentType = (BuildingType)Random.Range(0, 3);
                gap = Random.Range(2, 4) + 1;
            }
            else if (directionArray[i] == "Down" || (directionArray[i] == "Mid" && directionArray[i + 1] == "Up"))
            {
                currentType = (BuildingType)Random.Range(0, 3);
                gap = Random.Range(1, 3) + 1;
            }
            currentType = BuildingType.Basic;
            buildingSpecsArray[i] = new BuildingSpecs(buildingHeightArray[i], 3.5f, currentType);
            CreateBuilding(buildingSpecsArray[i], previousBuildingConstructed, 3.5f);
        }
    }

    public void CreateBuilding(BuildingSpecs buildingSpecs, GameObject previousBuilding, float gap)
    {
        var building = GetBuildingBasedOnSpecs(buildingSpecs);
        if (building != null)
        {
            var position = new Vector3(previousBuilding.transform.position.x + gap, previousBuilding.transform.position.y, 0.0f);
            previousBuildingConstructed = Instantiate(building, position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Building not found");
        }
    }

    public GameObject GetBuildingBasedOnSpecs(BuildingSpecs buildingSpecs)
    {
        return buildingGOList.FirstOrDefault(building =>
            building.GetComponent<BuildingID>().height == buildingSpecs.height &&
            building.GetComponent<BuildingID>().buildingType == buildingSpecs.buildingType);
    }


}