using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestDialog : MonoBehaviour
{

    [SerializeField] private XRSimpleInteractable model;
    void Start()
    {
        
    }

    void Update()
    {
       
    }

    public void TriggerDialog() {
        Debug.Log("HEY GUYS!");
    }
}
