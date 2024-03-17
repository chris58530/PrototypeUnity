using UniRx;
using UnityEngine;

namespace @_.Scripts.Ability
{
    [CreateAssetMenu(fileName = "GunAbilityData", menuName = "Ability/GunAbility", order = 8)]
    public class GunAbilitySO : AbilityBase
    {
        [SerializeField] private GameObject bullet;

        public override void AbilityAlgorithm()
        {
            //Hold this ability will do 
            Transform point = GameObject.Find("AbilityShootPoint").transform;

            GameObject obj = Instantiate(bullet, point.position, point.rotation);
            Destroy(obj, 5);

            Observable.EveryUpdate().Subscribe(_ =>
            {
                obj.GetComponent<Rigidbody>().velocity = obj.transform.forward * 250;
                obj.GetComponent<Rigidbody>().isKinematic = false;
                Debug.Log("flying");
            }).AddTo(obj);

            Debug.Log("Use Gun Ability");
        }

        public override void StartAbility()
        {
        }

        public override void QuitAbilityAlgorithm()
        {
            Debug.Log("------玩家血量加一!!------");
        }

        public override void TriggerEffect(Collider other)
        {
            //OnTrigger enemy will do 
            Debug.Log("Use fire TriggerEffect ");
        }
    }
}