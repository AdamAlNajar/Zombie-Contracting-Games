using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float runningMult = 3.5f;
    bool isRunning = false;

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
        if (isRunning == false)
            rb.linearVelocity = moveInput * speed;
        if(isRunning == true)
            rb.linearVelocity = moveInput * speed * runningMult;
    }
}