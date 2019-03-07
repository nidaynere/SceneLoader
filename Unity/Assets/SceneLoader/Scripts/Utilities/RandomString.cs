using System;

namespace SceneLoader.Utilities
{
    /// <summary>
    /// Serializable random string for load from StreamingAssets. Currently used by hints only.
    /// </summary>
    [Serializable]
    public class RandomString
    {
        public string[] Values;
    }
}
