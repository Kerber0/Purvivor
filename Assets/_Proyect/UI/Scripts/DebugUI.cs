using UnityEngine;
using System.Collections.Generic;

public class DebugUI : MonoBehaviour
{
    private class DebugMessage
    {
        public string text;
        public float timer;

        public DebugMessage(string text, float duration)
        {
            this.text = text;
            this.timer = duration;
        }
    }

    private List<DebugMessage> messages = new List<DebugMessage>();

    void Update()
    {
        // recorrer al revés para poder eliminar
        for (int i = messages.Count - 1; i >= 0; i--)
        {
            messages[i].timer -= Time.deltaTime;

            if (messages[i].timer <= 0)
            {
                messages.RemoveAt(i);
            }
        }
    }

    void OnGUI()
    {
        float startY = 10f;
        float spacing = 22f;

        for (int i = 0; i < messages.Count; i++)
        {
            GUI.Label(
                new Rect(10, startY + i * spacing, 400, 20),
                messages[i].text
            );
        }
    }

    public void ShowMessage(string msg, float duration)
    {
        messages.Add(new DebugMessage(msg, duration));
    }
}