using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility methods for random operations on collections.
/// </summary>
public static class RandomUtil
{
    #region Constants
    private const string c_listNullErrorMessage = "List cannot be null.";
    private const string c_listEmptyErrorMessage = "List cannot be empty.";
    #endregion

    #region Public API
    /// <summary>
    /// Selects a random element from a list and removes it.
    /// </summary>
    /// <typeparam name="T">Type of list element.</typeparam>
    /// <param name="list">The list to select from.</param>
    /// <param name="fallbackProvider">Optional fallback provider when list is empty.</param>
    /// <returns>The randomly selected element.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when list is null.</exception>
    /// <exception cref="System.ArgumentException">Thrown when list is empty and no fallback is provided.</exception>
    public static T TakeRandom<T>(List<T> list, Func<T> fallbackProvider = null) where T : class
    {
        try
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list), c_listNullErrorMessage);
            }

            if (list.Count == 0)
            {
                if (fallbackProvider != null)
                {
                    return fallbackProvider.Invoke();
                }
                throw new ArgumentException(c_listEmptyErrorMessage, nameof(list));
            }

            int index = UnityEngine.Random.Range(0, list.Count);
            T value = list[index];
            list.RemoveAt(index);
            return value;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error in TakeRandom: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Selects a random element from a list without removing it.
    /// </summary>
    /// <typeparam name="T">Type of list element.</typeparam>
    /// <param name="list">The list to select from.</param>
    /// <param name="fallbackProvider">Optional fallback provider when list is empty.</param>
    /// <returns>The randomly selected element.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when list is null.</exception>
    /// <exception cref="System.ArgumentException">Thrown when list is empty and no fallback is provided.</exception>
    public static T GetRandom<T>(List<T> list, Func<T> fallbackProvider = null) where T : class
    {
        try
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list), c_listNullErrorMessage);
            }

            if (list.Count == 0)
            {
                if (fallbackProvider != null)
                {
                    return fallbackProvider.Invoke();
                }
                throw new ArgumentException(c_listEmptyErrorMessage, nameof(list));
            }

            int index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error in GetRandom: {ex.Message}");
            throw;
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Validates that a list is not null and not empty.
    /// </summary>
    /// <typeparam name="T">Type of list element.</typeparam>
    /// <param name="list">The list to validate.</param>
    /// <param name="paramName">The name of the parameter for error messages.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when list is null.</exception>
    /// <exception cref="System.ArgumentException">Thrown when list is empty.</exception>
    private static void ValidateList<T>(List<T> list, string paramName)
    {
        if (list == null)
        {
            throw new ArgumentNullException(paramName, c_listNullErrorMessage);
        }

        if (list.Count == 0)
        {
            throw new ArgumentException(c_listEmptyErrorMessage, paramName);
        }
    }
    #endregion
} 