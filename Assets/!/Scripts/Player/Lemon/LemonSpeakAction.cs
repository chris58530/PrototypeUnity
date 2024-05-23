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
     float keepTime = 2;
    private float _currentTime;
    public string[] dialog;
    public SharedBool isImportantSpeaking;

    public override void OnStart()
    {
        _currentTime = 0;
        dialogText.Value.GetComponent<TMP_Text>().text = dialog[Random.Range(0, dialog.Length)];

        dialogCanvas.Value.SetActive(true);
    }

    public override TaskStatus OnUpdate()
    {
        _currentTime += Time.deltaTime;


        if (_currentTime > keepTime || isImportantSpeaking.Value)
            return TaskStatus.Success;

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        dialogCanvas.Value.SetActive(false);
        // dialogText.Value.GetComponent<TMP_Text>().text = "";
        _currentTime = 0;
    }
}