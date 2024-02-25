using _.Scripts.Enemy;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
//coldwater
using System.Collections;


public class ChameleonBase : Enemy, IDamageable
{
    public Image hpImage;
    //coldawter
    public Material ChameleonMat;
    [SerializeField] private Material EmssionMat;
    [SerializeField] private Material OringinMat;
    [SerializeField] private Renderer Body;
    [SerializeField] private Renderer Neck;
    [SerializeField] private Renderer Weapon;

    [SerializeField] private float maxHp;
    private ReactiveProperty<float> _currentHp = new ReactiveProperty<float>();

    private void Start()
    {
        Initialize();
        _currentHp.Subscribe(_ => { hpImage.fillAmount = _currentHp.Value / maxHp; }).AddTo(this);
    }

    void Initialize()
    {
        _currentHp.Value = maxHp;
    }

    public void OnTakeDamage(int value)
    {
        bt.SendEvent("GetHurt");
        _currentHp.Value -= value;
        //coldwater
        Body.material = EmssionMat;
        Neck.material = EmssionMat;
        Weapon.material = EmssionMat;
        Invoke("ResetMaterial", 0.2f);

        if (_currentHp.Value <= 0) OnDied();
    }
    public void OnDied()
    {
        bt.SendEvent("OnDied");
    }

    //coldwater
    public void ResetMaterial()
    {
        Body.material = OringinMat;
        Neck.material = OringinMat;
        Weapon.material = OringinMat;
    }

}
