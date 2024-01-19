using System;
using UnityEngine;

namespace Base.Core.Managers
{
    public class GameManager
    {
        public static GameManager Instance;

        public Player Player;
        public City City;
        public RandomEvents GameEvents;

        private Action onCompleteAction;

        public GameManager(Action onComplete)
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

            onCompleteAction = onComplete;
            InitManagers();
        }

        private void InitManagers()
        {
            new Player(result =>
            {
                Player = (Player)result;

                new City(result =>
                {
                    City = (City)result;

                    new RandomEvents(result =>
                    {
                        GameEvents = (RandomEvents)result;
                        onCompleteAction.Invoke();
                    });
                });
            });
        }
    }
}