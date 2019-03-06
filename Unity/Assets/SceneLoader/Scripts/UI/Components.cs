﻿using UnityEngine;

namespace SceneLoader.UI
{
	public class Components : MonoBehaviour {

        public static Components Instance;
        private void Awake ()
        {
            Instance = this;
        }

        public Visibility SceneSelector;
        public Visibility Loading;
	}
}
