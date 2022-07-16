using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : MonoBehaviour
{
    public GameObject block; // Need to store prefab of block you want to generate here
    private GameObject newBlock;
    public static List<GameObject> blocks = new List<GameObject>();

    public void AddBlock()
    {
        newBlock = new GameObject();
        newBlock = Instantiate(block, transform.position, transform.rotation);
        blocks.Add(newBlock);
    }
}
