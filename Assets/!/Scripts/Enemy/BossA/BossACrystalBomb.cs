using UnityEngine;

namespace @_.Scripts.Enemy.BossA
{
    public class BossACrystalBomb : MonoBehaviour
    {
        [SerializeField] private GameObject crystal;
        [SerializeField] private GameObject explo;
        [HideInInspector] public Transform target;


        private void OnTriggerEnter(Collider other)
        {            SpawnExplo(other);

            SpawnCrystal(other);
        }

        void SpawnCrystal(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) return;


            Instantiate(crystal, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        void SpawnExplo(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

            Instantiate(explo, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}