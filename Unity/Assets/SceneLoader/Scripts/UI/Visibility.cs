using UnityEngine;
using UnityEngine.Events;
using Unity.Entities;

namespace SceneLoader.UI
{
    class EntityVisibility : ComponentSystem
    {
        struct Components
        {
            public Visibility visibility;
            public CanvasGroup canvasGroup;
        }

        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;

            foreach (var e in GetEntities<Components>())
            {
                if (!e.visibility.Updater(e.canvasGroup, deltaTime))
                    return;
            }
        }
    }

    [RequireComponent (typeof (GameObjectEntity))]
    [RequireComponent (typeof(CanvasGroup))]
    /// <summary>
    /// Open/Close UI panels smoothly.
    /// </summary>
    public class Visibility : MonoBehaviour
    {
        #region publics
        /// <summary>
        /// Transtition speed of the panel while opening and closing.
        /// </summary>
        public float TransitionSpeed = 4f;

        /// <summary>
        /// Hide after opened? If it's 0, it wont hide.
        /// </summary>
        public float HideIn = 0;

        /// <summary>
        /// Is the panel is active?
        /// </summary>
        [HideInInspector]
        public bool activeSelf;
        #endregion

        #region privates
        private float defaultA = 1f;
        private float closeAt;
        private float fValue;
        private int alphaDown;
        #endregion

        #region actions
        public UnityEvent OnOpened;
        public UnityEvent OnClosed;
        #endregion

        /// <summary>
        /// Open/close the panel
        /// </summary>
        /// <param name="show"></param>
        public void Set (bool show)
        {
            if (alphaDown > 0 && show)
            {
                //Already opening.
                return;
            }

            if (alphaDown < 0 && !show)
            {
                //Already closing.
                return;
            }

            if (gameObject != null && activeSelf && !gameObject.activeSelf)
            {
                activeSelf = false;
                return;
            }

            activeSelf = show;

            if (show)
            {
                gameObject.SetActive(true);

                if (HideIn != 0)
                {
                    closeAt = Time.time + HideIn;
                }

                alphaDown = 1;
            }
            else
            {
                alphaDown = -1;
            }
        }

        /// <summary>
        /// Visibilities are controlled by on an another thread.
        /// </summary>
        public bool Updater (CanvasGroup canvas, float deltaTime)
        {
            if (closeAt > 0 && closeAt < Time.time)
            {
                closeAt = 0;
                Set(false);
            }

            if (alphaDown != 0)
            {
                canvas.alpha += alphaDown * deltaTime * TransitionSpeed;
                fValue = canvas.alpha;

                if ((alphaDown == 1 && fValue >= defaultA) ||
                (alphaDown == -1 && fValue <= TransitionSpeed/100f))
                {
                    if (alphaDown == -1)
                    {
                        gameObject.SetActive(false);
                        canvas.alpha = 0;

                        OnClosed?.Invoke();

                        alphaDown = 0;

                        return false;
                    }
                    else
                    {
                        alphaDown = 0;
                        canvas.alpha = 1;
                        OnOpened?.Invoke();
                    }
                }
            }

            return true;
        }
    }
}
