using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentWeapon = 0;

    void Start()
    {
        SelectWeapon(currentWeapon);
    }

    void Update()
    {
        // Scroll wheel input
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            currentWeapon++;

            // Loop back to first weapon
            if (currentWeapon >= weapons.Length)
                currentWeapon = 0;

            SelectWeapon(currentWeapon);
        }
        else if (scroll < 0f)
        {
            currentWeapon--;

            // Loop back to last weapon
            if (currentWeapon < 0)
                currentWeapon = weapons.Length - 1;

            SelectWeapon(currentWeapon);
        }
    }

    void SelectWeapon(int index)
    {
        currentWeapon = index;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeapon);
        }
    }
}