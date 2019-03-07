using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.Entities;

namespace SceneLoader.UI
{
    /// <summary>
    /// Smooth image filler.
    /// </summary>
    /// 
    [RequireComponent(typeof(GameObjectEntity))]
    public class Filler : MonoBehaviour
    {
        class EntityFiller : ComponentSystem
        {
            struct Components
            {
                public Filler filler;
                public Image image;
                public RectTransform transform;
            }

            protected override void OnUpdate()
            {
                int val;

                foreach (var e in GetEntities<Components>())
                {
                    if (!e.filler.filling)
                        continue;

                    if (Mathf.Abs(e.image.fillAmount - e.filler.fillAmount) > Time.deltaTime / e.filler.FillTime)
                    {
                        if (e.filler.fillAmount - e.image.fillAmount < 0)
                            val = -1;
                        else val = 1;

                        e.image.fillAmount += val * Time.deltaTime / e.filler.FillTime;

                        e.filler.filling = true;
                    }
                    else
                    {
                        e.image.fillAmount = e.filler.fillAmount;
                        e.filler.filling = false;

                        if (e.filler.fillerFinish != null)
                            e.filler.fillerFinish.Invoke();
                    }
                }
            }
        }

        public float FillTime = 5;

        public UnityEvent fillerFinish;

        public float _tVal;
        public float fillAmount
        {
            get
            {
                return _tVal;
            }

            set
            {
                _tVal = value;
                filling = true;
            }
        }

        [HideInInspector]
        public bool filling = false;

        /// <summary>
        /// Set fill amount without a transition effect.
        /// </summary>
        /// <param name="Value"></param>
        public void SetFill(float Value)
        {
            GetComponent<Image>().fillAmount = Value;
        }
    }
}

