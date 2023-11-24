using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaypointsFree;

public class EnemiesPathManager : MonoBehaviour
{
    public delegate WaypointsGroup GetPathEvent(EnemiesTypes enemiesTypes);
    public static GetPathEvent GetPathHandle;

    [SerializeField] private List<WaypointsGroup> enemiesBase1Paths;
    [SerializeField] private List<WaypointsGroup> enemiesBossPaths;
    [SerializeField] private List<WaypointsGroup> enemiesRedPaths;
    [SerializeField] private List<WaypointsGroup> enemiesYellowPaths;
    [SerializeField] private List<WaypointsGroup> enemiesBluePaths;

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
            case EnemiesTypes.Boss:
                {
                    return enemiesBossPaths[Random.Range(0, enemiesBossPaths.Count)];
                }
            case EnemiesTypes.Red:
                {
                    return enemiesRedPaths[Random.Range(0, enemiesRedPaths.Count)];
                }
            case EnemiesTypes.Yellow:
                {
                    return enemiesYellowPaths[Random.Range(0, enemiesYellowPaths.Count)];
                }
            case EnemiesTypes.Blue:
                {
                    return enemiesBluePaths[Random.Range(0, enemiesBluePaths.Count)];
                }
        }
        return null;
    }
}

public enum EnemiesTypes
{
    Base1, Boss, Red, Yellow, Blue
}

public enum ShootTypes
{
    Shoot1Enem, Shoot2Enem, Shoot3Enem, Shoot1
}