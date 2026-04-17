using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;
    [SerializeField] private ColorAdjustments colorAdjustmentsOverride;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color poisonedColor = Color.green;
    [SerializeField] private float timer = 0f;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2.5f)
        {
            if (globalVolume.profile.TryGet(out colorAdjustmentsOverride))
            {
                colorAdjustmentsOverride.colorFilter.Override(poisonedColor);
            }
        }
        if (timer > 2.75f)
        { 
            if (globalVolume.profile.TryGet(out colorAdjustmentsOverride))
            {
                colorAdjustmentsOverride.colorFilter.Override(normalColor);
            }
            timer = 0f;
        }
    }
}
