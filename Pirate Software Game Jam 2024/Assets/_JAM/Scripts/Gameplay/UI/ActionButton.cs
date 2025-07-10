using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles UI button for triggering a faction action on a district.
/// </summary>
[RequireComponent(typeof(Button))]
public class ActionButton : MyMonoBehaviour
{
    #region Fields
    [SerializeField, Tooltip("The faction action to perform when button is pressed")]
    private FactionAction m_factionActions;
    
    [SerializeField, Tooltip("The district this button controls")]
    private DistrictScript m_district;
    
    [SerializeField, Tooltip("The button component for this action")]
    private Button m_button;
    #endregion

    #region Events
    /// <summary>
    /// Event triggered when the action button is pressed.
    /// </summary>
    public event System.Action<FactionAction, Faction> OnActionTriggered;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        ValidateRequiredComponents();
        SetupButton();
    }

    private void OnDestroy()
    {
        if (m_button != null)
        {
            m_button.onClick.RemoveListener(DoAction);
        }
    }
    #endregion

    #region Public API
    /// <summary>
    /// Triggers the faction action on the district's faction.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">Thrown when district or faction is null.</exception>
    public void DoAction()
    {
        try
        {
            if (m_district == null)
            {
                throw new System.InvalidOperationException("District is not assigned to ActionButton.");
            }

            if (m_district.district == null)
            {
                throw new System.InvalidOperationException("District's district property is null.");
            }

            if (m_district.district.DistrictFaction == null)
            {
                throw new System.InvalidOperationException("District's faction is null.");
            }

            OnActionTriggered?.Invoke(m_factionActions, m_district.district.DistrictFaction);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error performing faction action: {ex.Message}");
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Validates that all required components are assigned.
    /// </summary>
    private void ValidateRequiredComponents()
    {
        if (m_district == null)
        {
            Debug.LogError("District is not assigned to ActionButton. Please assign a district in the inspector.");
        }

        if (m_button == null)
        {
            m_button = GetComponent<Button>();
            if (m_button == null)
            {
                Debug.LogError("Button component is missing from ActionButton. Adding RequireComponent should prevent this.");
            }
        }
    }

    /// <summary>
    /// Sets up the button click listener.
    /// </summary>
    private void SetupButton()
    {
        if (m_button != null)
        {
            m_button.onClick.AddListener(DoAction);
        }
    }
    #endregion
}