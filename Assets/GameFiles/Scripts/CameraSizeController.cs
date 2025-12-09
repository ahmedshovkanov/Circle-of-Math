using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
    private void Awake()
    {
        _targetSizeX = _isHorizontal ? SizeX : SizeY;
        _targetSizeY = _isHorizontal ? SizeY : SizeX;

        CameraResize();
    }

    private const float SizeX = 1920.0f;
    private const float SizeY = 1080.0f;
    private float _targetSizeX = 0f;
    private float _targetSizeY = 0f;
    private const float HalfSize = 200.0f;
    [SerializeField] private bool _isHorizontal = true;

    [SerializeField] private Camera _cam;
    
    private void CameraResize()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = _targetSizeX / _targetSizeY;

        if (screenRatio >= targetRatio)
        {
            Resize();
        }
        else
        {
            float differentSize = targetRatio / screenRatio;
            Resize(differentSize);
        }
    }

    private void Resize(float differentSize = 1.0f)
    {
        _cam.orthographicSize = _targetSizeY / HalfSize * differentSize;
    }
#if UNITY_EDITOR
    [ContextMenu("Resize")]
    private void ResizeOnEditor()
    {
        float higth = _isHorizontal ? SizeY : SizeX;
        _cam.orthographicSize = higth / HalfSize;
    }
#endif
}