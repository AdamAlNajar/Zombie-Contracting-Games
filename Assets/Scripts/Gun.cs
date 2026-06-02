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
    private float nextFireTime = 0f;

    [Header("References")]
    public Transform firePoint;
    public ParticleSystem muzzleFlash;
    public AudioSource shootSound;
    public Camera cam;
    public CameraShake camShake;

    private MessageSystem messageSystem;
    private Dialog dialog;

    private void OnEnable()
    {
        // when weapon becomes active via GunSwitcher
        WeaponManager.Instance.SetCurrentGun(this);

        if (currentAmmo <= 0)
            currentAmmo = magazineSize;

        messageSystem = FindFirstObjectByType<MessageSystem>();
        dialog = FindFirstObjectByType<Dialog>();
    }

    void Update()
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (isReloading)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            return;
        }

        HandleShooting();
    }

    void HandleShooting()
    {
        if (dialog != null && dialog.DialogActive)
            return;

        if (currentAmmo <= 0)
        {
            if (reserveAmmo > 0)
                Reload();
            else
                messageSystem.ShowMessage("Out of Ammo!");

            return;
        }

        bool canShoot = Time.time >= nextFireTime;

        if (isAuto)
        {
            if (Input.GetMouseButton(0) && canShoot)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;


        if (camShake != null)
            StartCoroutine(camShake.Shake(0.1f, 0.15f));

        if (muzzleFlash != null)
            muzzleFlash.Play();

        if (shootSound != null)
            shootSound.Play();

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, ray.direction, range);

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

    }

    public void AddAmmo(int amount)
    {
        reserveAmmo += amount;
    }

    public bool IsReloading()
    {
        return isReloading;
    }
}
