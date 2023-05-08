using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class fpsUI : MonoBehaviour
{
    private TMP_Text _text;
    
    void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void UpdateUI(int number)
    {
        this._text.text = number.ToString();
    }

    public void UpdateUI(float number)
    {
        _text.text = number.ToString();
    }

    public void UpdateUI(string text)
    {
        this._text.text = text;
    }

    public void UpdateUI(ref string text)
    {
        this._text.text = text;
    }
}