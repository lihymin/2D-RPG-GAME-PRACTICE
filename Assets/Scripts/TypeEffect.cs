using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypeEffect : MonoBehaviour
{
    public string targetMsg;
    public int CharPerSeconds;
    public GameObject endCursor;
    public Text msgText;
    int index;
    float interval;
    AudioSource audio;
    public bool isEffecting;

    void Awake()
    {
        msgText = GetComponent<Text>();
        audio = GetComponent<AudioSource>();
    }
    public void SetMsg(string msg)
    {
        if (isEffecting) {
            CancelInvoke("Effecting");
            msgText.text = targetMsg;
            EffectEnd();
        }

        else {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        endCursor.SetActive(false);

        interval = 1.0f / CharPerSeconds;
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        isEffecting = true;

        if (msgText.text == targetMsg) {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];

        if (targetMsg[index] != ' ' && targetMsg[index] != '.' && targetMsg[index] != '?') {
            audio.Play();
        }

        index++;
        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        isEffecting = false;
        endCursor.SetActive(true);
    }
}
