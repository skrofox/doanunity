using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float cameraHaftWidth;

    [SerializeField] private ParallaxLayer[] backgroundLayers;


    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHaftWidth = mainCamera.orthographicSize * mainCamera.aspect;
        InitializeLayers();
    }

    private void FixedUpdate()
    {
        float currentCameraPositionX = mainCamera.transform.position.x;
        float distanceToMove = currentCameraPositionX - lastCameraPositionX;
        lastCameraPositionX = currentCameraPositionX;

        float cameraRightEdge = currentCameraPositionX + cameraHaftWidth;
        float cameraLeftEdge = currentCameraPositionX - cameraHaftWidth;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }

    private void InitializeLayers()
    {
        foreach(ParallaxLayer layer in backgroundLayers)
        {
            layer.CaculateImageWidth();
        }
    }
}
