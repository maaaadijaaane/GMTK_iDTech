using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : MonoBehaviour
{
    public GameObject block; // Need to store prefab of block you want to generate here
    private GameObject newBlock;
    public static List<GameObject> blocks = new List<GameObject>();
    private GameObject droppedBlocks;
    private GameObject theseBlocks;

    void Start()
    {
        droppedBlocks = GameObject.Find("DroppedBlocks");
        theseBlocks = new GameObject(block.name + "Objects");
        theseBlocks.transform.SetParent(droppedBlocks.gameObject.transform);
    }
    public void AddBlock()
    {
        newBlock = Instantiate(block, transform.position, transform.rotation);
        newBlock.transform.SetParent(theseBlocks.gameObject.transform);
        blocks.Add(newBlock);
    }
}
