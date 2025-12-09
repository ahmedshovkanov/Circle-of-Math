using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotationFix : MonoBehaviour
{
    private Quaternion _startRot;
    private void Awake()
    {
        _startRot = this.transform.rotation;
    }

    private void FixedUpdate()
    {
        this.transform.rotation = _startRot;
        //this.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
