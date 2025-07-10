using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using Random = UnityEngine.Random;


    public class Gameplay : MyMonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameOrchestrator m_orchestrator;
        // UI and input handling only
        // (Remove all Player, City, Devotion, GameEvents, and state logic)
        // Example: Button click handlers
        public void OnMiracleButtonClicked(MiracleType miracleType, List<Citizen> targetCitizens)
        {
            m_orchestrator.DoMiracleOnCitizens(miracleType, targetCitizens);
        }
        public void OnFactionActionButtonClicked(FactionAction action, Faction faction)
        {
            m_orchestrator.DoFactionAction(action, faction);
        }
        public void OnShowEventButtonClicked()
        {
            m_orchestrator.ShowEvent();
        }
        public void OnStartGameButtonClicked()
        {
            m_orchestrator.StartGame();
        }
        public void OnReportAboutGameButtonClicked()
        {
            m_orchestrator.ReportAboutGame();
        }
        // (Remove all other orchestration/state logic)
    }


