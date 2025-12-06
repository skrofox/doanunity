using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private float imageWidthOffset = 10;

    private float imageFullWidth;
    private float imageHaftWidth;

    public void CaculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHaftWidth = imageFullWidth / 2;
    }

    public void Move(float distanceToMove)
    {
        //same logic but different
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier); //new Vector3(distanceToMove * parallaxMultiplier, 0);
    }

    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        float imageRightEdge = (background.position.x + imageHaftWidth) - imageWidthOffset;
        float imageLeftEdge = (background.position.x - imageHaftWidth) + imageWidthOffset;

        if (imageRightEdge < cameraLeftEdge)
        {
            background.position += Vector3.right * imageFullWidth;
        }
        else if (imageLeftEdge > cameraRightEdge)
        {
            background.position += Vector3.right * -imageFullWidth;
        }

    }
}
