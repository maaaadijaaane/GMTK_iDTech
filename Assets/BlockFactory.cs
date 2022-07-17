using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockFactory : MonoBehaviour
{
    public int totalBlocks = 0;
    public GameObject block; // Need to store prefab of block you want to generate here
    private GameObject newBlock;
    public static List<GameObject> blocks = new List<GameObject>();
    private GameObject droppedBlocks;
    private GameObject theseBlocks;
    [SerializeField] private TextMeshProUGUI amountLabel;

    void Start()
    {
        droppedBlocks = GameObject.Find("DroppedBlocks");
        theseBlocks = new GameObject(block.name + "Objects");
        theseBlocks.transform.SetParent(droppedBlocks.gameObject.transform);
    }
    public void AddBlock()
    {
        newBlock = Instantiate(block, transform.position, Quaternion.identity);
        newBlock.transform.SetParent(theseBlocks.gameObject.transform);
        Rigidbody rb = newBlock.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        blocks.Add(newBlock);
        SetTotal(totalBlocks - 1);
    }

    public void SetTotal(int amount)
    {
        totalBlocks = amount;

        gameObject.SetActive(totalBlocks != 0);

        amountLabel.text = "x" + totalBlocks;
    }
}