using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ExplosionCamera;

    private static CameraManager instanceCamera;

    private const float YDistanceFromExplosion = 2900f;

    private void Awake()
    {
        if (instanceCamera != null)
        {
            Destroy(gameObject);
            return;
        }
        instanceCamera = this;
        DontDestroyOnLoad(gameObject);
    }

    public static CameraManager Instance
    {
        get { return instanceCamera; }
    }

    public void SwitchToExplosionCamera(Vector3 actualPos)
    {
        Instantiate<GameObject>(ExplosionCamera, actualPos + new Vector3(0, YDistanceFromExplosion, 0),ExplosionCamera.transform.rotation);
    }
}
