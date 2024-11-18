using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Core.Components;
using Base.Core.Managers;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace Base.Gameplay
{
    public class CitizenAgent : MyMonoBehaviour
    {
        public int buildingChecksPerMove;
        public float checkBuildingTime;

        public float moveTime;

        public int dutySanityDrainPerSecond;

        public int naturalHealthDrainPerSecond;
        public int naturalSanityDrainPerSecond;
        public int naturalDutyDrainPerSecond;

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

        void Start()
        {
            citizen.CitizenNeeds.CalculateNeeds(citizen.Sanity, citizen.Happiness, citizen.FactionDuty);
            checkTimer = Random.Range(0, 1f);
            buildingChecks = Random.Range(0, buildingChecksPerMove + 1);
            checkBuildingTime += Random.Range(0, 0.15f);
            gridXOffset = Random.Range(0, 0.25f);
            gridZOffset = Random.Range(0, 0.25f);
        }

        private void FixedUpdate()
        {
            if (destination == null)
            {
                destination = GetNextDestination();
                occupyingBuilding = generationTestScript.buildingGrid[citizen.housePosition];
                modelObject.SetActive(false);
            }

            citizen.CitizenNeeds.CalculateNeeds(citizen.Sanity, citizen.Happiness, citizen.FactionDuty);
            sanity = citizen.Sanity;
            health = citizen.Happiness;
            duty = citizen.FactionDuty;
            DrainStats();

            checkTimer += Time.fixedDeltaTime;
            if (checkTimer >= checkBuildingTime)
            {
                checkTimer = 0;

                switch (destination.buildingType)
                {
                    case BuildingType.Faction:
                        citizen.FactionDuty += 5;
                        break;
                    case BuildingType.Health:
                        citizen.Happiness += 5;
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
                    if (buildingChecks > buildingChecksPerMove)
                    {
                        buildingChecks = 0;
                        destination = GetNextDestination();
                        if (destination != occupyingBuilding)
                        {
                            currentPath = pathfindingTestScript.FindPath(new Vector2(transform.position.x, transform.position.z), destination.gridPositions[0]);
                            StartCoroutine(MoveCitizen());
//                            print(destination.name);
                        }
                    }
                }
            }
        }

        private IEnumerator MoveCitizen()
        {
            isMoving = true;
            modelObject.SetActive(true);

            for (int i = currentPath.Length - 1; i >= 0; i--)
            {
                transform.DOMove(new Vector3(currentPath[i].x, 0, currentPath[i].y), moveTime);
                yield return new WaitForSeconds(moveTime);
            }

            transform.position = new Vector3(destination.gridPositions[0].x + gridXOffset, 0, destination.gridPositions[0].y + gridZOffset);
            occupyingBuilding = destination;

            modelObject.SetActive(false);
            isMoving = false;
        }

        private void DrainStats()
        {
            drainTimer += Time.fixedDeltaTime;

            if (drainTimer >= 1f)
            {
                drainTimer = 0f;

                if (citizen.Happiness > 0 && !isGainingHealth)
                {
                    citizen.Happiness -= naturalHealthDrainPerSecond;
                }

                if (citizen.FactionDuty > 0 && !isGainingDuty)
                {
                    citizen.FactionDuty -= naturalDutyDrainPerSecond;
                }

                if (citizen.Sanity > 0 && !isGainingSanity)
                {
                    citizen.Sanity -= naturalSanityDrainPerSecond;
                    if (citizen.CitizenNeeds.dutyRatio < 0.25f)
                    {
                        citizen.Sanity -= dutySanityDrainPerSecond;
                    }
                }

                sanity = citizen.Sanity;
                health = citizen.Happiness;
                duty = citizen.FactionDuty;
            }
        }

        private Building GetNextDestination()
        {
            BuildingType buildingType = BuildingType.House;

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
                return generationTestScript.buildingGrid[citizen.housePosition];
            }
            else
            {
                return generationTestScript.GetRandomBuildingByType(buildingType);
            }
        }

        public void SetHousePosition(Vector2 _housePos)
        {
            citizen.housePosition = _housePos;
        }
    }
}
