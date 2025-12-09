using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCellsDiv : MonoBehaviour
{
    public float TargetRotationZ;
    //public static RotationCellsDiv Instance;

    //private void Awake()
    //{
    //    Instance = this;
    //}

    private void Start()
    {
        MainMechController.Instance.OnGameStageChange += OnGameStageChange;
    }

    private void OnGameStageChange(GameStage stage)
    {
        if (stage == GameStage.Setup)
        {
            TargetRotationZ = 0f;
            speed = 100f;
        }
        else if (stage == GameStage.Result)
        {
            TargetRotationZ = 0f;
            speed = 100f;
        }
        else
        {
            speed = 25f;
        } 
    }
    public float speed;

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, TargetRotationZ), speed * Time.fixedDeltaTime);
    }




}
