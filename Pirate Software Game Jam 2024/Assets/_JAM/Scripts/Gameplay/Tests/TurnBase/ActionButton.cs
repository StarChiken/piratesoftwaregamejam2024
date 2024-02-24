using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;

namespace Base.Gameplay
{
    public class ActionButton : MyMonoBehaviour
    {
        public FactionAction factionActions;
        public SectorScript sector;
        public Gameplay gameplayManager;

        private void Awake()
        {
            gameplayManager = GameObject.Find("Gameplay").GetComponent<Gameplay>();
        }
        public void DoAction()
        {
            gameplayManager.DoFactionAction(factionActions, sector.sector.SectorFaction);
        }
    }
}