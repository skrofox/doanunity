using UnityEngine;

public class Object_CheckPoint : MonoBehaviour, ISaveable
{
    private Object_CheckPoint[] allCheckPoints;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        allCheckPoints = FindObjectsByType<Object_CheckPoint>(FindObjectsSortMode.None);
    }


    public void ActiveCheckPoint(bool active)
    {
        anim.SetBool("isActive", active);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var point in allCheckPoints)
        {
            point.ActiveCheckPoint(false);
        }

        SaveManager.instance.GetGameData().savedCheckPoint = transform.position;
        ActiveCheckPoint(true);
    }

    public void LoadData(GameData data)
    {
        bool active = data.savedCheckPoint == transform.position;
        ActiveCheckPoint(active);

        if (active)
            Player.instance.TeleportPlayer(transform.position);
    }

    public void SaveData(ref GameData data)
    {
    }
}
