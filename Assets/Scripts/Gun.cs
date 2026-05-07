using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Stats")]
    public float damage = 20f;
    public float range = 100f;
    public float fireRate = 5f;
    public bool isAuto = false;

    [Header("References")]
    public Transform firePoint;
    public ParticleSystem muzzleFlash;
    public AudioSource shootSound;
    public Camera cam;
    public CameraShake camShake;

    private float nextFireTime = 0f;

    void Update()
    {
        HandleShooting();
    }

    void HandleShooting()
    {
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
        StartCoroutine(camShake.Shake(0.1f, 0.15f));
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Convert 3D ray to 2D direction
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
}
