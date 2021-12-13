using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextApear : MonoBehaviour
{
    public Text text;
    void Start()
    {
        text = GetComponent<Text>();
        StartBlinking();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       /* Color C = text.color;
        C.a = 255f;*/
    }

    IEnumerator Blink()
    {
        while (true)
        {
            switch (text.color.a.ToString())
            {
                case "0":
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    void StartBlinking()
    {
        StopCoroutine("Blink");
        StartCoroutine("Blink");
    }

    void StopBlinking()
    {
        StopCoroutine("Blink");
    }

    
}


