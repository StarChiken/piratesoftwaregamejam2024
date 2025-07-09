using System;
using System.Threading.Tasks;

namespace Base.Core.Managers
{
    /// <summary>
    /// Base class for all managers, providing async initialization and GameManager access.
    /// </summary>
    public class BaseManager
    {
        /// <summary>
        /// Provides access to the global GameManager instance.
        /// </summary>
        protected GameManager GameManager => GameManager.Instance;
        private readonly Action<BaseManager> _onCompleteAction;

        /// <summary>
        /// Constructs a new BaseManager and stores the completion callback.
        /// </summary>
        protected BaseManager(Action<BaseManager> onComplete)
        {
            _onCompleteAction = onComplete;
        }

        /// <summary>
        /// Marks initialization as complete and invokes the callback asynchronously.
        /// </summary>
        protected async void OnInitComplete()
        {
            await Task.Delay(500);
            _onCompleteAction?.Invoke(this);
        }
    }
}