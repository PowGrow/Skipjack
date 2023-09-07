using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera VirtualCamera;

    public void Follow(GameObject following)
    {
        VirtualCamera.Follow = following.transform;
        VirtualCamera.LookAt = following.transform;
        VirtualCamera.gameObject.SetActive(true);
    }
}