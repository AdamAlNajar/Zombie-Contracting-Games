using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    [Header("Coins UI")]
    [SerializeField] TMP_Text coinsText;
    [SerializeField] private Sprite coinIcon;  // Drag Coin.png here

    [Header("Health")]
    [SerializeField] float currentHealth;
    [SerializeField] private HitEffect hitEffect;
    public float startingHealth = 100f;

    private void Start()
    {
        currentHealth = startingHealth;

        if (HealthBar.Instance == null)
        {
            Debug.LogError("HealthBar missing in scene!");
            return;
        }

        HealthBar.Instance.SetMaxHealth(startingHealth);
        
        // Auto-find HitEffect on same GameObject if not assigned
        if (hitEffect == null)
            hitEffect = GetComponent<HitEffect>();
        
        // Auto-create coins text if not assigned (avoids sharing with ammo text)
        if (coinsText == null)
            coinsText = CreateCoinsText();
        
        if (coinsText != null)
        {
            coinsText.text = GameData.Instance.gameCoins.ToString();
        }
    }

    void Update()
    {
        if(coinsText != null)
        {
            coinsText.text = GameData.Instance.gameCoins.ToString();
        }
    }

    private TMP_Text CreateCoinsText()
    {
        // Find the AmmoUI to match its canvas, font, and position
        AmmoUI ammoUI = FindFirstObjectByType<AmmoUI>();
        Canvas targetCanvas = null;
        TMP_Text ammoRef = null;

        if (ammoUI != null && ammoUI.ammoText != null)
        {
            ammoRef = ammoUI.ammoText;
            targetCanvas = ammoRef.GetComponentInParent<Canvas>();
        }

        // Fallback: create own canvas
        if (targetCanvas == null)
        {
            GameObject canvasObj = new GameObject("CoinsHUDCanvas");
            targetCanvas = canvasObj.AddComponent<Canvas>();
            targetCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            targetCanvas.sortingOrder = 10;
            canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        }

        // Determine the vertical anchor and offset from the ammo text
        // so coins sit at the same height on the left side
        float coinsYAnchor = 1f;
        float coinsYOffset = -10f;
        if (ammoRef != null)
        {
            RectTransform ammoRt = ammoRef.rectTransform;
            coinsYAnchor = ammoRt.anchorMin.y;  // Match ammo's vertical anchor
            coinsYOffset = ammoRt.anchoredPosition.y;  // Match ammo's Y offset
        }

        // --- Coins text (left side, icon then number) ---
        GameObject textObj = new GameObject("CoinsText");
        textObj.transform.SetParent(targetCanvas.transform, false);

        TMP_Text tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = GameData.Instance.gameCoins.ToString();
        tmp.color = Color.white;
        tmp.alignment = TextAlignmentOptions.Left;

        // Copy font from ammo text if available
        if (ammoRef != null)
        {
            tmp.font = ammoRef.font;
            tmp.fontSize = ammoRef.fontSize;
            tmp.fontStyle = ammoRef.fontStyle;
        }
        else
        {
            tmp.fontSize = 20;
        }

        RectTransform rt = tmp.rectTransform;
        rt.anchorMin = new Vector2(0f, coinsYAnchor);
        rt.anchorMax = new Vector2(0f, coinsYAnchor);
        rt.pivot = new Vector2(0f, coinsYAnchor);
        rt.anchoredPosition = new Vector2(38f, coinsYOffset);  // Right of icon
        rt.sizeDelta = new Vector2(100, 30);

        // --- Coin icon (left of text) ---
        GameObject iconObj = new GameObject("CoinIcon");
        iconObj.transform.SetParent(targetCanvas.transform, false);

        UnityEngine.UI.Image iconImage = iconObj.AddComponent<UnityEngine.UI.Image>();
        iconImage.sprite = coinIcon;
        iconImage.raycastTarget = false;

        RectTransform iconRt = iconImage.rectTransform;
        iconRt.anchorMin = new Vector2(0f, coinsYAnchor);
        iconRt.anchorMax = new Vector2(0f, coinsYAnchor);
        iconRt.pivot = new Vector2(0f, coinsYAnchor);
        iconRt.anchoredPosition = new Vector2(10f, coinsYOffset);  // Same Y as text
        iconRt.sizeDelta = new Vector2(24, 24);

        return tmp;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HealthBar.Instance.SetHealth(currentHealth);

        // Visual feedback
        if (hitEffect != null)
            hitEffect.OnTakeDamage();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        if (hitEffect != null)
            hitEffect.OnDeath();
        
        Destroy(gameObject);
    }
}
