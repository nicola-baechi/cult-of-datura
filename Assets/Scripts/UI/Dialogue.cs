using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [TextArea] public string text;
    
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    
    private int index;
    
    private bool isPressed;
    
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        lines[0] = text;
        StartDialogue();
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !isPressed)
        {
            isPressed = true;
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
            
        }
    }
    
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypteLine());
    }
    
    IEnumerator TypteLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypteLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
