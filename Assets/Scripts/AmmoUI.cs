using UnityEngine;
using TMPro;
public class AmmoUI : MonoBehaviour
{
    public TMP_Text ammoText;

    void Update()
    {
        if (WeaponManager.Instance == null)
        {
            Debug.Log("WeaponManager is NULL");
            return;
        }

        if (WeaponManager.Instance.currentGun == null)
        {
            Debug.Log("CurrentGun is NULL");
            return;
        }

        if (ammoText == null)
        {
            Debug.Log("ammoText is NULL");
            return;
        }
        

        Gun gun = WeaponManager.Instance.currentGun;

        if (gun.IsReloading())
        {
            ammoText.text = "Reloading";
            ammoText.color = Color.red;
        }
        else if (gun.currentAmmo <= 10)
        {
            ammoText.text = "Press R to reload " + gun.currentAmmo + " / " + gun.reserveAmmo;
            ammoText.color = Color.red;
        }
        else
        {
            ammoText.text = gun.currentAmmo + " / " + gun.reserveAmmo;
            ammoText.color = Color.white;
        }
    }
}