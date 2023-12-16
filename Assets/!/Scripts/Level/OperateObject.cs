using System;
using UnityEngine;

namespace _.Scripts.Level
{
    public class OperateObject : TaskObject
    {
        [SerializeField] private GameObject reviewPanel = null;

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
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 6)
            {
                    reviewPanel.SetActive(false);
                    isDone = false;
            }
        }
    }
}