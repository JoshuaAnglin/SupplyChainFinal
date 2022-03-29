using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Player Cameras")]
    [SerializeField] Camera playerCam;
    [Space]
    [Header("Cutscene Cameras")]
    [SerializeField] List<Camera> cutsceneCameras = new List<Camera>();

    enum GameplayState{
        Cutscene,
        Gameplay
    }

    GameplayState gameState;

    [SerializeField] Transform playerCameraPosition;

    Transform currentCam;

    void Awake()
    {
        //currentCam = transform.GetChild(0);
        gameState = GameplayState.Gameplay; // CHANGE LATER
    }

    void Update()
    {
        switch (gameState)
        {
            case GameplayState.Cutscene:
                break;

            case GameplayState.Gameplay:
                //currentCam.position = playerCameraPosition.position;
                break;
        }
    }
}
