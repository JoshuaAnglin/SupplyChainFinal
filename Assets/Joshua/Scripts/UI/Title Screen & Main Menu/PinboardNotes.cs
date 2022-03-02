using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PinboardNotes : MonoBehaviour
{
    [SerializeField] Texture tt2DOption;
    [SerializeField] GameObject moveToLocation;
    [SerializeField] UnityEvent action;

    bool isOverObject = false;
    string notOverObjectColour = "#DECD98";
    string OverObjectColour = "#D2BA70";

    MeshRenderer meshNote;

    void Start()
    {
        meshNote = transform.GetChild(0).GetComponent<MeshRenderer>();
        if (tt2DOption != null) meshNote.material.mainTexture = tt2DOption;
    }

    void Update()
    { 
        if (isOverObject) meshNote.material.color = MainMenu.col.HexColour(OverObjectColour);
        else if (!isOverObject) meshNote.material.color = MainMenu.col.HexColour(notOverObjectColour);
    }

    // Highlight object when mouse is over
    void OnMouseOver()
    {
        if (MainMenu.inOptions && !isOverObject)
        { 
            isOverObject = true;
        }
    }

    // Highlight object when mouse has exited
    void OnMouseExit()
    {
        if (MainMenu.inOptions && isOverObject)
        {
            isOverObject = false;
        }
    }

    void OnMouseDown()
    {
        if (MainMenu.inOptions)
        {
            action.Invoke();
        }
    }
}
