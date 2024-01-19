using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    [Serializable]
    public class Devotion
    {
        public int DevotionPoints;
        public Dictionary<MiracleType, int> DevotionActionsList;
        public Dictionary<CommandmentType, int> CommandmentsList;
        
        public Devotion(int startingPoints)
        {
            DevotionPoints = startingPoints;
            InitializeMiraclesAndCursesList();
            InitializeCommandmentsList();
        }
        
        public void AddCommandment(CommandmentType commandment)
        {
            switch (commandment)
            {
                case CommandmentType.RedCommandment:
                    CommandmentsList[CommandmentType.RedCommandment]++;
                    DevotionActionsList[MiracleType.RedMiracle]++;
                    break;
                case CommandmentType.BlueCommandment:
                    CommandmentsList[CommandmentType.BlueCommandment]++;
                    DevotionActionsList[MiracleType.BlueMiracle]++;
                    break;
                case CommandmentType.GreenCommandment:
                    CommandmentsList[CommandmentType.GreenCommandment]++;
                    DevotionActionsList[MiracleType.GreenMiracle]++;
                    break;
            }
        }
        
        public void ChangeDevotionAmount(int amount)
        {
            DevotionPoints += amount;
        }
        
        public int Miracle(MiracleType type)
        {
            DevotionActionsList.TryGetValue(type, out int modifier);
            
            return modifier;
        }
        
        private void InitializeCommandmentsList()
        {
            CommandmentsList = new Dictionary<CommandmentType, int>
            {
                { CommandmentType.RedCommandment, 0 },
                { CommandmentType.BlueCommandment, 0 },
                { CommandmentType.GreenCommandment, 0 }
            };
        }
        private void InitializeMiraclesAndCursesList()
        {
            DevotionActionsList = new Dictionary<MiracleType, int>
            {
                { MiracleType.RedMiracle, 0 },
                { MiracleType.BlueMiracle, 0 },
                { MiracleType.GreenMiracle, 0 }
            };
        }
    }
}