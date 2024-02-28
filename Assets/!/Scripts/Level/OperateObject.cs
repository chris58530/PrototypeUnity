using System;
using _.Scripts.Task;
using UnityEngine;

namespace _.Scripts.Level
{
    public class OperateObject :MonoBehaviour, ITaskObject
    {
        [SerializeField] private GameObject reviewPanel = null;
        [SerializeField] private GameObject Puller;

        private void Start()
        {
            reviewPanel.SetActive(false);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == 6)
            {
                reviewPanel.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isDone = true;
                    TaskManager.checkTaskAction?.Invoke(0);

                    Puller.GetComponent<Animator>().Play("PullDown");
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 6)
            {
                reviewPanel.SetActive(false);
            }
        }

        public bool isDone { get; set; }
    }
}