using Base.Core.Managers;
using UnityEngine;

namespace Base.Core.Components
{
    /// <summary>
    /// Base class for all MonoBehaviours in the project, providing convenient access to the GameManager singleton.
    /// </summary>
    public class MyMonoBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Provides protected access to the global GameManager instance for subclasses.
        /// </summary>
        protected GameManager GameManager => GameManager.Instance; // Use in derived classes for global access
    }
}