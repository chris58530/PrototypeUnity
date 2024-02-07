using UnityEngine;

namespace @_.Scripts.Enemy.BossA
{
    public class BossACrystalBomb : MonoBehaviour
    {
        [SerializeField] private GameObject crystal;
        [HideInInspector] public Transform target;
        public LayerMask groundLayer;


        private void OnTriggerEnter(Collider other)
        {
            if (groundLayer == (groundLayer | (1 << other.gameObject.layer)))
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            var obg = Instantiate(crystal, transform.position, Quaternion.identity);
        }
    }
}