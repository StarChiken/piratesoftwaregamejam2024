using System;
using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using TMPro;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Base.Gameplay
{
    public class Gameplay : MyMonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameOrchestrator orchestrator;
        // UI and input handling only
        // (Remove all Player, City, Devotion, GameEvents, and state logic)
        // Example: Button click handlers
        public void OnMiracleButtonClicked(MiracleType miracleType, List<Citizen> targetCitizens)
        {
            orchestrator.DoMiracleOnCitizens(miracleType, targetCitizens);
        }
        public void OnFactionActionButtonClicked(FactionAction action, Faction faction)
        {
            orchestrator.DoFactionAction(action, faction);
        }
        public void OnShowEventButtonClicked()
        {
            orchestrator.ShowEvent();
        }
        public void OnStartGameButtonClicked()
        {
            orchestrator.StartGame();
        }
        public void OnReportAboutGameButtonClicked()
        {
            orchestrator.ReportAboutGame();
        }
        // (Remove all other orchestration/state logic)
    }

}
    public enum GameState
    {
        StartGamePhase = 0,
        PlayerTurnPhase = 1,
        CalculateCityPhase = 2,
        GetCommandmentPhase = 3,
    }