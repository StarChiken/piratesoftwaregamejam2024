using System;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Base class for all managers, providing async initialization and GameManager access.
/// </summary>
public class BaseManager
{
    #region Fields
    private readonly Action<BaseManager> m_onCompleteAction;
    [SerializeField, Tooltip("Reference to the GameManager for this manager")]
    protected GameManager m_gameManager;
    #endregion

    #region Properties
    /// <summary>
    /// Provides access to the global GameManager instance.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">Thrown when GameManager is not initialized.</exception>
    protected GameManager GameManager
    {
        get
        {
            try
            {
                if (m_gameManager == null)
                {
                    throw new System.InvalidOperationException("GameManager instance is null. Ensure it's properly initialized.");
                }
                return m_gameManager;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error accessing GameManager: {ex.Message}");
                return null;
            }
        }
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Constructs a new BaseManager and stores the completion callback.
    /// </summary>
    /// <param name="onComplete">Callback to invoke when initialization is complete.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when onComplete is null.</exception>
    protected BaseManager(Action<BaseManager> onComplete)
    {
        if (onComplete == null)
        {
            throw new System.ArgumentNullException(nameof(onComplete), "Completion callback cannot be null.");
        }

        m_onCompleteAction = onComplete;
    }
    #endregion

    #region Protected Methods
    /// <summary>
    /// Marks initialization as complete and invokes the callback asynchronously.
    /// </summary>
    protected async void OnInitComplete()
    {
        try
        {
            await Task.Delay(500);
            m_onCompleteAction?.Invoke(this);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error in OnInitComplete: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates that the manager is properly initialized.
    /// </summary>
    /// <returns>True if properly initialized, false otherwise.</returns>
    protected bool ValidateInitialization()
    {
        try
        {
            return GameManager != null;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error validating manager initialization: {ex.Message}");
            return false;
        }
    }
    #endregion
}