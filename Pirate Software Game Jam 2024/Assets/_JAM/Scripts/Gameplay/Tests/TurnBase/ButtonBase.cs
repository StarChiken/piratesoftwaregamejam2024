using System;
using System.Collections.Generic;
using System.Linq;
using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace Base.Gameplay
{
    public class ButtonBase : MyMonoBehaviour//, IPointerEnterHandler
    {
        public GameObject[] buttonObject;
        public GameObject[] otherPanels;
        public GameObject panel;
        private bool panelState;
        private float startTime;
        
        private void Awake()
        {
            OpenClosePanel(false);
        }

        public void OpenClosePanel(bool isActive)
        {
            panel.SetActive(isActive);

            foreach (var obj in otherPanels)
            {
                if (obj.activeSelf)
                {
                    obj.SetActive(!isActive);
                }
            }

            panelState = isActive;
            // Reset open count and start timer when opening the panel
            if (isActive)
            {
                startTime = Time.time;
            }
        }

        
        // public void OnPointerEnter(PointerEventData eventData)
        // {
        //
        //     foreach (var obj in buttonObject)
        //     {
        //         if (obj.TryGetComponent(out MiracleButton miracleScript))
        //         {
        //             var button = miracleScript.button;
        //             var miracleType = miracleScript.MiracleType;
        //
        //             // Define the mappings between MiracleType and CommandmentType using a dictionary
        //             Dictionary<MiracleType, CommandmentType[]> miracleToCommandmentsMapping = new Dictionary<MiracleType, CommandmentType[]>
        //             {
        //                 { MiracleType.RedBasic, new[] { CommandmentType.Prayer, CommandmentType.ReadingScripture } },
        //                 { MiracleType.RedIntermediate, new[] { CommandmentType.CopyingText, CommandmentType.Research } },
        //                 { MiracleType.RedSuperior, new[] { CommandmentType.Confessions, CommandmentType.Exorcism, CommandmentType.Alchemy } },
        //                 { MiracleType.BlueBasic, new[] { CommandmentType.Feast, CommandmentType.Creation } },
        //                 { MiracleType.BlueIntermediate, new[] { CommandmentType.Dance, CommandmentType.Song } },
        //                 { MiracleType.BlueSuperior, new[] { CommandmentType.RitualisticAction, CommandmentType.RitualisticPunishment } },
        //                 { MiracleType.GreenBasic, new[] { CommandmentType.MaterialOfferings, CommandmentType.ReligiousCultivation } },
        //                 { MiracleType.GreenIntermediate, new[] { CommandmentType.Relics, CommandmentType.Shrines } },
        //                 { MiracleType.GreenSuperior, new[] { CommandmentType.GatheringBlood, CommandmentType.Donations, CommandmentType.RitualSacrifice } }
        //             };
        //
        //             // Check if any of the associated commandments are present in the Devotion's CommandmentsList
        //             button.interactable = miracleToCommandmentsMapping[miracleType].Any(commandment =>
        //                 GameManager.Player.Devotion.CommandmentsList[commandment]);
        //         
        //             // button.interactable = miracleType switch
        //             // {
        //             //     MiracleType.RedBasic => 
        //             //         !(GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.Prayer) || GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.ReadingScripture)),
        //             //     MiracleType.RedIntermediate => 
        //             //         !(GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.CopyingText) || GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.Research)),
        //             //     MiracleType.RedSuperior => 
        //             //         !(GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.Confessions) || GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.Exorcism) || GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.Alchemy)),
        //             //     MiracleType.BlueBasic => 
        //             //         !(GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.Feast) || GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.Creation)),
        //             //     MiracleType.BlueIntermediate => 
        //             //         !(GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.Dance) || GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.Song)),
        //             //     MiracleType.BlueSuperior => 
        //             //         !(GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.RitualisticAction) || GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.RitualisticPunishment)),
        //             //     MiracleType.GreenBasic => 
        //             //         !(GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.MaterialOfferings) || GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.ReligiousCultivation)),
        //             //     MiracleType.GreenIntermediate => 
        //             //         !(GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.Relics) || GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.Shrines)),
        //             //     MiracleType.GreenSuperior => 
        //             //         !(GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.GatheringBlood) || GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.Donations) || GameManager.Player.Devotion.CommandmentsList.ContainsKey(CommandmentType.RitualSacrifice)),
        //             //     _ => throw new ArgumentOutOfRangeException()
        //             // };
        //         }
        //     
        //         if (obj.TryGetComponent(out ActionButton actionButton))
        //         {
        //             var button = actionButton.button;
        //             var actionType = actionButton.factionActions;
        //             var faction = actionButton.sector.sector.SectorFaction;
        //             
        //             switch (actionType)
        //             {
        //                 case FactionAction.GetResource:
        //                     button.interactable = GameManager.Player.Currency != 0;
        //                     break;
        //             
        //                 case FactionAction.GetFavor:
        //                     button.interactable = faction.FactionAlignment >= 11;
        //                     break;
        //                 case FactionAction.GetInfluence:
        //                     button.interactable = faction.FactionAlignment >= 50;
        //
        //                     break;
        //                 default:
        //                     throw new ArgumentOutOfRangeException();
        //             }
        //         }
        //     
        //         if (obj.TryGetComponent(out ProphetButton prophetButton))
        //         {
        //             var button = prophetButton.button;
        //             var actionType = prophetButton.prophetActions;                 
        //         
        //             switch (actionType)
        //             {
        //                 case ProphetAction.Pray:
        //                     button.interactable = GameManager.Player.prophet.ProphetStat >= 5;
        //                     break;
        //                 case ProphetAction.Preach:
        //                     button.interactable = GameManager.Player.prophet.ProphetStat >= 15;
        //                     break;
        //                 case ProphetAction.Infiltrate:
        //                     button.interactable = GameManager.Player.prophet.ProphetStat >= 25;
        //
        //                     break;
        //                 default:
        //                     throw new ArgumentOutOfRangeException();
        //             }
        //         }
        //     }
        //
        // }



        private void Update()
        {
            // if (panelState)
            // {
            //     // Close the panel after 30 seconds
            //     if (Time.time - startTime > 10)
            //     {
            //         OpenClosePanel(false);
            //     }
            // }
        }
    }
}