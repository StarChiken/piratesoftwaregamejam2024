using System;
using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Resources = UnityEngine.Resources;

namespace Base.Gameplay
{
    /// <summary>
    /// Represents a miracle object that can perform miracles on citizens.
    /// </summary>
    public class MiracleObject : MyMonoBehaviour
    {
        [SerializeField] public MiracleType miracleType; // switch this in the inspector to define what miracle is it
        // this miracle
        public Miracle MiracleScript;
        
        // VFX
        [SerializeField] private GameObject vfxPrefab;
        private string vfxName;
        [SerializeField] private float tempOverlapRadius = 10;
        private Camera main => Camera.main;
        
        // Sound
        private AudioComponent audioSource;
        [SerializeField] private AudioClip idleSound;
        [SerializeField] private AudioClip castSound;
        [SerializeField] private List<AudioClip> audioClips; // 0 is idle, 1 is cast

        /// <summary>
        /// Initializes the MiracleObject. Should be called after the game starts.
        /// </summary>
        public void Init() // needs to be called after the game starts only (after the GameManger was called new)
        {
            MiracleScript = new Miracle();
            miracleType = MiracleScript._miracleType;
            SetupMiracleObject();
        }

        /// <summary>
        /// Sets up the miracle object VFX and sounds based on the miracle type.
        /// </summary>
        private void SetupMiracleObject()
        {
            // All types use the same logic, so we can simplify
            idleSound = audioClips[0];
            castSound = audioClips[1];
            vfxPrefab = Instantiate(Resources.Load(vfxName), transform.position, Quaternion.identity) as GameObject;
        }

        /// <summary>
        /// Performs the miracle on all citizens within the overlap radius.
        /// </summary>
        /// <param name="miracleType">The type of miracle to perform.</param>
        public void DoMiracleOnCitizens(MiracleType miracleType)
        {
            int devotionPoints = 10;//GameManager.Player.Devotion.DevotionPoints;
            
            if (devotionPoints > 0)
            {
                //GameManager.Player.Devotion.ChangeDevotionAmount(-1);
                
                Debug.Log($"A <color=red>{miracleType}</color> is being cast...");
                
                Vector3 sphereCenter = transform.position; // Testing Purposes Only, to be replaced by SerializedField variable or in the Cast Miracle prefab
                //LayerMask citizens_LayerMask = new LayerMask(); // Testing Purposes Only, to be replaced in each citizen prefab
                
                Collider[] targetedCitizens = Physics.OverlapSphere(sphereCenter, tempOverlapRadius);//,citizens_LayerMask);
                
                foreach (Collider citizenCollider in targetedCitizens)
                {
                    // Get the GameObject associated with the collider
                    GameObject citizenGameObject = citizenCollider.gameObject;

                    // Check if the GameObject has a CitizenAgent component
                    var citizenScript = citizenGameObject.GetComponent<CitizenAgent>();
                    if (citizenScript == null) continue;
                    
                    Citizen citizen = citizenScript.citizen;
                    // Invoke the miracle on the Citizen
                    GameManager.Player.Devotion.DoMiracle(miracleType, citizen);
                    
                    // Use shared trait-miracle matcher
                    if (TraitMiracleMatcher.IsTraitMatchingMiracle(citizen.FaithAttractionTrait, miracleType))
                    {
                        int attractionAmount = GameManager.Player.Devotion.MiracleFaithAttractionByType(miracleType);
                        citizen.ChangeAttractionAmount(attractionAmount);

                        Debug.Log($"<color=red>{citizen.CitizenName}</color> is happy about <color=red>{miracleType.ToString()}</color>, " +
                                  $"because he is a {citizen.FaithAttractionTrait}. His faith attraction is now {citizen.PlayerGodAttraction}");
                    }
                }
            }
            else
            {
                return;
            }
        }

        public Vector3 offset;

        private bool followCursor = false;

        private void Start()
        {
            transform.position = new Vector3(-300, -300, -300);
        }

        public void SetFollowCursor(bool _followCursor)
        {
            followCursor = _followCursor;
            if (!_followCursor)
            {
                transform.position = new Vector3(-300, -300, -300);
            }
        }

        private void Update()
        {
            if (followCursor)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    DoMiracleOnCitizens(miracleType);
                    SetFollowCursor(false);
                }
                else
                {
                    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 300))
                    {
                        transform.position = hit.point + offset;
                    }
                }
            }
        }
    }
}