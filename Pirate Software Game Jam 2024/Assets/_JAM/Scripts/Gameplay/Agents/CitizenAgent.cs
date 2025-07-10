using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

/// <summary>
/// Controls the behavior and needs of a citizen agent in the game.
/// </summary>
public class CitizenAgent : MyMonoBehaviour
{
    // Config properties
    private GameplayConfig GameplayConfig => ConfigManager.Instance.GetConfig<GameplayConfig>();

    public GenerationTest generationTestScript;
    public PathfindingTest pathfindingTestScript;

    public GameObject modelObject;

    [SerializeField] int duty, sanity, health;

    public Citizen citizen;

    private int buildingChecks = 0;

    private float gridXOffset = 0;
    private float gridZOffset = 0;
    private float drainTimer = 0;
    private float checkTimer = 0;

    private bool isGainingHealth = false;
    private bool isGainingSanity = false;
    private bool isGainingDuty = false;
    private bool isMoving = false;

    private Building destination = null;
    private Building occupyingBuilding = null;

    private Vector2[] currentPath;

    private IGridManager gridManager;
    private IPathfindingService pathfindingTest;

    /// <summary>
    /// Unity Start method. Initializes citizen needs and randomizes some parameters.
    /// </summary>
    void Start()
    {
        citizen.CitizenNeeds.CalculateNeeds(citizen.Sanity, citizen.Health, citizen.FactionDuty);
        checkTimer = Random.Range(0, 1f);
        buildingChecks = Random.Range(0, GameplayConfig.BuildingChecksPerMove + 1);
        float checkBuildingTime = Random.Range(0, 0.15f);
        gridXOffset = Random.Range(0, 0.25f);
        gridZOffset = Random.Range(0, 0.25f);
    }

    /// <summary>
    /// Unity FixedUpdate method. Handles movement, stat draining, and building checks.
    /// </summary>
    private void FixedUpdate()
    {
        if (destination == null)
        {
            destination = GetNextDestination();
            occupyingBuilding = gridManager.GetBuildingAt(citizen.housePosition);
            modelObject.SetActive(false);
        }

        citizen.CitizenNeeds.CalculateNeeds(citizen.Sanity, citizen.Health, citizen.FactionDuty);
        sanity = citizen.Sanity;
        health = citizen.Health;
        duty = citizen.FactionDuty;
        DrainStats();

        checkTimer += Time.fixedDeltaTime;
        if (checkTimer >= GameplayConfig.CheckBuildingTime)
        {
            checkTimer = 0;

            // Apply stat bonuses based on building type
            switch (destination.buildingType)
            {
                case BuildingType.Faction:
                    citizen.FactionDuty += 5;
                    break;
                case BuildingType.Health:
                    citizen.Health += 5;
                    break;
                case BuildingType.Sanity:
                    citizen.Sanity += 5;
                    break;
                default:
                    //Do Nothing
                    break;
            }

            if (!isMoving)
            {
                buildingChecks++;
                if (buildingChecks > GameplayConfig.BuildingChecksPerMove)
                {
                    buildingChecks = 0;
                    destination = GetNextDestination();
                    if (destination != occupyingBuilding)
                    {
                        // Find a path to the new destination and start moving
                        currentPath = pathfindingTest.FindPath(new Vector2(transform.position.x, transform.position.z), destination.gridPositions[0]);
                        StartCoroutine(MoveCitizen());
                        print(destination.name);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Coroutine to move the citizen along the current path.
    /// </summary>
    private IEnumerator MoveCitizen()
    {
        isMoving = true;
        modelObject.SetActive(true);

        for (int i = currentPath.Length - 1; i >= 0; i--)
        {
            transform.DOMove(new Vector3(currentPath[i].x, 0, currentPath[i].y), GameplayConfig.MoveTime);
            yield return new WaitForSeconds(GameplayConfig.MoveTime);
        }

        transform.position = new Vector3(destination.gridPositions[0].x + gridXOffset, 0, destination.gridPositions[0].y + gridZOffset);
        occupyingBuilding = destination;

        modelObject.SetActive(false);
        isMoving = false;
    }

    /// <summary>
    /// Drains the citizen's stats over time, applying additional penalties if needed.
    /// </summary>
    private void DrainStats()
    {
        drainTimer += Time.fixedDeltaTime;

        if (drainTimer >= 1f)
        {
            drainTimer = 0f;

            if (citizen.Health > 0 && !isGainingHealth)
            {
                citizen.Health -= GameplayConfig.NaturalHealthDrainPerSecond;
            }

            if (citizen.FactionDuty > 0 && !isGainingDuty)
            {
                citizen.FactionDuty -= GameplayConfig.NaturalDutyDrainPerSecond;
            }

            if (citizen.Sanity > 0 && !isGainingSanity)
            {
                citizen.Sanity -= GameplayConfig.NaturalSanityDrainPerSecond;
                if (citizen.CitizenNeeds.dutyRatio < 0.25f)
                {
                    citizen.Sanity -= GameplayConfig.DutySanityDrainPerSecond;
                }
            }

            sanity = citizen.Sanity;
            health = citizen.Health;
            duty = citizen.FactionDuty;
        }
    }

    /// <summary>
    /// Determines the next building destination for the citizen based on their needs.
    /// </summary>
    /// <returns>The next building to move to.</returns>
    private Building GetNextDestination()
    {
        BuildingType buildingType = BuildingType.House;

        // Prioritize needs: health, then duty, then sanity
        if (citizen.CitizenNeeds.healthRatio < 0.5f)
        {
            buildingType = BuildingType.Health;
        }
        else if (citizen.CitizenNeeds.dutyRatio < 0.5f)
        {
            buildingType = BuildingType.Faction;
        }
        else if (citizen.CitizenNeeds.sanityRatio < 0.5f)
        {
            buildingType = BuildingType.Sanity;
        }

        if (buildingType == BuildingType.House)
        {
            return gridManager.GetBuildingAt(citizen.housePosition);
        }
        else
        {
            return gridManager.GetRandomBuildingByType(buildingType);
        }
    }

    /// <summary>
    /// Sets the house position for the citizen.
    /// </summary>
    /// <param name="_housePos">The new house position.</param>
    public void SetHousePosition(Vector2 _housePos)
    {
        citizen.housePosition = _housePos;
    }

    public void Init(IGridManager gridManager, IPathfindingService pathfindingTest)
    {
        this.gridManager = gridManager;
        this.pathfindingTest = pathfindingTest;
    }
}
