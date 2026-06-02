using UnityEngine;
using UnityEngine.Events;
public class ButtonScriptable : MonoBehaviour
{
    public UnityEvent onPressed;

    private bool pressed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pressed)
            return;

        if (!other.CompareTag("Player"))
            return;

        pressed = true;

        onPressed.Invoke();

        Destroy(gameObject);
    }
}