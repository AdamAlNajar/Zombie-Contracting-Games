using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Automatically sets up Post Processing on the main camera at runtime.
/// Attach this to your Main Camera in scenes where you want post-processing effects.
/// 
/// NOTE: You must also add a PostProcessLayer component to your camera
/// (Component -> Rendering -> Post-process Layer) and assign its "Resources" field
/// by clicking the "Init" button in the Inspector, OR simply drag this script's
/// "PostProcessResources" field below.
/// </summary>
[RequireComponent(typeof(Camera))]
public class PostProcessingSetup : MonoBehaviour
{
    [Header("Post-Processing Profile")]
    public PostProcessProfile profile;

    [Header("Post-Process Layer Resources")]
    public PostProcessResources resources; // Assign in Inspector via the "Init" button

    [Header("Effects Toggle")]
    public bool enableBloom = true;
    public bool enableColorGrading = true;
    public bool enableVignette = true;
    public bool enableAmbientOcclusion = false;
    public bool enableDepthOfField = false;

    private void Awake()
    {
        SetupPostProcessing();
    }

    private void SetupPostProcessing()
    {
        Camera cam = GetComponent<Camera>();

        // Ensure the "PostProcessing" layer exists
        EnsurePostProcessingLayer();
        int ppLayer = LayerMask.NameToLayer("PostProcessing");

        // --- PostProcessLayer ---
        // NOTE: PostProcessLayer requires a PostProcessResources asset to function.
        // If we have resources assigned, set up the layer.
        PostProcessLayer layer = GetComponent<PostProcessLayer>();
        if (layer == null && resources != null)
        {
            layer = gameObject.AddComponent<PostProcessLayer>();
            layer.volumeLayer = 1 << ppLayer;
            layer.volumeTrigger = cam.transform;
            layer.antialiasingMode = PostProcessLayer.Antialiasing.FastApproximateAntialiasing;
            layer.Init(resources);
            Debug.Log("PostProcessingSetup: PostProcessLayer initialized from assigned resources.");
        }
        else if (layer != null && resources != null)
        {
            // Layer exists but resources might not be set — re-init if needed
            layer.Init(resources);
        }
        else if (layer != null)
        {
            // Layer exists, just reconfigure
            layer.volumeLayer = 1 << ppLayer;
            layer.volumeTrigger = cam.transform;
        }
        else
        {
            Debug.LogWarning(
                "PostProcessingSetup: No PostProcessLayer on camera AND no resources assigned.\n" +
                "Please add a PostProcessLayer component to this camera (Component -> Rendering -> Post-process Layer)\n" +
                "and assign its 'Resources' field (click 'Init' in the Inspector), OR assign a PostProcessResources\n" +
                "to the 'Resources' field on this PostProcessingSetup component.\n\n" +
                "The post-processing Volume and Profile have been created — they will activate once the Layer is set up."
            );
        }

        // --- PostProcessVolume ---
        PostProcessVolume volume = FindFirstObjectByType<PostProcessVolume>();
        if (volume == null)
        {
            GameObject volumeObj = new GameObject("PostProcessVolume");
            volume = volumeObj.AddComponent<PostProcessVolume>();
            volume.isGlobal = true;
            volume.priority = 0f;
            volumeObj.layer = ppLayer >= 0 ? ppLayer : 0;
        }

        // Build profile at runtime if none assigned
        if (profile == null)
        {
            profile = ScriptableObject.CreateInstance<PostProcessProfile>();

            // --- Bloom ---
            if (enableBloom)
            {
                Bloom bloom = profile.AddSettings<Bloom>();
                bloom.intensity.value = 0.75f;
                bloom.threshold.value = 1.1f;
                bloom.softKnee.value = 0.5f;
                bloom.diffusion.value = 5f;
                bloom.enabled.value = true;
            }

            // --- Color Grading ---
            if (enableColorGrading)
            {
                ColorGrading grading = profile.AddSettings<ColorGrading>();
                grading.postExposure.value = 0.1f;
                grading.contrast.value = 10f;
                grading.saturation.value = 5f;
                grading.temperature.value = -5f;
                grading.tint.value = 0f;
                grading.enabled.value = true;
            }

            // --- Vignette ---
            if (enableVignette)
            {
                Vignette vignette = profile.AddSettings<Vignette>();
                vignette.intensity.value = 0.35f;
                vignette.smoothness.value = 0.45f;
                vignette.roundness.value = 0.1f;
                vignette.color.value = Color.black;
                vignette.enabled.value = true;
            }

            // --- Ambient Occlusion ---
            if (enableAmbientOcclusion)
            {
                AmbientOcclusion ao = profile.AddSettings<AmbientOcclusion>();
                ao.intensity.value = 0.3f;
                ao.radius.value = 0.5f;
                ao.quality.value = AmbientOcclusionQuality.Medium;
                ao.enabled.value = true;
            }

            // --- Depth of Field ---
            if (enableDepthOfField)
            {
                DepthOfField dof = profile.AddSettings<DepthOfField>();
                dof.focusDistance.value = 10f;
                dof.aperture.value = 5.6f;
                dof.focalLength.value = 50f;
                dof.enabled.value = true;
            }
        }

        volume.sharedProfile = profile;
    }

    /// <summary>
    /// Ensures the "PostProcessing" layer exists.
    /// </summary>
    private static void EnsurePostProcessingLayer()
    {
        int existingLayer = LayerMask.NameToLayer("PostProcessing");
        if (existingLayer != -1)
            return;

#if UNITY_EDITOR
        TryAddLayerInEditor();
#else
        Debug.LogWarning(
            "PostProcessingSetup: 'PostProcessing' layer not found! " +
            "Please add a layer named 'PostProcessing' in Edit > Project Settings > Tags and Layers, " +
            "then re-enter this scene for post-processing to work."
        );
#endif
    }

#if UNITY_EDITOR
    private static void TryAddLayerInEditor()
    {
        UnityEngine.Object[] assets = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(
            "ProjectSettings/TagManager.asset"
        );
        if (assets == null || assets.Length == 0)
        {
            Debug.LogWarning("PostProcessingSetup: Could not load TagManager.asset. " +
                "Please manually add 'PostProcessing' layer in Edit > Project Settings > Tags and Layers.");
            return;
        }

        UnityEditor.SerializedObject tagManager = new UnityEditor.SerializedObject(assets[0]);
        UnityEditor.SerializedProperty layers = tagManager.FindProperty("layers");

        for (int i = 8; i < layers.arraySize; i++)
        {
            UnityEditor.SerializedProperty layerProp = layers.GetArrayElementAtIndex(i);
            if (string.IsNullOrEmpty(layerProp.stringValue))
            {
                layerProp.stringValue = "PostProcessing";
                tagManager.ApplyModifiedProperties();
                Debug.Log("PostProcessingSetup: Added 'PostProcessing' layer at index " + i);
                return;
            }
        }

        Debug.LogWarning("PostProcessingSetup: No free layer slots available! " +
            "Please manually assign a layer named 'PostProcessing' in Edit > Project Settings > Tags and Layers.");
    }
#endif
}
