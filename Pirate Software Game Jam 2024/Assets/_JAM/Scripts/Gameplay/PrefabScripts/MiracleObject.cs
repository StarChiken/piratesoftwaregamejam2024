using System;
using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Resources = UnityEngine.Resources;

namespace Base.Gameplay
{
    public class MiracleObject : MyMonoBehaviour
    {
        [SerializeField] public MiracleType miracleType; // switch this in the inspector to define what miracle is it
        // this miracle
        public Miracle MiracleScript;
        
        // VFX
        [SerializeField] private GameObject vfxPrefab;
        private string vfxName;

        private Camera main => Camera.main;
        
        // Sound
        private AudioComponent audioSource;
        [SerializeField] private AudioClip idleSound;
        [SerializeField] private AudioClip castSound;
        [SerializeField] private List<AudioClip> audioClips; // 0 is idle, 1 is cast

        public void Init() // needs to be called after the game starts only (after the GameManger was called new)
        {
            MiracleScript = new Miracle();
            miracleType = MiracleScript._miracleType;
            SetupMiracleObject();
        }

        private void SetupMiracleObject()
        {
            switch (miracleType)
            {
                case MiracleType.RedBasic:
                case MiracleType.RedIntermediate:
                case MiracleType.RedSuperior:
                    idleSound = audioClips[0];
                    castSound = audioClips[1];
                    vfxPrefab = Instantiate(Resources.Load(vfxName), transform.position, Quaternion.identity) as GameObject;
                    break;
                case MiracleType.BlueBasic:
                case MiracleType.BlueIntermediate:
                case MiracleType.BlueSuperior:
                    idleSound = audioClips[0];
                    castSound = audioClips[1];
                    vfxPrefab = Instantiate(Resources.Load(vfxName), transform.position, Quaternion.identity) as GameObject;
                    break;
                case MiracleType.GreenBasic:
                case MiracleType.GreenIntermediate:
                case MiracleType.GreenSuperior:
                    idleSound = audioClips[0];
                    castSound = audioClips[1];
                    vfxPrefab = Instantiate(Resources.Load(vfxName), transform.position, Quaternion.identity) as GameObject;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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