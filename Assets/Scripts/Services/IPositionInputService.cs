using UnityEngine;

public interface IPositionInputService
{
    bool TryGetPosition(out Vector3 position);
}