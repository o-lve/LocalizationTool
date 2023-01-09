using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizetionText : MonoBehaviour
{
    public int index = 0;
    private void Awake()
    {
        var uiText = this.GetComponent<Text>();
    }
}
