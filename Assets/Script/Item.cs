using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour
{
    public Transform parent;
    public enum Type { Ammo, Coin, Gernade, Heart, Weapon };
    public Type type;
    public int value;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(parent);
            other.transform.localPosition = Vector3.zero;
            Debug.Log("ÇØ¸Ó");
        }
    }


}
