using UnityEngine;

public class CaveTrigger : MonoBehaviour
{
    public GameObject transportPoint; // Point to transport player to
    public Dialog dialogCave; // Reference to Dialog component (optional, can use Dialog.Instance)
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Cave Entered");
            
            // Transport player to cave
            GameObject player = other.gameObject;
            player.transform.position = transportPoint.transform.position;
            
            // Set dialog lines for the cave story
            dialogCave.dialogLines = new string[]
            {
                "You step into the cave...",
                "The air is thick and stale. Somewhere in the darkness, water drips onto stone.",
                "Stalactites hang from the ceiling like the teeth of some ancient beast.",
                "Shadows dance at the edge of your vision — or is something moving in them?",
                "A low growl rumbles from deeper in the tunnel. You're not alone.",
                "The temperature drops. Whatever's down here, it knows you've arrived.",
                "Steady your weapon. There's no turning back now."
            };
            
            // IMPORTANT: Activate textBox BEFORE starting dialog
           dialogCave.textBox.SetActive(true);
        
            dialogCave.StartDialog();
        }
    }

}