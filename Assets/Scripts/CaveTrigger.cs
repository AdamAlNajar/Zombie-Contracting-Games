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
            Dialog.Instance.dialogLines = new string[]
            {
                "You step into the cave...",
                "It's cold and dark.",
                "Stalactites hang from the ceiling like sharp teeth.",
                "Water drips echo through the chamber...",
                "Something is watching you from the shadows.",
                "You hear a low growl ahead.",
                "Maybe you should be careful..."
            };
            
            // IMPORTANT: Activate textBox BEFORE starting dialog
            Dialog.Instance.textBox.SetActive(true);
        
            Dialog.Instance.StartDialog();
        }
    }

}