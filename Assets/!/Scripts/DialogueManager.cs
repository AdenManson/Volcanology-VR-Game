using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button[] optionButtons;
    [SerializeField] private Button skipButton;

    [SerializeField] private float typingSpeed = 0.05f;

    private List<dialogueString> dialogueList;

    private int currentDialogueIndex = 0;

    private InputDevice leftController;
    private InputDevice rightController;

    private bool skipDialogue = false;

    private void Start()
    {
        dialogueParent.SetActive(false);


        //FetchInputDevices();

    }

    public void DialogueStart(List<dialogueString> textToPrint)
    {
        dialogueParent.SetActive(true);
        
        dialogueList = textToPrint;
        currentDialogueIndex = 0;

        DisableButtons();

        StartCoroutine(PrintDialogue());
    }

    private void DisableButtons()
    {
        foreach (var optionButton in optionButtons)
        {
            optionButton.interactable = false;
            optionButton.GetComponentInChildren<TMP_Text>().text = "No option";
            optionButton.gameObject.SetActive(false);
        }
    }

    private bool optionSelected = false;
    private IEnumerator PrintDialogue()
    {
        while(currentDialogueIndex < dialogueList.Count)
        {
            dialogueString line = dialogueList[currentDialogueIndex];

            line.startDialogueEvent?.Invoke();

            if(line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.text));

                for (int i = 0; i < optionButtons.Length; i++)
                {
                    optionButtons[i].interactable = true;
                    optionButtons[i].gameObject.SetActive(true);
                    
                    int index = i; // Capture the index variable
                    optionButtons[i].GetComponentInChildren<TMP_Text>().text = line.dialogueOptions[index].answerOption;
                    optionButtons[i].onClick.AddListener(() => HandleOptionSelected(line.dialogueOptions[index].optionIndexJump));
                }

                yield return new WaitUntil(() => optionSelected);
            } 
            else
            {
                skipButton.onClick.AddListener(() => SkipDialogue());
                yield return StartCoroutine(TypeText(line.text));
            }

            line.endDialogueEvent?.Invoke();

            optionSelected = false;
        }
        DialogueStop();
    }

    private void SkipDialogue()
    {
        dialogueString line = dialogueList[currentDialogueIndex];
        if (line.isEnd)
        {
            DialogueStop();
            return;
        }

        if (!dialogueText.text.Equals(dialogueList[currentDialogueIndex].text))
        {
            // If the dialogue is still printing, immediately show the full message
            dialogueText.text = dialogueList[currentDialogueIndex].text;
            skipDialogue = false;
        }
        else
        {
            // If the full message is already shown, move on to the next dialogue
            skipDialogue = true;
        }
    }

    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true;
        DisableButtons();

        currentDialogueIndex = indexJump;
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach(char letter in text.ToCharArray())
        {
            if (!dialogueText.text.Equals(dialogueList[currentDialogueIndex].text))
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        if (!dialogueList[currentDialogueIndex].isQuestion)
        {
            //yield return new WaitUntil(() => leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool primaryVal) && primaryVal);
            yield return new WaitUntil(() => skipDialogue == true);
            skipDialogue = false;
        }

        //if (dialogueList[currentDialogueIndex].isEnd)
            //DialogueStop();

        currentDialogueIndex++;
    }

    private void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);
    }

    private void Update() // TODO: Shift this functionality into a seperate InputManager class
    {
        if (leftController.isValid && rightController.isValid)
            return;

        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left))
            {
                leftController = device;
            }
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right))
            {
                rightController = device;
            }
        }

    }
}
