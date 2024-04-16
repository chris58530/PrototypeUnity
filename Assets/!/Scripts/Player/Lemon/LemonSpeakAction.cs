using _.Scripts.Enemy.TypeA;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using TMPro;
using UnityEngine.AI;
using UnityEngine;
[TaskCategory("Lemon")]


public class LemonSpeakAction : Action
{
    public SharedGameObject dialogText;
    public SharedGameObject dialogCanvas;
    public float keepTime;
    private float _currentTome;
    public string[] dialog;

    public override void OnStart()
    {
        _currentTome = 0;
        dialogCanvas.Value.SetActive(true);
        dialogText.Value.GetComponent<TMP_Text>().text = dialog[Random.Range(0, dialog.Length)];
    }

    public override TaskStatus OnUpdate()
    {
        _currentTome += Time.deltaTime;


        if (_currentTome > keepTime)
            return TaskStatus.Success;

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        dialogCanvas.Value.SetActive(false);
        dialogText.Value.GetComponent<TMP_Text>().text = "";
    }
}