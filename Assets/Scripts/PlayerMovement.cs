using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float runningMult = 3.5f;
    bool isRunning = false;
    private Dialog dialog;

    void Start()
    {
        dialog = FindFirstObjectByType<Dialog>();
    }

    Vector2 moveInput;
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(x, y).normalized;
        isRunning = Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        if (dialog.DialogActive)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        if (isRunning == false)
            rb.linearVelocity = moveInput * speed;
        if(isRunning == true)
            rb.linearVelocity = moveInput * speed * runningMult;
    }
}