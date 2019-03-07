using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace SceneLoader.Utilities
{
    /// <summary>
    /// Flying object, randomly flies on sky.
    /// </summary>
    [RequireComponent(typeof(GameObjectEntity))]
    public class FlyingObject : MonoBehaviour
    {
        class EntityFlyingObject : ComponentSystem
        {
            struct Components
            {
                public Transform transform;
                public FlyingObject flyingObject;
            }

            protected override void OnUpdate()
            {
                float deltaTime = Time.deltaTime;
                Vector3 Rotate = new Vector3(0, 180, 0);

                foreach (var e in GetEntities<Components>())
                {
                    e.transform.position += e.transform.forward * deltaTime * 5;
                    e.transform.rotation *= Quaternion.Euler(Rotate* deltaTime);
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            CreateFlyingCanvas();
        }

        private void CreateFlyingCanvas()
        {
            UI.FollowTransform follower = Instantiate(UI.Components.Instance.FlyingCanvas);
            follower.Target = transform;
            follower.gameObject.SetActive(true);
            follower.GetComponentInChildren<UnityEngine.UI.Text>().text = Random.Range(0, 9000000).ToString();
        }
    }
}

