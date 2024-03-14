using UnityEngine;

namespace @_.Scripts.Enemy.BossA
{
    public class BossACrystalBomb : MonoBehaviour
    {
        [SerializeField] private GameObject[] crystal;
        [SerializeField] private GameObject explo;
        [SerializeField] private LayerMask crystalLayer;
        [SerializeField] private LayerMask exploLayer;

        private void OnTriggerEnter(Collider other)
        {
            SpawnExplo(other);
            SpawnCrystal(other);
        }

        void SpawnCrystal(Collider other)
        {
            if (crystalLayer != (crystalLayer | (1 << other.gameObject.layer))) return;


            Vector3 offset = new Vector3(transform.position.x, 0, transform.position.z);

            Crystal crystalComponent =
                Instantiate(crystal[Random.Range(0, crystal.Length)], offset, Quaternion.identity)
                    .GetComponent<Crystal>();
            crystalComponent.canRelife = false;
            Destroy(gameObject);
        }

        void SpawnExplo(Collider other)
        {
            if (exploLayer != (exploLayer | (1 << other.gameObject.layer))) return;


            GameObject obj = Instantiate(explo, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(obj, 0.5f);
        }
    }
}