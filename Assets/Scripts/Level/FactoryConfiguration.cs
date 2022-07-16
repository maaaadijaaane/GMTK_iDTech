
using UnityEngine;


[CreateAssetMenu(fileName = "New Level Configuration", menuName = "Custom / Level Configuration")]
public class FactoryConfiguration : ScriptableObject
{
    public int cubes;
    public int triangles;
    public int cylinders;
    public int domes;
}
