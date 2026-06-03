using UnityEngine;
using TMPro;

public class TransitionUI : MonoBehaviour
{
    public static TransitionUI Instance;

    [SerializeField] private GameObject root;
    [SerializeField] private TMP_Text text;


    private void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(string message)
    {
        root.SetActive(true);
        text.text = message;
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}
