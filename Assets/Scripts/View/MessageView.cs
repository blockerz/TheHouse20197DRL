using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageView : MonoBehaviour
{
    [SerializeField] private Text messagesText;


    public void PostMessageLog(Queue<string> messages, Color color)
    {
        string toPrint = "";

        string[] lines = messages.ToArray();

        for (int i = 0; i < lines.Length; i++)
        {
            toPrint += lines[i];
            if (i < lines.Length - 1)
            {
                toPrint += Environment.NewLine;
            }
        }

        messagesText.color = color;
        messagesText.text = toPrint;
    }
}
