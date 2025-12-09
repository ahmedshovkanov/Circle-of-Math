using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenBoundsController : MonoBehaviour
{
    public static ScreenBoundsController Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Camera _cam;
    
    public float[] BoundsCalculationsLRTB()
    {
        float[] bounds = new float[4];
        bounds[0] = _cam.transform.position.x - _cam.orthographicSize * Screen.width / Screen.height;
        bounds[1] = _cam.transform.position.x + _cam.orthographicSize * Screen.width / Screen.height;

        bounds[2] = _cam.transform.position.y + _cam.orthographicSize;
        bounds[3] = _cam.transform.position.y - _cam.orthographicSize;

        return bounds;
    }
}
