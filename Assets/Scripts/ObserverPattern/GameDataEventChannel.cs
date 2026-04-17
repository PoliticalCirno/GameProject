using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GameData Event")]
public class GameDataEventChannel : ScriptableObject
{
    public UnityAction<GameData> OnEventRaised;

    public void RaiseEvent(GameData value)
    {
        if (OnEventRaised == null) return;
        OnEventRaised.Invoke(value);
    }
}
