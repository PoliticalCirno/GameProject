using UnityEngine;

[ExecuteInEditMode]
public class MobileUIValidator : MonoBehaviour
{
    [SerializeField] private GameObject mobileUiPanel;
    private void OnValidate()
    {
#if UNITY_ANDROID
        mobileUiPanel.SetActive(true);
#else
        mobileUiPanel.SetActive(false);
#endif
    }
}
