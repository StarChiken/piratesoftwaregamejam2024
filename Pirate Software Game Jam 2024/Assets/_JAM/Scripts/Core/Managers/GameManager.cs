using System;
using UnityEngine;

namespace Base.Core.Managers
{
    /// <summary>
    /// Coordinates core game managers and provides global access.
    /// </summary>
    public class GameManager : IGameManager
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError("GameManager instance is null!");
                return _instance;
            }
            private set { _instance = value; }
        }

        public Player Player { get; private set; }
        public City City { get; private set; }
        public RandomEvents GameEvents { get; private set; }

        private readonly Action _onCompleteAction;

        public GameManager(Action onComplete, Player player = null, City city = null, RandomEvents gameEvents = null)
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError($"Two {typeof(GameManager)} instances exist, didn't create new one");
                return;
            }

            _onCompleteAction = onComplete;
            InitManagers(player, city, gameEvents);
        }

        private void InitManagers(Player player, City city, RandomEvents gameEvents)
        {
            if (player != null && city != null && gameEvents != null)
            {
                Player = player;
                City = city;
                GameEvents = gameEvents;
                _onCompleteAction?.Invoke();
                return;
            }

            // Default initialization chain
            new Player(new PlayerConfig(), result =>
            {
                Player = (Player)result;
                new City(new CityConfig(), result =>
                {
                    City = (City)result;
                    new RandomEvents(new RandomEventsConfig(), result =>
                    {
                        GameEvents = (RandomEvents)result;
                        _onCompleteAction?.Invoke();
                    });
                });
            });
        }
    }
}