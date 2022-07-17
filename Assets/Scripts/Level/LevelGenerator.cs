using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
public class LevelGenerator : MonoBehaviour
{
    public List<FactoryConfiguration> configurations;
    [SerializeField] private BlockFactory cubeFactory;
    [SerializeField] private BlockFactory cylinderFactory;
    [SerializeField] private BlockFactory triangleFactory;
    [SerializeField] private BlockFactory domeFactory;
    
    public void GenerateLevel(int diceRoll)
    {
        FactoryConfiguration config = configurations[diceRoll];
        cubeFactory.SetTotal(config.cubes) ;
        cylinderFactory.SetTotal(config.cylinders);
        triangleFactory.SetTotal(config.triangles);
        domeFactory.SetTotal(config.domes);
    }
}