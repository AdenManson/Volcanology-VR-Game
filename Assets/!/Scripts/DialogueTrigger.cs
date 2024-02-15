using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    [SerializeField] private Canvas GUICanvas;

    [SerializeField] private GameObject player;

    private bool hasSpoken = false;

    public void Start()
    {
        GUICanvas.enabled = false;
        //GUICanvas.enabled = true; // TODO: Make it so you can get repeating dialogue when convo has already happened
        player.gameObject.GetComponent<DialogueManager>().DialogueStart(dialogueStrings);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasSpoken)
        {
            Debug.Log("Collision enter");
            GUICanvas.enabled = true; // TODO: Make it so you can get repeating dialogue when convo has already happened
            other.gameObject.GetComponent<DialogueManager>().DialogueStart(dialogueStrings);
            hasSpoken = true;
        }
    }
}

[System.Serializable]
public class DialogueOption
{
    public string answerOption;
    public int optionIndexJump;

    public DialogueOption(string answerOption, int optionIndexJump)
    {
        this.answerOption = answerOption;
        this.optionIndexJump = optionIndexJump;
    }
}

[System.Serializable]
public class dialogueString
{
    public string text; // NPC text
    public bool isEnd; // is line if final line of sentence

    [Header("Branch")]
    public bool isQuestion;
    public DialogueOption[] dialogueOptions; 

    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;
}
