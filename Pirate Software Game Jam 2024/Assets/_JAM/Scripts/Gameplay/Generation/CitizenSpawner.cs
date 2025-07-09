using System.Collections.Generic;
using UnityEngine;
using Base.Core.Managers;

namespace Base.Gameplay
{
    /// <summary>
    /// Handles instantiation and assignment of citizens to buildings.
    /// </summary>
    public class CitizenSpawner
    {
        private readonly GameObject _citizenPrefab;
        private readonly Transform _parent;
        private readonly IGridManager _gridManager;
        private readonly IPathfindingService _pathfindingTest;

        /// <summary>
        /// Initializes a new CitizenSpawner.
        /// </summary>
        /// <param name="citizenPrefab">The prefab to use for citizen instantiation.</param>
        /// <param name="parent">The parent transform for spawned citizens (optional).</param>
        public CitizenSpawner(GameObject citizenPrefab, IGridManager gridManager, IPathfindingService pathfindingTest, Transform parent = null)
        {
            _citizenPrefab = citizenPrefab;
            _gridManager = gridManager;
            _pathfindingTest = pathfindingTest;
            _parent = parent;
        }

        /// <summary>
        /// Spawns a citizen at the given position and assigns them to a building.
        /// </summary>
        /// <param name="position">The world position to spawn the citizen.</param>
        /// <param name="building">The building to assign the citizen to.</param>
        public void SpawnCitizen(Vector3 position, Building building)
        {
            var citizenObj = Object.Instantiate(_citizenPrefab, position, Quaternion.identity, _parent);
            var citizenAgent = citizenObj.GetComponent<CitizenAgent>();
            citizenAgent.Init(_gridManager, _pathfindingTest);
            citizenAgent.citizen = new Citizen();
            citizenAgent.citizen.housePosition = new Vector2(position.x, position.z);
            // Optionally, add the citizen to the building's population list here
        }
    }
} 