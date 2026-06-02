using UnityEngine;
using TMPro;
using System.Collections;

public class Dialog : MonoBehaviour
{
    public TMP_Text text;
    public string[] dialogLines;
    public float textSpeed;
    public bool DialogActive;
    int index;
    public GameObject textBox;

    
    void Start()
    {
        if (text != null)
            text.text = string.Empty;
        
        if (textBox != null)
            textBox.SetActive(false);
    }

    void Update()
    {
        if (!DialogActive || dialogLines == null || dialogLines.Length == 0)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (index < dialogLines.Length && text.text == dialogLines[index])
            {
                NextLine();
            }
            else if (index < dialogLines.Length)
            {
                StopAllCoroutines();
                text.text = dialogLines[index];
            }
        }
    }

    public void StartDialog()
    {
        Debug.Log("StartDialog called!");
        
        if (dialogLines == null || dialogLines.Length == 0)
        {
            Debug.LogError("No dialog lines to display!");
            return;
        }

        // Reset to first line
        index = 0;
        DialogActive = true;
        
        // Make sure textBox is active
        if (textBox != null)
        {
            textBox.SetActive(true);
        }
        
        // Clear text and start typing
        if (text != null)
        {
            text.text = string.Empty;
            StopAllCoroutines();
            StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine()
    {
        if (index >= dialogLines.Length)
        {
            yield break;
        }
        
        text.text = string.Empty;
        string currentLine = dialogLines[index];
        
        foreach (char c in currentLine.ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < dialogLines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        } 
        else
        {
            // END OF DIALOG - CLEAR EVERYTHING
            DialogActive = false;
            
            // Clear the text
            if (text != null)
                text.text = string.Empty;
            
            // Hide the text box
            if (textBox != null)
                textBox.SetActive(false);
            
            // CRITICAL: Set dialogLines to an EMPTY array (no elements)
            dialogLines = new string[0];  // This creates an array with 0 elements
            
            Debug.Log($"Dialog cleared! dialogLines length is now: {dialogLines.Length}");
        }
    }
    
    public void ClearDialog()
    {
        StopAllCoroutines();
        DialogActive = false;
        
        if (text != null)
            text.text = string.Empty;
        
        if (textBox != null)
            textBox.SetActive(false);
        
        // Set to empty array (0 elements)
        dialogLines = new string[0];
    }
}
