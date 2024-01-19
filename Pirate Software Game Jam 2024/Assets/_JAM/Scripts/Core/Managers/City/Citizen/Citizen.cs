using System;
using UnityEngine.Serialization;

namespace Base.Core.Managers
{
    [Serializable]
    public class Citizen
    {
        // Refrances Stats
        private int _health;
        private int _sanity;
        private int _faith;
        private int _alignment;
        
        // Real Stats
        public string CitizenName;
        public TraitType FaithAttractionTrait; // What kind of faith actions they like
        public int PlayerFaithAttraction; // Current amount of attraction to the players faith

        public Citizen()
        {
            PlayerFaithAttraction = 0;
            CitizenName = GenerateName();
            FaithAttractionTrait = GenerateRandomTrait();
        }

        public void ChangeFaithAmount(int amount)
        {
            PlayerFaithAttraction += amount;
        }
        
        private string GenerateName()
        {
            string[] names = { "John", "Jane", "Alex", "Emily", "Michael", "Olivia", "David", "Sophia" };
            int index = UnityEngine.Random.Range(0, names.Length);
            return names[index];
        }
        
        private TraitType GenerateRandomTrait()
        {
            Array values = Enum.GetValues(typeof(TraitType));
            int index = UnityEngine.Random.Range(0, values.Length);
            return (TraitType)values.GetValue(index);
        }
    }
}