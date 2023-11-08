using UnityEngine;

namespace _.Scripts.Tools
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;
        public static T Instance => instance;

        protected virtual void Awake()
        {
            if (instance != null)
                Destroy(gameObject);
            else
            {
                instance = (T)this;
                Debug.Log($"Singleton ({typeof(T).Name}) has been Created");
            }
            
        }
        // public static bool IsInitialized
        // {
        //     get { return instance != null; }
        // }

        protected virtual void OnDestroy()
        {
            if (instance == this)
                instance = null;
            Debug.Log($"Singleton ({typeof(T).Name}) has been destroyed");
        }
    }
}
