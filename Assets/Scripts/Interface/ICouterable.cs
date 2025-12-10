using UnityEngine;

public interface ICouterable
{
    public bool CanBeCounter { get; }
    public void HandleCouter();
}
