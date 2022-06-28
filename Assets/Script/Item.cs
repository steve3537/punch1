using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : MonoBehaviour
{
    public Transform _parent;
    public enum Type { Ammo, Coin, Gernade, Heart, Weapon };
    public Type type;
    public int value;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(_parent);
            other.transform.SetParent(_parent);
            other.transform.localPosition = new Vector3(0,0,0);
            Debug.Log("ÇØ¸Ó");
        }
    }


}
