using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;

    public void Move(float distanceToMove)
    {
        //same logic but different
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier); //new Vector3(distanceToMove * parallaxMultiplier, 0);
    }
}
