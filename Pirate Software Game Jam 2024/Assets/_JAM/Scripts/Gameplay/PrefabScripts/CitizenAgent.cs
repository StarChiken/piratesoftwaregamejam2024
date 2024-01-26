using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Core.Components;
using Base.Core.Managers;
using DG.Tweening;

namespace Base.Gameplay
{
    public class CitizenAgent : MyMonoBehaviour
    {
        public int dutySanityDrainPerSecond;

        public int naturalHealthDrainPerSecond;
        public int naturalSanityDrainPerSecond;
        public int naturalDutyDrainPerSecond;

        public GenerationTest generationTestScript;
        public PathfindingTest pathfindingTestScript;

        [SerializeField] int duty, sanity, health;

        private Citizen citizen;

        private float drainTimer = 0;
        private float checkTimer = 0;
        private int buildingChecks = 0;

        private bool isGainingHealth = false;
        private bool isGainingSanity = false;
        private bool isGainingDuty = false;
        private bool isMoving = false;

        private Building destination = null;

        private Vector2[] currentPath;

        void Start()
        {
            citizen = new Citizen();
            citizen.CitizenNeeds.CalculateNeeds(citizen.Sanity, citizen.Health, citizen.FactionDuty);
        }

        private void FixedUpdate()
        {
            if (destination == null)
            {
                destination = GetNextDestination();
                print(destination.buildingType);
            }

            citizen.CitizenNeeds.CalculateNeeds(citizen.Sanity, citizen.Health, citizen.FactionDuty);
            sanity = citizen.Sanity;
            health = citizen.Health;
            duty = citizen.FactionDuty;
            DrainStats();

            checkTimer += Time.fixedDeltaTime;
            if (checkTimer >= 1f)
            {
                checkTimer = 0;

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
                        citizen.Health += 1;
                        citizen.Sanity += 1;
                        break;
                }

                if (!isMoving)
                {
                    buildingChecks++;
                    if (buildingChecks > 3)
                    {
                        buildingChecks = 0;
                        destination = GetNextDestination();
                        currentPath = pathfindingTestScript.FindPath(transform.position, destination.gridPositions[0]);
                        StartCoroutine(MoveCitizen());
                        print(destination.name);
                    }
                }
            }
        }

        private IEnumerator MoveCitizen()
        {
            isMoving = true;
            float moveTime = 0.1f;
            for (int i = currentPath.Length - 1; i >= 0; i--)
            {
                transform.DOMove(currentPath[i], moveTime);
                yield return new WaitForSeconds(moveTime);
            }
            transform.position = destination.gridPositions[0];
            isMoving = false;
        }

        private void DrainStats()
        {
            drainTimer += Time.fixedDeltaTime;

            if (drainTimer >= 1f)
            {
                drainTimer = 0f;

                if (citizen.Health > 0 && !isGainingHealth)
                {
                    citizen.Health -= naturalHealthDrainPerSecond;
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
                health = citizen.Health;
                duty = citizen.FactionDuty;
            }
        }

        private Building GetNextDestination()
        {
            BuildingType buildingType = BuildingType.House;

            if (citizen.CitizenNeeds.healthRatio < 0.5f)
            {
                buildingType = BuildingType.Health;
                print(citizen.CitizenNeeds.healthRatio);
            }
            else if (citizen.CitizenNeeds.dutyRatio < 0.5f)
            {
                buildingType = BuildingType.Faction;
                print(citizen.CitizenNeeds.dutyRatio);
            }
            else if (citizen.CitizenNeeds.sanityRatio < 0.5f)
            {
                buildingType = BuildingType.Sanity;
                print(citizen.CitizenNeeds.sanityRatio);
            }
            return generationTestScript.GetRandomBuildingByType((Vector2)transform.position, buildingType);
        }
    }
}
