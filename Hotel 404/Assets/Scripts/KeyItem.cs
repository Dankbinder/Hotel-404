using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [SerializeField] private Colorkeys m_key;

    [SerializeField] private int m_Keynumber = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ManagerKeys>().Addkey(m_key);
            Destroy(this.gameObject);
        }
    }
}

public enum Colorkeys
{
    Red,
    Green,
}