using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;


namespace Base.Gameplay
{
    public class MiracleButton : MyMonoBehaviour
    {
        public MiracleType MiracleType;
        public SectorScript sector;
        
        // data
        public List<Citizen> sectorPop = new ();
        public Gameplay gameplayManager;

        private void Awake()
        {
            gameplayManager = GameObject.Find("Gameplay").GetComponent<Gameplay>();
        }
        

        public void DoMiracle()
        {
            sectorPop = sector.sector.SectorPopulace;
            gameplayManager.DoMiracleOnCitizens(MiracleType, sectorPop);
        }
    }
}