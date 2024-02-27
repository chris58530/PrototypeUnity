using System;
using UnityEngine;

namespace _.Scripts.Task
{
    [Serializable]
    public class Task
    {
        public string name;
        public TaskObject[] taskObjects;
        public GameObject[] taskResults;
    }
}