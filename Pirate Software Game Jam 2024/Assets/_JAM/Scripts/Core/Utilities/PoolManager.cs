using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages object pooling for frequently instantiated objects to improve performance.
/// </summary>
public class PoolManager : MonoBehaviour
{
    #region Fields
    [SerializeField, Tooltip("Default pool size for new pools")]
    private int m_defaultPoolSize = 10;
    
    [SerializeField, Tooltip("Maximum pool size to prevent memory issues")]
    private int m_maxPoolSize = 100;
    
    [SerializeField, Tooltip("Whether to expand pools automatically when empty")]
    private bool m_autoExpand = true;
    
    [SerializeField, Tooltip("Expand size when auto-expanding pools")]
    private int m_expandSize = 5;

    private readonly Dictionary<string, ObjectPool> m_pools = new();
    private readonly Dictionary<GameObject, string> m_objectToPoolMap = new();
    #endregion

    #region Properties
    /// <summary>
    /// Gets the singleton instance of PoolManager.
    /// </summary>
    public static PoolManager Instance { get; private set; }
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    #endregion

    #region Public API
    /// <summary>
    /// Creates a new pool for the specified prefab.
    /// </summary>
    /// <param name="prefab">The prefab to create a pool for.</param>
    /// <param name="initialSize">Initial size of the pool.</param>
    /// <returns>The name of the created pool.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when prefab is null.</exception>
    public string CreatePool(GameObject prefab, int initialSize = -1)
    {
        if (prefab == null)
        {
            throw new ArgumentNullException(nameof(prefab), "Prefab cannot be null.");
        }

        try
        {
            string poolName = prefab.name;
            
            if (m_pools.ContainsKey(poolName))
            {
                Debug.LogWarning($"Pool '{poolName}' already exists.");
                return poolName;
            }

            int size = initialSize > 0 ? initialSize : m_defaultPoolSize;
            var pool = new ObjectPool(prefab, size, m_maxPoolSize, m_autoExpand, m_expandSize);
            m_pools[poolName] = pool;
            
            Debug.Log($"Created pool '{poolName}' with size {size}");
            return poolName;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error creating pool for {prefab.name}: {ex.Message}");
            return string.Empty;
        }
    }

