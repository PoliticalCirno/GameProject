using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Float Event")]
public class FloatEventChannel : ScriptableObject
{
    public UnityAction<float> OnEventRaised;

    public void RaiseEvent(float value)
    {
        if (OnEventRaised == null) return;
        OnEventRaised.Invoke(value);
    }
}
