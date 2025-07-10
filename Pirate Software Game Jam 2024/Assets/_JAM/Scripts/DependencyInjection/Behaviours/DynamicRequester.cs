using UnityEngine;

namespace DesignPatterns.DependencyInjection.Behaviours
{
    [DefaultExecutionOrder(-999)]
    public class DynamicRequester : MonoBehaviour
    {
        [SerializeField] private bool includeChildren;

        private void Awake()
        {
            if (includeChildren)
            {
                var componentsInChildren = transform.GetComponentsInChildren<MonoBehaviour>(true);

                Injector.Instance.HandleInjections(componentsInChildren);
            }
            else
            {
                var components = transform.GetComponents<MonoBehaviour>();

                Injector.Instance.HandleInjections(components);
            }
        }
    }
}