using System;
using System.Collections.Generic;
using System.Linq;
using Base.Core.Managers;
using UnityEngine.Serialization;

namespace Base.Gameplay
{
    public class MiracleButton : ButtonBase
    {
        public MiracleType MiracleType;
        public SectorScript sector;
        
        public void DoMiracle()
        {
            sectorPop = sector.sector.SectorPopulace;
            gameplayManager.DoMiracleOnCitizens(MiracleType, sectorPop);
        }
    }
    
    public class ActionButton : ButtonBase
    {
        public SectorType SectorType;
        [FormerlySerializedAs("ActionType")] public FactionAction factionAction;
        public SectorScript sector;
        [FormerlySerializedAs("actions")] public FactionAction factionActions;
        
        public void DoAction()
        {
            // sectorPop = sector.sector.SectorPopulace;
            // gameplayManager.DoFactionAction(ActionType, );
        }
    }
}