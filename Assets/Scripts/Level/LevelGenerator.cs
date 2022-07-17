using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<FactoryConfiguration> configurations;
    [SerializeField] private BlockFactory cubeFactory;
    [SerializeField] private BlockFactory cylinderFactory;
    [SerializeField] private BlockFactory triangleFactory;
    [SerializeField] private BlockFactory domeFactory;
    public static int currCheckpoint;
    public static List<int> checkpoints;
    public void GenerateLevel(int diceRoll)
    {
        FactoryConfiguration config = configurations[diceRoll];
        cubeFactory.SetTotal(config.cubes) ;
        cylinderFactory.SetTotal(config.cylinders);
        triangleFactory.SetTotal(config.triangles);
        domeFactory.SetTotal(config.domes);
    }

    public void RandomizeCheckpoint()
    {
        float checkPoint = UnityEngine.Random.Range(1, 6);
        currCheckpoint = (int) checkPoint;
        Debug.Log(currCheckpoint);
    }
}