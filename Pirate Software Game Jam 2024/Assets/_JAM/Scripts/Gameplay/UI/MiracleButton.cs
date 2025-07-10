using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles UI button for triggering a miracle on a district's citizens.
/// </summary>
[RequireComponent(typeof(Button))]
public class MiracleButton : MyMonoBehaviour
{
    #region Fields
    [SerializeField, Tooltip("The type of miracle to perform when button is pressed")]
    private MiracleType m_miracleType;
    
    [SerializeField, Tooltip("The district this miracle button controls")]
    private DistrictScript m_district;
    
    [SerializeField, Tooltip("The button component for this miracle action")]
    private Button m_button;
    
    private readonly List<Citizen> m_districtPop = new();
    #endregion

    #region Events
    /// <summary>
    /// Event triggered when the miracle button is pressed.
    /// </summary>
    public event System.Action<MiracleType, List<Citizen>> OnMiracleTriggered;
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
            m_button.onClick.RemoveListener(DoMiracle);
        }
    }
    #endregion

    #region Public API
    /// <summary>
    /// Triggers the miracle on the district's citizens.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">Thrown when district or citizens are null.</exception>
    public void DoMiracle()
    {
        try
        {
            if (m_district == null)
            {
                throw new System.InvalidOperationException("District is not assigned to MiracleButton.");
            }

            if (m_district.district == null)
            {
                throw new System.InvalidOperationException("District's district property is null.");
            }

            if (m_district.district.DistrictPopulace == null)
            {
                throw new System.InvalidOperationException("District's populace is null.");
            }

            m_districtPop.Clear();
            m_districtPop.AddRange(m_district.district.DistrictPopulace);
            
            if (m_districtPop.Count == 0)
            {
                Debug.LogWarning("No citizens in district for miracle.");
                return;
            }

            OnMiracleTriggered?.Invoke(m_miracleType, m_districtPop);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error performing miracle: {ex.Message}");
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
            Debug.LogError("District is not assigned to MiracleButton. Please assign a district in the inspector.");
        }

        if (m_button == null)
        {
            if (!TryGetComponent<Button>(out m_button))
            {
                Debug.LogError("Button component is missing from MiracleButton. Adding RequireComponent should prevent this.");
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
            m_button.onClick.AddListener(DoMiracle);
        }
    }
    #endregion
}