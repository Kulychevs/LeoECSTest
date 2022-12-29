using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct ButtonComponent
    {
        public int DoorID;
        public Vector3 OriginalPosition;
        public Vector3 PressedPosition;
    }
}