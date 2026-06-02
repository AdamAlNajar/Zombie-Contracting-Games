using UnityEngine;
using TMPro;
using System.Collections;

public class MessageSystem : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private TMP_Text messageText;

    [Header("Settings")]
    [SerializeField] private float defaultDuration = 2f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        messageText.gameObject.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        ShowMessage(message, defaultDuration);
    }

    public void ShowMessage(string message, float duration)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(ShowRoutine(message, duration));
    }
    public void SetColor(Color color)
    {
        messageText.color = color;
    }

    private IEnumerator ShowRoutine(string message, float duration)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;

        yield return new WaitForSeconds(duration);
        messageText.gameObject.SetActive(false);
    }
}
