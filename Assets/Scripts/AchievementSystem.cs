using UnityEngine;

public class AchievementSystem : MonoBehaviour
{
    [SerializeField] private VoidEventChannel voidChannel;
    [SerializeField] private GameDataEventChannel gameDataChannel;

    private int achievementJumps = 10;
    private int currentJumps = 0;

    private void OnEnable()
    {
        voidChannel.OnEventRaised += EventCalled;
        gameDataChannel.OnEventRaised += GameDataEventCalled;
    }
    private void OnDisable()
    {
        voidChannel.OnEventRaised -= EventCalled;
        gameDataChannel.OnEventRaised -= GameDataEventCalled;
    }

    private void EventCalled()
    {
        Debug.Log("Event Called by listening to the Event Channel of Void type");
        currentJumps++;
        if (currentJumps == achievementJumps)
        {
            Debug.Log("Achievement Completed. Jumped 10 times");
        }
    }
    private void GameDataEventCalled(GameData data)
    {
        Debug.Log("Event with GameData data passed on with filename as "+data.fileName);
    }
}