using UnityEngine;
using UnityEngine.UI;

namespace SceneLoader.UI
{
    /// <summary>
    /// All UI elements for reaching from other classes.
    /// </summary>
	public class Components : MonoBehaviour {

        public static Components Instance;
        private void Awake ()
        {
            Instance = this;
        }

        public Visibility SceneSelector;
        public Visibility Loading;
        public Filler LoadingFill;
        public FollowTransform FlyingCanvas;
	}
}

