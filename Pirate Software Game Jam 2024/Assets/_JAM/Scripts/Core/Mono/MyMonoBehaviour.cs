using UnityEngine;

/// <summary>
/// Base class for all MonoBehaviours in the project, providing convenient access to the GameManager singleton.
/// </summary>
public class MyMonoBehaviour : MonoBehaviour
{
    #region Properties
    /// <summary>
    /// Provides protected access to the global GameManager instance for subclasses.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">Thrown when GameManager is not initialized.</exception>
    protected GameManager GameManager
    {
        get
        {
            try
            {
                var gameManager = GameManager.Instance;
                if (gameManager == null)
                {
                    throw new System.InvalidOperationException("GameManager instance is null. Ensure it's properly initialized.");
                }
                return gameManager;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error accessing GameManager: {ex.Message}");
                return null;
            }
        }
    }
    #endregion

    #region Unity Lifecycle
    protected virtual void Awake()
    {
        ValidateGameManager();
    }

    protected virtual void Start()
    {
        // Override in derived classes if needed
    }

    protected virtual void OnDestroy()
    {
        // Override in derived classes if needed
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Validates that the GameManager is properly initialized.
    /// </summary>
    /// <returns>True if GameManager is valid, false otherwise.</returns>
    protected bool ValidateGameManager()
    {
        try
        {
            var gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                Debug.LogWarning("GameManager is not initialized. Some functionality may not work correctly.");
                return false;
            }
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error validating GameManager: {ex.Message}");
            return false;
        }
    }
    #endregion
}