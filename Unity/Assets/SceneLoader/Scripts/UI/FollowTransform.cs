using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace SceneLoader.UI
{
    [RequireComponent (typeof (GameObjectEntity))]
    public class FollowTransform : MonoBehaviour
    {
        public Transform Target;
    }

    class EntityFollower : ComponentSystem
    {
        struct Components
        {
            public RectTransform transform;
            public FollowTransform followTransform;
        }

        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;
            Vector3 cameraPos = Camera.main.transform.position;

            foreach (var e in GetEntities<Components>())
            {
                e.transform.position = e.followTransform.Target.position + Vector3.up;
                e.transform.rotation = Quaternion.LookRotation(e.transform.position - cameraPos);
            }
        }
    }
}

