using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    public TMP_Text text;
    public string[] dialogLines;
    public float textSpeed;
    public bool DialogActive;
    public UnityEvent onDialogComplete;
    int index;
    public GameObject textBox;
    private CursorManager cursorManager;

    void Awake()
    {
        // Hide textBox in Awake() so it runs BEFORE any other script's Start().
        // This prevents a race where another script's Start() shows the box,
        // then this Start() hides it (order is undefined between objects).
        if (textBox != null)
            textBox.SetActive(false);
    }

    void Start()
    {
        if (text != null)
            text.text = string.Empty;

        cursorManager = FindFirstObjectByType<CursorManager>();
    }

    void Update()
    {
        if (!DialogActive || dialogLines == null || dialogLines.Length == 0)
            return;

        // Don't progress dialog while game is paused
        if (PauseMenu.IsPaused)
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
        
        // Notify cursor manager to show cursor during dialog
        if (cursorManager == null)
            cursorManager = FindFirstObjectByType<CursorManager>();
        if (cursorManager != null)
            cursorManager.OnDialogOpened();
        
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
            onDialogComplete?.Invoke(); 
            // END OF DIALOG - CLEAR EVERYTHING
            DialogActive = false;
            
            // Notify cursor manager to hide cursor again
            if (cursorManager == null)
                cursorManager = FindFirstObjectByType<CursorManager>();
            if (cursorManager != null)
                cursorManager.OnDialogClosed();
            
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
        
        // Notify cursor manager
        if (cursorManager == null)
            cursorManager = FindFirstObjectByType<CursorManager>();
        if (cursorManager != null)
            cursorManager.OnDialogClosed();
        
        if (text != null)
            text.text = string.Empty;
        
        if (textBox != null)
            textBox.SetActive(false);
        
        // Set to empty array (0 elements)
        dialogLines = new string[0];
    }
}
