using UnityEngine;

public class UnityTimeService : ITimeService
{
    public float DeltaTime => Time.deltaTime;
}
