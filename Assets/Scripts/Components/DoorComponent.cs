using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct DoorComponent
    {
        public int ID;
        public Quaternion OpenedRotation;
    }
}