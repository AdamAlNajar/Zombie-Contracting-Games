using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int minAmmo = 10;
    public int maxAmmo = 40;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Gun[] guns = FindObjectsOfType<Gun>();

        if (guns.Length == 0)
            return;

        // Pick random gun
        Gun randomGun = guns[Random.Range(0, guns.Length)];

        // Random ammo amount
        int ammoAmount = Random.Range(minAmmo, maxAmmo + 1);

        randomGun.AddAmmo(ammoAmount);

        //Debug.Log("Added " + ammoAmount + " ammo to " + randomGun.name);
        MessageSystem.Instance.SetColor(Color.white);
        MessageSystem.Instance.ShowMessage("Added " + ammoAmount + " ammo to " + randomGun.name, 3f);

        Destroy(gameObject);
    }
}
