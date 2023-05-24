using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Cameraswitch : MonoBehaviour
{
    public ARCameraManager arCameraManager;

    public void CameraSwitch()
    {
        if (arCameraManager.currentFacingDirection == CameraFacingDirection.User)
        {
            arCameraManager.requestedFacingDirection = CameraFacingDirection.World;
        }
        else
        {
            arCameraManager.requestedFacingDirection = CameraFacingDirection.User;
        }
    }
}
