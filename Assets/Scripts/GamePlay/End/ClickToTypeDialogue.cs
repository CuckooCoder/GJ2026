using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ClickToTypeDialogue : MonoBehaviour
{
    public TMP_Text textUI;
    public List<string> lines;
    public float typeSpeed = 0.03f;

    int index;
    bool isTyping;
    Coroutine co;

    void Start()
    {
        index = 0;
        StartTyping();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnClick();
        }
    }

    void OnClick()
    {
        if (isTyping)
        {
            StopCoroutine(co);
            textUI.text = lines[index];
            isTyping = false;
            return;
        }

        index++;

        if (index >= lines.Count)
        {
            textUI.text = "";
            Debug.Log("END");
            return;
        }

        StartTyping();
    }

    void StartTyping()
    {
        co = StartCoroutine(Type(lines[index]));
    }

    IEnumerator Type(string s)
    {
        isTyping = true;
        textUI.text = "";

        foreach (char c in s)
        {
            textUI.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }
}