    /// <summary>
    /// Gets an object from the specified pool.
    /// </summary>
    /// <param name="poolName">The name of the pool.</param>
    /// <param name="position">The position to spawn the object at.</param>
    /// <param name="rotation">The rotation to spawn the object with.</param>
    /// <returns>The spawned GameObject, or null if pool doesn't exist.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when poolName is null or empty.</exception>
    public GameObject GetFromPool(string poolName, Vector3 position = default, Quaternion rotation = default)
    {
        if (string.IsNullOrEmpty(poolName))
        {
            throw new ArgumentNullException(nameof(poolName), "Pool name cannot be null or empty.");
        }

        try
        {
            if (!m_pools.TryGetValue(poolName, out var pool))
            {
                Debug.LogError($"Pool '{poolName}' does not exist.");
                return null;
            }

            var obj = pool.GetObject(position, rotation);
            if (obj != null)
            {
                m_objectToPoolMap[obj] = poolName;
            }
            
            return obj;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error getting object from pool '{poolName}': {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Returns an object to its pool.
    /// </summary>
    /// <param name="obj">The object to return.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when obj is null.</exception>
    public void ReturnToPool(GameObject obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj), "Object cannot be null.");
        }

        try
        {
            if (!m_objectToPoolMap.TryGetValue(obj, out var poolName))
            {
                Debug.LogWarning($"Object {obj.name} is not from a pool. Destroying instead.");
                Destroy(obj);
                return;
            }

            if (m_pools.TryGetValue(poolName, out var pool))
            {
                pool.ReturnObject(obj);
                m_objectToPoolMap.Remove(obj);
            }
            else
            {
                Debug.LogError($"Pool '{poolName}' not found for object {obj.name}. Destroying instead.");
                Destroy(obj);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error returning object {obj.name} to pool: {ex.Message}");
            Destroy(obj);
        }
    }

    /// <summary>
    /// Destroys the specified pool and all its objects.
    /// </summary>
    /// <param name="poolName">The name of the pool to destroy.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when poolName is null or empty.</exception>
    public void DestroyPool(string poolName)
    {
        if (string.IsNullOrEmpty(poolName))
        {
            throw new ArgumentNullException(nameof(poolName), "Pool name cannot be null or empty.");
        }

        try
        {
            if (m_pools.TryGetValue(poolName, out var pool))
            {
                pool.DestroyPool();
                m_pools.Remove(poolName);
                
                // Remove all objects from this pool from the mapping
                var objectsToRemove = new List<GameObject>();
                foreach (var kvp in m_objectToPoolMap)
                {
                    if (kvp.Value == poolName)
                    {
                        objectsToRemove.Add(kvp.Key);
                    }
                }
                
                foreach (var obj in objectsToRemove)
                {
                    m_objectToPoolMap.Remove(obj);
                }
                
                Debug.Log($"Destroyed pool '{poolName}'");
            }
            else
            {
                Debug.LogWarning($"Pool '{poolName}' does not exist.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error destroying pool '{poolName}': {ex.Message}");
        }
    }

    /// <summary>
    /// Gets information about all pools.
    /// </summary>
    /// <returns>A dictionary with pool names and their information.</returns>
    public Dictionary<string, PoolInfo> GetPoolInfo()
    {
        var info = new Dictionary<string, PoolInfo>();
        
        foreach (var kvp in m_pools)
        {
            info[kvp.Key] = kvp.Value.GetPoolInfo();
        }
        
        return info;
    }

    /// <summary>
    /// Clears all pools and destroys all pooled objects.
    /// </summary>
    public void ClearAllPools()
    {
        try
        {
            foreach (var pool in m_pools.Values)
            {
                pool.DestroyPool();
            }
            
            m_pools.Clear();
            m_objectToPoolMap.Clear();
            
            Debug.Log("All pools cleared.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error clearing all pools: {ex.Message}");
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Validates that a pool name is valid.
    /// </summary>
    /// <param name="poolName">The pool name to validate.</param>
    /// <returns>True if valid, false otherwise.</returns>
    private bool ValidatePoolName(string poolName)
    {
        return !string.IsNullOrEmpty(poolName) && !string.IsNullOrWhiteSpace(poolName);
    }
    #endregion
}

/// <summary>
/// Information about a pool.
/// </summary>
public struct PoolInfo
{
    public string PoolName;
    public int ActiveCount;
    public int InactiveCount;
    public int TotalCount;
    public int MaxSize;
}

/// <summary>
/// Represents a pool of GameObjects.
/// </summary>
public class ObjectPool
{
    #region Fields
    private readonly GameObject m_prefab;
    private readonly Queue<GameObject> m_inactiveObjects = new();
    private readonly List<GameObject> m_activeObjects = new();
    private readonly int m_maxSize;
    private readonly bool m_autoExpand;
    private readonly int m_expandSize;
    #endregion

    #region Constructor
    /// <summary>
    /// Creates a new object pool.
    /// </summary>
    /// <param name="prefab">The prefab to pool.</param>
    /// <param name="initialSize">Initial size of the pool.</param>
    /// <param name="maxSize">Maximum size of the pool.</param>
    /// <param name="autoExpand">Whether to auto-expand when empty.</param>
    /// <param name="expandSize">Size to expand by when auto-expanding.</param>
    public ObjectPool(GameObject prefab, int initialSize, int maxSize, bool autoExpand, int expandSize)
    {
        m_prefab = prefab;
        m_maxSize = maxSize;
        m_autoExpand = autoExpand;
        m_expandSize = expandSize;

        // Pre-populate the pool
        for (int i = 0; i < initialSize; i++)
        {
            var obj = CreateNewObject();
            m_inactiveObjects.Enqueue(obj);
        }
    }
    #endregion

    #region Public API
    /// <summary>
    /// Gets an object from the pool.
    /// </summary>
    /// <param name="position">The position to spawn at.</param>
    /// <param name="rotation">The rotation to spawn with.</param>
    /// <returns>The spawned GameObject.</returns>
    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        GameObject obj;

        if (m_inactiveObjects.Count > 0)
        {
            obj = m_inactiveObjects.Dequeue();
        }
        else if (m_autoExpand && m_activeObjects.Count < m_maxSize)
        {
            // Auto-expand the pool
            for (int i = 0; i < m_expandSize && m_activeObjects.Count + m_inactiveObjects.Count < m_maxSize; i++)
            {
                var newObj = CreateNewObject();
                m_inactiveObjects.Enqueue(newObj);
            }
            
            if (m_inactiveObjects.Count > 0)
            {
                obj = m_inactiveObjects.Dequeue();
            }
            else
            {
                Debug.LogWarning($"Pool for {m_prefab.name} is full and cannot expand further.");
                return null;
            }
        }
        else
        {
            Debug.LogWarning($"Pool for {m_prefab.name} is empty and auto-expand is disabled.");
            return null;
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        
        m_activeObjects.Add(obj);
        
        return obj;
    }

    /// <summary>
    /// Returns an object to the pool.
    /// </summary>
    /// <param name="obj">The object to return.</param>
    public void ReturnObject(GameObject obj)
    {
        if (obj == null) return;

        if (m_activeObjects.Remove(obj))
        {
            obj.SetActive(false);
            obj.transform.SetParent(null);
            
            if (m_inactiveObjects.Count < m_maxSize)
            {
                m_inactiveObjects.Enqueue(obj);
            }
            else
            {
                // Pool is full, destroy the object
                UnityEngine.Object.Destroy(obj);
            }
        }
    }

    /// <summary>
    /// Destroys the pool and all its objects.
    /// </summary>
    public void DestroyPool()
    {
        foreach (var obj in m_activeObjects)
        {
            if (obj != null)
            {
                UnityEngine.Object.Destroy(obj);
            }
        }
        
        while (m_inactiveObjects.Count > 0)
        {
            var obj = m_inactiveObjects.Dequeue();
            if (obj != null)
            {
                UnityEngine.Object.Destroy(obj);
            }
        }
        
        m_activeObjects.Clear();
    }

    /// <summary>
    /// Gets information about this pool.
    /// </summary>
    /// <returns>Pool information.</returns>
    public PoolInfo GetPoolInfo()
    {
        return new PoolInfo
        {
            PoolName = m_prefab.name,
            ActiveCount = m_activeObjects.Count,
            InactiveCount = m_inactiveObjects.Count,
            TotalCount = m_activeObjects.Count + m_inactiveObjects.Count,
            MaxSize = m_maxSize
        };
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Creates a new object for the pool.
    /// </summary>
    /// <returns>The created GameObject.</returns>
    private GameObject CreateNewObject()
    {
        var obj = UnityEngine.Object.Instantiate(m_prefab);
        obj.SetActive(false);
        return obj;
    }
    #endregion
} 