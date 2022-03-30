using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCG.Stats
{
    public class Item : MonoBehaviour, IInteractWith
    {
        public int id;
        public float launchSpeed;

        public bool Effect(int idRef)
        {
            return id == idRef;
        }
    }
}