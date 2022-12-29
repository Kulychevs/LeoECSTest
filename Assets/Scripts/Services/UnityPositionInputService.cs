using UnityEngine;

public class UnityPositionInputService : IPositionInputService
{
    private Camera _Camera;

    public bool TryGetPosition(out Vector3 position)
    {
        if (_Camera == null)
            _Camera = Camera.main;

        position = Vector3.zero;
        if (!Input.GetMouseButtonDown(0))
            return false;

        var ray = _Camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit))
            return false;

        position = hit.point;
        return true;
    }
}

