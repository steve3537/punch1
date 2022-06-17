using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunchMachin : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private Player player;

    private void OnTriggerStay(Collider other)
    {
        player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (player.isAtk)
            {
                text.text = (player.Atk * 2).ToString();
            }
        }
    }
}
