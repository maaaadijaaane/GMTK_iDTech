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
<<<<<<< Updated upstream
    
    public void GenerateLevel(int diceRoll)
    {
        cylinderFactory.SetTotal(config.cylinders);
        triangleFactory.SetTotal(config.triangles);
        domeFactory.SetTotal(config.domes);
<<<<<<< Updated upstream
=======
    }
>>>>>>> Stashed changes
}