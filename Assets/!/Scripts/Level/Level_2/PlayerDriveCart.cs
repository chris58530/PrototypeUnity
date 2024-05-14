using System;
using System.Collections;
using UnityEngine;

namespace @_.Scripts.Level.Level_2
{
    public class PlayerDriveCart : MonoBehaviour
    {
        [SerializeField] private GameObject sitPoint;
        [SerializeField] private GameObject playerObject;
        [Range(0, 1), SerializeField] private float speed;
        [SerializeField] private float delayTime;
        private bool _startMoving;

        private void OnEnable()
        {
            TimeLineManager.onQuitTimelLine += OnArriveEnd;
        }

        private void OnDisable()
        {
            TimeLineManager.onQuitTimelLine -= OnArriveEnd;
        }

        public void OnArriveEnd()
        {
            if (!_startMoving) return;
            playerObject.transform.parent = null;
            playerObject.transform.position += new Vector3(0,-7,0);
            playerObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            Destroy(gameObject);
        }

        public void StartMoveToSitPoint()
        {
            Debug.Log("Start Moving player to sit point");

            StartCoroutine(nameof(GoToSitPoint));
        }

        IEnumerator GoToSitPoint()
        {
            yield return new WaitForSeconds(delayTime);
            _startMoving = true;
            playerObject.transform.parent = sitPoint.transform;
            while (Vector3.Distance(playerObject.transform.position, sitPoint.transform.position) > 0.1f)
            {
                Debug.Log("Moving player to sit point");
                playerObject.transform.position =
                    Vector3.MoveTowards(playerObject.transform.position, sitPoint.transform.position, speed);
                yield return null;
            }
        }
    }
}