using System;
using Base.Core.Components;
using UnityEngine;

namespace Base.Gameplay
{
    public class Pause : MyMonoBehaviour
    {
        [Range(0.1f, 2)] 
        public float modifiedScale;

        private void OnEnable()
        {
            Time.timeScale = 0.1f;
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }

            void Update()
        {
            Time.timeScale = modifiedScale;
        }
    }
}
