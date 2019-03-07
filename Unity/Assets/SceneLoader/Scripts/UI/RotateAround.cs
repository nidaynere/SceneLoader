using UnityEngine;
using Unity.Entities;

[RequireComponent (typeof (GameObjectEntity))]
public class RotateAround : MonoBehaviour
{
    class EntityVisibility : ComponentSystem
    {
        struct Components
        {
            public RectTransform transform;
            public RotateAround rotator;
        }

        protected override void OnUpdate()
        {
            float deltaTime = Time.deltaTime;

            foreach (var e in GetEntities<Components>())
            {
                e.transform.Rotate(Vector3.forward, deltaTime * e.rotator.RotateSpeed);
            }
        }
    }

    public float RotateSpeed = 120;
}
