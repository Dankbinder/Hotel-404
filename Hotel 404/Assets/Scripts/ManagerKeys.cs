using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerKeys : MonoBehaviour
{
    private List<Colorkeys> m_keys = new List<Colorkeys>();

    public void Addkey(Colorkeys key)
    {
        if (m_keys.Contains(key))
        {
            return;
        }
        else
        {
            m_keys.Add(key);
        }
    }

    public bool DoWeHaveTheKey(Colorkeys key)
    {
        return m_keys.Contains(key);
    }
}
