using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;


namespace Base.Gameplay.UI
{
    public class CommandmentButton : MyMonoBehaviour
    {
        public CommandmentType commandmentType;
        public Gameplay gameplayManager;

        public void ChooseCommandmentType()
        {
            gameplayManager.ChooseCommandment(commandmentType);
            Debug.Log($"A <color=red>{commandmentType}</color> was chosen!");
            gameplayManager.ChangeState(GameState.PlayerTurnPhase);
        }
    }
}