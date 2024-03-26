using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    [SerializeField] private NPCConversation conversation;
    [SerializeField] private GameObject conversationManager;
    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            conversationManager.transform.SetParent(canvas.transform);
            conversationManager.transform.position = canvas.transform.position;
            conversationManager.transform.rotation = canvas.transform.rotation;
            ConversationManager.Instance.StartConversation(conversation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ConversationManager.Instance.EndConversation();
        }
    }
}
