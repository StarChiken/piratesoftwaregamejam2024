using System;
using System.Collections.Generic;

namespace Base.Core.Managers
{
    /// <summary>
    /// Manages player data, followers, devotion, and resources.
    /// </summary>
    [Serializable]
    public class Player : BaseManager
    {
        private readonly PlayerConfig _config;
        private readonly PlayerNameProvider _nameProvider;

        /// <summary>
        /// The player's character name.
        /// </summary>
        public string CharacterName { get; private set; }
        /// <summary>
        /// The list of followers for the player.
        /// </summary>
        public IReadOnlyList<Citizen> FollowerCount => _followerCount;
        private readonly List<Citizen> _followerCount = new();
        /// <summary>
        /// The player's devotion system.
        /// </summary>
        public Devotion Devotion { get; private set; }
        /// <summary>
        /// The player's resources.
        /// </summary>
        public int Resources { get; set; }

        /// <summary>
        /// Initializes a new player with starting followers and devotion using the provided configuration.
        /// </summary>
        /// <param name="config">Configuration for the player.</param>
        /// <param name="onComplete">Callback when initialization is complete.</param>
        public Player(PlayerConfig config, Action<BaseManager> onComplete) : base(onComplete)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _nameProvider = new PlayerNameProvider(_config.PlayerNames);
            for (int i = 0; i < _config.StartingFollowerAmount; i++)
            {
                Citizen follower = new Citizen();
                follower.ChangeAttractionAmount(3);
                _followerCount.Add(follower);
            }
            Devotion = new Devotion(new DevotionConfig {
                StartingDevotionPoints = _config.StartingDevotionAmount
            });
            CharacterName = _nameProvider.TakeRandom();
            OnInitComplete();
        }
    }
}