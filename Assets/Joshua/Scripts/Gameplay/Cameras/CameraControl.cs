using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    enum GameplayState{
        Cutscene,
        Gameplay
    }

    GameplayState gameState;

    [SerializeField] Transform playerCameraPosition;

    Transform playerCam;

    void Awake()
    {
        playerCam = transform.GetChild(0);
        gameState = GameplayState.Gameplay; // CHANGE LATER
    }

    void Update()
    {
        switch (gameState)
        {
            case GameplayState.Cutscene:
                break;

            case GameplayState.Gameplay:
                playerCam.position = playerCameraPosition.position;
                break;
        }
    }
}
