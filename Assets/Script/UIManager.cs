using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private Player player;
    [SerializeField]
    private Text text;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        text.text = $"현재 속도 {player.spd}\n공격력{player.Atk}";
    }
}
