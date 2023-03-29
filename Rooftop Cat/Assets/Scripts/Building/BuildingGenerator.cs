using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BuildingType
{
    Basic,
    PositiveSlope,
    NegativeSlope,
    AllSlope,
    Corridor,
}


public class BuildingGenerator : MonoBehaviour
{
    public GameObject buildingsParentObject;

    public GameObject previousBuildingConstructed;

    public int collectionSize;
    public float initialGap;
    public float buildingYPosition;

    public float heightBase;
    public float heightVariation;

    public List<GameObject> buildingGOList;

    public BuildingSpecs[] buildingSpecsArray;
    public float [] buildingHeightArray;
    public string[] directionArray;


    public void Start()
    {
        //GenerateBuildingSection(10);
        buildingSpecsArray = new BuildingSpecs[collectionSize];
        directionArray = new string[collectionSize];
        buildingHeightArray = new float[collectionSize];
    }



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateBuildingDirection();
            //GenerateBuildingSection();

        }
    }
    public string GetRandomBuildingHeight(bool hasReachedLimit, float limitValue)
    {
        string buildingHeightString = "";
        int randomNumber = 0;
        if (hasReachedLimit)
        {
            if (limitValue == 2)
            {
                randomNumber = Random.Range(2, 4);
            }
            else if (limitValue == -2)
            {
                randomNumber = Random.Range(1, 3);
            }
        }
        else
        {
            randomNumber = Random.Range(1, 4);
        }
        switch (randomNumber)
        {
            case 1:
                buildingHeightString = "Up";
                break;
            case 2:
                buildingHeightString = "Mid";
                break;
            case 3:
                buildingHeightString = "Down";
                break;
        }
        return buildingHeightString;
    }








    public void GenerateBuildingDirection()
    {
        int heightCounter = 0;
        bool hasReachedLimit = false;

        for (int i = 0; i < collectionSize; i++)
        {
            string currentDirection = "";
            if (i == 0)
            {
                currentDirection = "Mid";
                heightCounter = 0;
            }
            else
            {
                currentDirection = GetRandomBuildingHeight(hasReachedLimit, heightCounter);
                if (currentDirection == "Up")
                {
                    heightCounter++;
                    if (heightCounter == 2)
                    {
                        hasReachedLimit = true;
                    }
                    else
                    {
                        hasReachedLimit = false;
                    }
                }
                else if (currentDirection == "Down" && heightCounter == -2)
                {
                    heightCounter--;
                    if (heightCounter == -2)
                    {
                        hasReachedLimit = true;
                    }
                    else
                    {
                        hasReachedLimit = false;
                    }
                }
            }
            directionArray[i] = currentDirection;
            buildingHeightArray[i] = (heightCounter * heightVariation) + heightBase ;
            Debug.Log("Direction: " +  directionArray[i]);
            Debug.Log("Height: " +  buildingHeightArray[i]);
        }
    }

    public void GenerateBuildingSection()
    {
        for (int i = 0; i < collectionSize; i++)
        {
            BuildingType currentType = BuildingType.Basic;
            int randomBuildingType;
            int randomGap;
            float gap = -1;
            if (i != collectionSize - 1)
            {
                if (directionArray[i] == directionArray[i + 1])
                {
                    randomBuildingType = Random.Range(0, 5);
                    randomGap = Random.Range(1, 4);
                    gap = randomGap + 1;

                    //switch (randomBuildingType)
                    //{
                    //    case 0:
                    //        currentType = BuildingType.Basic;
                    //        break;
                    //    case 1:
                    //        currentType = BuildingType.AllSlope;
                    //        break;
                    //    case 2:
                    //        currentType = BuildingType.NegativeSlope;
                    //        break;
                    //    case 3:
                    //        currentType = BuildingType.PositiveSlope;
                    //        break;
                    //    case 4:
                    //        currentType = BuildingType.Corridor;
                    //        break;
                    //}
                }
                else
                {
                    if (directionArray[i] == "Up" || directionArray[i] == "Mid" && directionArray[i + 1] == "Down")
                    {
                        randomBuildingType = Random.Range(0, 3);
                        randomGap = Random.Range(2, 4);
                        gap = randomGap + 1;
                        //switch (randomBuildingType)
                        //{
                        //    case 0:
                        //        currentType = BuildingType.Basic;
                        //        break;
                        //    case 1:
                        //        currentType = BuildingType.AllSlope;
                        //        break;
                        //    case 2:
                        //        currentType = BuildingType.NegativeSlope;
                        //        break;
                        //}
                    }
                    else if (directionArray[i] == "Down" || directionArray[i] == "Mid" && directionArray[i + 1] == "Up")
                    {
                        randomBuildingType = Random.Range(0, 3);
                        randomGap = Random.Range(1, 3);
                        gap = randomGap + 1;
                        //switch (randomBuildingType)
                        //{
                        //    case 0:
                        //        currentType = BuildingType.Basic;                                
                        //        break;
                        //    case 1:
                        //        currentType = BuildingType.PositiveSlope;
                        //        break;
                        //    case 2:
                        //        currentType = BuildingType.Corridor;
                        //        break;
                        //}
                    }
                }
            }
            buildingSpecsArray[i] = new BuildingSpecs(buildingHeightArray[i], 5, currentType);
            CreateBuilding(buildingSpecsArray[i],previousBuildingConstructed,5);
            Debug.Log("Building["+i+"]" + " Type: " + "<"+currentType.ToString()+">" + " " + "Gap: " + gap + " /" + " Heigth: " + buildingHeightArray[i]);
        }
    }



    public GameObject GetBuildingBasedOnSpecs(BuildingSpecs buildingSpecs)
    {
        GameObject matchingObject = buildingGOList.FirstOrDefault(building => building.GetComponent<BuildingID>().height == buildingSpecs.height && building.GetComponent<BuildingID>().buildingType == buildingSpecs.buildingType);
        if (matchingObject != null)
        {
            return matchingObject;
        }
        else
        {
            return null;
        }
    }

    public void CreateBuilding(BuildingSpecs buildingSpecs,GameObject previousBuilding, float gap)
    {
        var building = GetBuildingBasedOnSpecs(buildingSpecs);
        var newBuilding = Instantiate(building, new Vector3(previousBuilding.transform.position.x + gap, previousBuilding.transform.position.y , 0.0f), Quaternion.identity);
        previousBuildingConstructed = newBuilding;
    }

    //public void GenerateBuildingRuleset(bool hasReachedThreshold, int value)
    //{

    //    // Building is either together or have space..
    //    // If there's space, add the margin
    //    //But it depends on if the height of the previous one is lower than the current one

    //    // Iteration types : 
    //    // 1. Up
    //    // 2. Mid
    //    // 3. Down

    //    // Margin types : 
    //    // 1. LowMargin
    //    // 2. MedMargin
    //    // 3. HighMargin


    //    // If : (previous iteration is lower than the current iteration)
    //    // Then : Add the margin (LowMargin)

    //    // If : (both iterations are the same size)
    //    // Then : Add the margin (LowMargin - HighMargin)

    //    // If : (previous iteration is higher than the current iteration)  
    //    // Then : Add the margin (MedMargin - HighMargin)

    //}

}
