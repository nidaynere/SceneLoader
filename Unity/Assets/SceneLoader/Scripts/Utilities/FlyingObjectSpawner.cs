using UnityEngine;
using System.Collections;

namespace SceneLoader.Utilities
{
    public class FlyingObjectSpawner : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Spawn());
        }

        public Transform prefab;

        int max = 20;
        IEnumerator Spawn()
        {
            if (max > 0)
            {
                max--;

                yield return new WaitForSeconds(0.5f);

                Instantiate(prefab, transform.position + Random.insideUnitSphere * 8, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));

                StartCoroutine(Spawn());
            }
        }
    }
}
