using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LevelGenerator : MonoBehaviour
{
    private LevelGenerator Instance;
    public List<FactoryConfiguration> configurations;
    [SerializeField] private BlockFactory cubeFactory;
    [SerializeField] private BlockFactory cylinderFactory;
    [SerializeField] private BlockFactory triangleFactory;
    [SerializeField] private BlockFactory domeFactory;
    [SerializeField] private BlockFactory diceFactory;
    public static int currCheckpoint;
    public static List<int> checkpoints;

    private bool usedDice;

    private List<BlockFactory> emptyFactories;
    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    /*
    void Awake()
    {
        if(Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
    */
    private void Start()
    {
        emptyFactories = new List<BlockFactory>();
    }

    public void GenerateLevel(int diceRoll)
    {
        FactoryConfiguration config = configurations[diceRoll - 1];
        cubeFactory.SetTotal(config.cubes);
        cylinderFactory.SetTotal(config.cylinders);
        triangleFactory.SetTotal(config.triangles);
        domeFactory.SetTotal(config.domes);

        diceFactory.gameObject.SetActive(false);

        if (emptyFactories == null)
            emptyFactories = new List<BlockFactory>();
        else
            emptyFactories.Clear();
    }

    public void RandomizeCheckpoint()
    {
        float checkPoint = UnityEngine.Random.Range(1, 6);
        currCheckpoint = (int)checkPoint;
        Debug.Log(currCheckpoint);
    }

    private void Update()
    {
        if (emptyFactories.Count == 4)
        {
            ShowDiceFactory();
        }
    }

    public void OnBlockCreated(BlockFactory factory, GameObject block)
    {
        if (factory.totalBlocks == 0 && factory != diceFactory)
        {
            emptyFactories.Add(factory);
        }
        else if (factory == diceFactory)
        {
            block.GetComponent<Dice>().onDiceFinishRoll.AddListener(result => GenerateLevel(result));
        }
    }

    public void ShowDiceFactory()
    {
        diceFactory.gameObject.SetActive(true);
    }
}