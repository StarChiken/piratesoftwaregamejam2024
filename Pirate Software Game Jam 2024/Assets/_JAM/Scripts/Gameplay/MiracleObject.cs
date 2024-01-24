using System;
using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;

namespace Base.Gameplay
{
    public class MiracleObject : MyMonoBehaviour
    {
        public Miracle MiracleScript;
        [SerializeField] public MiracleType miracleType; // double type? already have type in Miracle?
        
        public void Init(Miracle miracle) // needs to be called after the GameManger was called
        {
            MiracleScript = miracle;
            miracleType = MiracleScript._miracleType;
        }
        
        public void DoMiracleOnCitizens(MiracleType miracleType)
        {
            int devotionPoints = GameManager.Player.Devotion.DevotionPoints;
            
            if (devotionPoints > 0)
            {
                GameManager.Player.Devotion.ChangeDevotionAmount(-1);
                
                Debug.Log($"A <color=red>{miracleType}</color> is being cast...");

                float tempOverlapRadius = 10; // Testing Purposes Only, to be replaced bu SerializedField variable
                Vector3 sphereCenter = transform.position; // Testing Purposes Only, to be replaced bu SerializedField variable or in the Cast Miracle prefab
                LayerMask citizens_LayerMask = new LayerMask(); // Testing Purposes Only, to be replaced in each citizen prefab
                
                Collider[] targetedCitizens = Physics.OverlapSphere(sphereCenter, tempOverlapRadius,citizens_LayerMask);
                
                foreach (Collider citizenCollider in targetedCitizens)
                {
                    // Check if the collider has a Citizen component
                    Citizen citizen = citizenCollider.GetComponent<Citizen>();
                    if (citizen == null) continue;
                    
                    // Invoke the miracle on the Citizen
                    GameManager.Player.Devotion.DoMiracle(miracleType, citizen);
                    
                    // Match Miracle Type to Trait Type and Change Attraction
                    if (IsTraitMatchingMiracle(citizen.FaithAttractionTrait, miracleType))
                    {
                        int attractionAmount = GameManager.Player.Devotion.MiracleFaithAttractionByType(miracleType);
                        citizen.ChangeAttractionAmount(attractionAmount);

                        Debug.Log($"<color=red>{citizen.CitizenName}</color> is happy about <color=red>{miracleType.ToString()}</color>, " +
                                  $"because he is a {citizen.FaithAttractionTrait}. His faith attraction is now {citizen.PlayerGodAttraction}");
                    }
                }
                
                // LayerMask buildings_LayerMask = new LayerMask(); // Testing Purposes Only, to be replaced in each building prefab
                // Collider[] targetedBuildings = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, buildings_LayerMask);
                //
                // foreach (Collider buildingCollider in targetedBuildings)
                // {
                // // Check if the collider has a Citizen component
                //     Building building = buildingCollider.GetComponent<Building>();
                // }
                
            }
            else
            {
                return;
            }
        }
        private bool IsTraitMatchingMiracle(TraitType citizenTrait, MiracleType miracleType)
        {
            switch (citizenTrait)
            {
                case TraitType.Academic:
                case TraitType.Apologist:
                case TraitType.Spiritual:
                    return miracleType is MiracleType.RedBasic or MiracleType.RedIntermediate or MiracleType.RedSuperior;
                case TraitType.Collector:
                case TraitType.Witch:
                    return miracleType is MiracleType.RedBasic or MiracleType.RedIntermediate or MiracleType.RedSuperior 
                        or MiracleType.GreenBasic or MiracleType.GreenIntermediate or MiracleType.GreenSuperior;
                case TraitType.Poet:
                case TraitType.Scheduled:
                    return miracleType is MiracleType.RedBasic or MiracleType.RedIntermediate or MiracleType.RedSuperior or MiracleType.BlueBasic;
                case TraitType.Performer:
                case TraitType.Naturalist:
                case TraitType.Soldier:
                    return miracleType is MiracleType.BlueBasic or MiracleType.BlueIntermediate or MiracleType.BlueSuperior;
                case TraitType.Aesthetic:
                case TraitType.Masochist:
                    return miracleType is MiracleType.GreenBasic or MiracleType.GreenIntermediate or MiracleType.GreenSuperior
                        or MiracleType.BlueBasic or MiracleType.BlueIntermediate or MiracleType.BlueSuperior;
                case TraitType.Fanatic:
                case TraitType.Noble:
                case TraitType.Farmer:
                    return miracleType is MiracleType.GreenBasic or MiracleType.GreenIntermediate or MiracleType.GreenSuperior;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}