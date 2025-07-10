using UnityEngine;

namespace DesignPatterns.DependencyInjection.Behaviours
{
    [DefaultExecutionOrder(-998)]
    public class DynamicProvider : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour target;

        private void Awake()
        {
            Injector.Instance.RegisterDynamicObject(target);
        }

        private void OnDestroy()
        {
            // Avoid recreating the injector when the game closes
            if (Injector.TryGetInstance(out var instance))
            {
                instance.DeregisterDynamicObject(target);
            }
        }
    }
}