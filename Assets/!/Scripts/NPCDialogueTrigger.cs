using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    [SerializeField] private NPCConversation conversation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ConversationManager.Instance.StartConversation(conversation);
        }
    }
}
