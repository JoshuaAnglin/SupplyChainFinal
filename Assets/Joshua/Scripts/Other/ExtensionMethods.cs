using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ExtensionMethods
{
    static public Color HexColour(this Color hexCol, string Hex)
    {
        ColorUtility.TryParseHtmlString(Hex, out hexCol);
        return hexCol;
    }
}
