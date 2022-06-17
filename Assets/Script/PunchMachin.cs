using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchMachin : MonoBehaviour
{
    private CharacterController player;

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<CharacterController>();
        
         
    }
}
