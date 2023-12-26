using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace @_.Scripts.Enemy.Chameleon
{
    public class PushChameleonBase : Enemy, IDamageable
    {
        public Image hpImage;

        [SerializeField] private float maxHp;
        private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();
        [SerializeField] private GameObject bomb;
        private void Start()
        {
            Initialize();
            _currentHp.Subscribe(_ => { hpImage.fillAmount = _currentHp.Value / maxHp; }).AddTo(this);

            bomb.OnTriggerEnterAsObservable().Subscribe(_ =>
            {
                
            }).AddTo(this);
        }

        void Initialize()
        {
            _currentHp.Value = maxHp;
        }

        public void OnTakeDamage(float value)
        {
            bt.SendEvent("GetHurt");
            _currentHp.Value -= value;

            if (_currentHp.Value <= 0) OnDied();
        }


        public void OnDied()
        {
            bt.SendEvent("OnDied");
        }

   
    }
}