

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorLock : MonoBehaviour
{
    [SerializeField] private Colorkeys m_lock;
    [SerializeField] private Animator m_animator;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<ManagerKeys>().DoWeHaveTheKey(key: m_lock))
        {
            m_animator.SetBool("Lock", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_animator.SetBool("Lock", true);
        }
    }
}
