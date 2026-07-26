using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Attach to the Player. Provides visual damage feedback:
/// - Red vignette/flash overlay on the screen
/// - Brief screen shake on hit
/// </summary>
public class HitEffect : MonoBehaviour
{
    [Header("Damage Flash")]
    public Image damageOverlay;          // Full-screen red Image (set alpha to 0 in inspector)
    public float flashDuration = 0.15f;
    public float maxFlashAlpha = 0.5f;

    [Header("Damage Shake")]
    public CameraShake cameraShake;
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.2f;

    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        if (player == null)
            Debug.LogWarning("HitEffect: No Player component found on this GameObject.");

        // Auto-create damage overlay Canvas if none assigned
        if (damageOverlay == null)
            damageOverlay = CreateDamageOverlay();
        else
        {
            Color c = damageOverlay.color;
            c.a = 0f;
            damageOverlay.color = c;
        }

        // Find camera shake if not assigned
        if (cameraShake == null)
            cameraShake = FindFirstObjectByType<CameraShake>();
    }

    /// <summary>
    /// Creates a full-screen Canvas with a red overlay Image for damage feedback.
    /// </summary>
    private Image CreateDamageOverlay()
    {
        // Find existing Canvas or create one
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("DamageOverlayCanvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 999; // Always on top
        }

        // Create the overlay image
        GameObject overlayObj = new GameObject("DamageOverlay");
        overlayObj.transform.SetParent(canvas.transform, false);

        Image img = overlayObj.AddComponent<Image>();
        img.color = new Color(0.8f, 0f, 0f, 0f); // Transparent red
        img.raycastTarget = false; // Don't block clicks

        // Stretch to fill screen
        RectTransform rt = img.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        return img;
    }

    /// <summary>
    /// Call this from Player.TakeDamage().
    /// </summary>
    public void OnTakeDamage()
    {
        if (damageOverlay != null)
            StartCoroutine(FlashOverlay());

        if (cameraShake != null)
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
    }

    /// <summary>
    /// Call this when the player dies.
    /// </summary>
    public void OnDeath()
    {
        if (damageOverlay != null)
            StartCoroutine(FlashOverlay(1f, 2f));
    }

    private IEnumerator FlashOverlay(float alpha = -1f, float duration = -1f)
    {
        float targetAlpha = alpha >= 0f ? alpha : maxFlashAlpha;
        float dur = duration >= 0f ? duration : flashDuration;

        if (damageOverlay == null)
            yield break;

        Color c = damageOverlay.color;
        c.a = targetAlpha;
        damageOverlay.color = c;

        yield return new WaitForSeconds(dur);

        // Fade out
        float elapsed = 0f;
        while (elapsed < 0.2f)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(targetAlpha, 0f, elapsed / 0.2f);
            damageOverlay.color = c;
            yield return null;
        }

        c.a = 0f;
        damageOverlay.color = c;
    }
}
