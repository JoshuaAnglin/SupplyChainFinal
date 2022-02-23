using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used as a way for objects (specifically their methods) to subscribe to so that events can be fired from one point, and so that data can be shared
// without having to make everything public
public class GameEventSystem : MonoBehaviour
{
    static public GameEventSystem GES;

    public event Action<int> onCameraTurn;
    public event Action onWithinAnArea;

    void Awake()
    {GES = this;}

    public void CameraTurn(int direction)
    {
        if (onCameraTurn != null)
            onCameraTurn(direction);
    }

    public void WithinAnArea()
    {
        if (onWithinAnArea != null)
        {
            onWithinAnArea();
        }
        
    }
}
