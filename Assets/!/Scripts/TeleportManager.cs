using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject destination;

    public void TeleportPlayer()
    {
       player.transform.position = destination.transform.position;
    }
}
