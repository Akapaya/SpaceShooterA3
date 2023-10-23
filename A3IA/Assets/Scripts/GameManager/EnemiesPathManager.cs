using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaypointsFree;

public class EnemiesPathManager : MonoBehaviour
{
    public delegate WaypointsGroup GetPathEvent(EnemiesTypes enemiesTypes);
    public static GetPathEvent GetPathHandle;

    [SerializeField] private List<WaypointsGroup> enemiesBase1Paths;

    private void OnEnable()
    {
        GetPathHandle += GetPath;
    }

    private void OnDisable()
    {
        GetPathHandle -= GetPath;
    }

    private WaypointsGroup GetPath(EnemiesTypes enemiesTypes)
    {
        switch(enemiesTypes)
        {
            case EnemiesTypes.Base1:
                {
                    return enemiesBase1Paths[Random.Range(0, enemiesBase1Paths.Count)];
                }
        }
        return null;
    }
}

public enum EnemiesTypes
{
    Base1,
}