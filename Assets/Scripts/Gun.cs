using UnityEngine;
using TMPro;
public class Gun : MonoBehaviour
{
    [Header("Stats")]
    public float damage = 20f;
    public float range = 100f;
    public float fireRate = 5f;
    public bool isAuto = false;

    [Header("Ammo")]
    public int magazineSize = 30;
    public int currentAmmo;
    public int reserveAmmo = 90;
    public float reloadTime = 2f;

    private bool isReloading = false;

    [Header("References")]
    public Transform firePoint;
    public ParticleSystem muzzleFlash;
    public AudioSource shootSound;
    public Camera cam;
    public CameraShake camShake;

    private float nextFireTime = 0f;

    [Header("UI")]
    public TMP_Text ammoText;

    void Start()
    {
        currentAmmo = magazineSize;
        UpdateAmmoUI();
    }

    void Update()
    {
        UpdateAmmoUI();
        if (isReloading)
            return;

        // Reload key
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            return;
        }

        HandleShooting();
    }

    void HandleShooting()
    {
        if (Dialog.Instance.DialogActive)
            return;

        // No ammo
        if (currentAmmo <= 0)
        {
            if (reserveAmmo > 0)
            {
                Reload();
            }
            else
            {
                MessageSystem.Instance.ShowMessage("Out of Ammo!");
            }

            return;
        }

        bool canShoot = Time.time >= nextFireTime;

        if (isAuto)
        {
            if (Input.GetMouseButton(0) && canShoot)
            {
                nextFireTime = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                nextFireTime = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI();

        StartCoroutine(camShake.Shake(0.1f, 0.15f));

        if (muzzleFlash != null)
            muzzleFlash.Play();

        if (shootSound != null)
            shootSound.Play();

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Vector2 direction = ray.direction;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, range);

        if (hit.collider != null)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    void Reload()
    {
        if (isReloading)
            return;

        if (reserveAmmo <= 0)
            return;

        if (currentAmmo >= magazineSize)
            return;

        isReloading = true;

        Invoke(nameof(FinishReload), reloadTime);
    }

    void FinishReload()
    {
        int ammoNeeded = magazineSize - currentAmmo;

        int ammoToLoad = Mathf.Min(ammoNeeded, reserveAmmo);

        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;

        isReloading = false;
        UpdateAmmoUI();
    }

    // Called by ammo boxes
    public void AddAmmo(int amount)
    {
        reserveAmmo += amount;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoText == null)
            return;

        ammoText.text = currentAmmo + " / " + reserveAmmo;

        // Low ammo color
        if (currentAmmo <= 5)
            ammoText.color = Color.red;
        else
            ammoText.color = Color.white;
    }
}
