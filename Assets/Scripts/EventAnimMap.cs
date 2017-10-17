using UnityEngine;

[CreateAssetMenu(fileName = "EventAnimMap.asset", menuName = "Maps")]

public class EventAnimMap : ScriptableObject
{
    public enum EventType
    {
        OnPickUp,
        OnDetachFromHand
    };

    [System.Serializable]
    public class EventAnimEntry
    {
        public EventType Event;
        public AnimationClip Animation;
    }

    public EventAnimEntry[] EventAnimations;
}