using BehaviorDesigner.Runtime;
using UnityEngine;

namespace _.Scripts.Enemy
{
    public class Enemy : MonoBehaviour, IPullable
    {
        [SerializeField] private float pullDistance;
        [SerializeField] private GameObject visualizePullDirection;
        public Vector3 PullDirection { get; set; }

        private BehaviorTree _bt;
        private Rigidbody _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _bt = GetComponent<BehaviorTree>();
        }

        private void Start()
        {
            visualizePullDirection.SetActive(false);
            PullDirection = Vector3.zero;
        }

        private void Update()
        {

            if (PullDirection != Vector3.zero)
            {
                SetVisualizePullDirection(PullDirection);
            }
            else visualizePullDirection.SetActive(false);
        }

        public void Pull()
        {
            _bt.SendEvent("HasStun");
            if (PullDirection == Vector3.zero) return;
            Vector3 dir = PullDirection - transform.position;
            _rb.AddForce(dir.normalized * pullDistance, ForceMode.Impulse);
            PullDirection = Vector3.zero;
        }

        public void SetVisualizePullDirection(Vector3 direction)
        {
            Debug.Log("set active");
            visualizePullDirection.SetActive(true);
            Vector3 look = new Vector3(PullDirection.x,
                visualizePullDirection.transform.position.y, PullDirection.z);
            visualizePullDirection.transform.LookAt(look);
        }

    }
}