using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Base.Core.Components;
using Base.Core.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Base.Gameplay
{
    public class SectorScript : MyMonoBehaviour
    {
        public TextMeshProUGUI name;
        public Sector sector;
        
        // details panel
        public GameObject panel;
        [SerializeField] public TextMeshProUGUI text;
        private StringBuilder _logStringBuilder = new();
        public bool isOpen;
        
        public void DoDistrictWindow(bool isOpen)
        {
            Debug.Log("Inside DoDistrictWindow" + isOpen);
            if (isOpen)
            {
                gameObject.SetActive(isOpen);
                gameObject.transform.DOScale(1, 0.5f);
            }
            else
            {
                gameObject.transform.DOScale(0, 0.5f).OnComplete(() =>
                {
                    gameObject.SetActive(isOpen);
                });
            }
        }

        private void Start()
        {
            //gameObject.SetActive(false);
        }

        // private void OnDisable()
        // {
        //     text.text = String.Empty; 
        // }

        public void OpenStatsPanel()
        {
            ReportSector();
            panel.SetActive(true);
        }

        private void ReportSector()
        {
            // PrintInfo($"<sprite index=4> Sector Type; {sector.sectorType}");
            //
            // PrintInfo($"<sprite index=4> Faction Name; {sector.SectorFaction.FactionName}");
            // PrintInfo($" Faction Alignment; {sector.SectorFaction.FactionAlignment}   <sprite index=3>");
            // PrintInfo($" Resource Give Amount; {sector.SectorFaction.FactionGiveAmount}   <sprite index=3>");
            // PrintInfo($" In Favor; {sector.SectorFaction.InFavor}   <sprite index=3>");
            
            PrintInfo(PrintTritPercent().ToString());
            
            int attractedNum = 0;
            int followerNum = 0;
            foreach (var citizen in sector.SectorPopulace)
            {
                if (citizen.PlayerGodAttraction == 2) // on 3 a citizen joins as a follower
                {
                    attractedNum++;
                }
            }

            foreach (var citizen in sector.SectorPopulace)
            {
                if (citizen.PlayerGodAttraction == 3)
                {
                    followerNum++;
                }
            }
            

            var total = GameManager.City.StartingCitizenAmountPerDistrict;
            
            float attractedPercentage = ((float)attractedNum / total) * 100;
            float followerPercentage = ((float)followerNum / total) * 100;

            
            PrintInfo($" {attractedPercentage} of {sector.SectorName} populace are about to become a follower <sprite index=3>");
            PrintInfo($" {followerPercentage} of {sector.SectorName} populace are follower <sprite index=3>");
        }

        private StringBuilder PrintTritPercent()
        {
            Dictionary<TraitType, int> traitCounts = new Dictionary<TraitType, int>
            {
                { TraitType.Academic, 0 },
                { TraitType.Apologist, 0 },
                { TraitType.Spiritual, 0 },
                { TraitType.Collector, 0 },
                { TraitType.Witch, 0 },
                { TraitType.Poet, 0 },
                { TraitType.Scheduled, 0 },
                { TraitType.Performer, 0 },
                { TraitType.Naturalist, 0 },
                { TraitType.Soldier, 0 },
                { TraitType.Aesthetic, 0 },
                { TraitType.Masochist, 0 },
                { TraitType.Fanatic, 0 },
                { TraitType.Noble, 0 },
                { TraitType.Farmer, 0 },
            };

            // Count the number of citizens for each trait type
            foreach (var citizen in sector.SectorPopulace)
            {
                traitCounts[citizen.FaithAttractionTrait]++;
            }

            var traitThatAreMoreThen0 =
                traitCounts.Where(kvp => kvp.Value > 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            StringBuilder NewStringer = new StringBuilder();

            foreach (var kvp in traitThatAreMoreThen0)
            {
                float percentage = (kvp.Value / (float)sector.SectorPopulace.Count) * 100;
                NewStringer.AppendLine($"{percentage:F2}% are {kvp.Key} <sprite index=3>");
            }

            return NewStringer;
        }

        private void PrintInfo(string logString)
        {
            _logStringBuilder.AppendLine(logString);
            text.text = _logStringBuilder.ToString();
        }
    }
}