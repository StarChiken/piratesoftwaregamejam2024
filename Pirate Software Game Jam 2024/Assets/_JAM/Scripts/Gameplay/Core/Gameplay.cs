using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using Random = UnityEngine.Random;


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


