using UnityEngine;

public class Object_Waypoint : MonoBehaviour
{
    [SerializeField] private string transferToScene;
    [Space]
    public RespawnType waypointType;
    [SerializeField] private RespawnType connectedWaypoint;
    [SerializeField] private bool canBeTriggerd = true;

    private void OnValidate()
    {
        gameObject.name = "Object Waypoint - " + waypointType.ToString() + " - " + transferToScene;

        if (waypointType == RespawnType.Enter)
            connectedWaypoint = RespawnType.Exit;

        if (waypointType == RespawnType.Exit)
            connectedWaypoint = RespawnType.Enter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeTriggerd == false)
            return;

        SaveManager.instance.SaveGame();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canBeTriggerd = true;

    }
}
