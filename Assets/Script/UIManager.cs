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
        text.text = $"���� �ӵ� {player.spd}\n���ݷ�{player.Atk}";
    }
}
