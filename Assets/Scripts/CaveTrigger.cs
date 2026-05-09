using UnityEngine;

public class CaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Cave Entered"); // Transport player to cave
        }
    }
    // Make Some sort of story to the cave
    // Make Cave interior
}
