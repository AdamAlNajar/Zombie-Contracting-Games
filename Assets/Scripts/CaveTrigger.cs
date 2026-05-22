using UnityEngine;

public class CaveTrigger : MonoBehaviour
{
    public GameObject transportPoint; // Point to transport player to
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Cave Entered"); // Transport player to cave
            GameObject player = other.gameObject;
            player.transform.position = transportPoint.transform.position;
        }
    }
    // Make Some sort of story to the cave
    // Make Cave interior
}
