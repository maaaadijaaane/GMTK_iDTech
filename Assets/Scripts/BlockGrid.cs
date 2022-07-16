using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrid : MonoBehaviour
{
    [SerializeField] private float gridOverlayZCoord;
    [SerializeField] private float gridSize = .5f;
    [SerializeField] private GameObject gridLine;
    [SerializeField] private float gridWidth;
    [SerializeField] private float gridHeight;
    [SerializeField] private float allowedOverlap;


    private List<GameObject> gridLines;
    private List<Collider> gridObjects;
    private Collider activeObj;

    public static BlockGrid currentGrid;

    public bool[][] gridData;

    private void Awake()
    {
        if (currentGrid != null)
        {
            Destroy(currentGrid);
        }

        currentGrid = this;
        gridLines = new List<GameObject>();
        gridObjects = new List<Collider>();
    }

    private void OnEnable()
    {
        DrawGrid();
    }

    private void OnDisable()
    {
        gridLines.ForEach(line => Destroy(line));
        gridLines.Clear();
    }

    private void Update()
    {

    }

    public void SetActiveObject(GameObject obj)
    {
        activeObj = obj.GetComponent<Collider>(); 
    }

    public Vector3 SnapToGrid(Vector3 inCoords)
    {
        //Vector3 inCoords = obj.transform.position;
        float x = Mathf.Round((inCoords.x) / gridSize) + gridSize / 2;
        float y = Mathf.Round((inCoords.y) / gridSize) + gridSize / 2;

        return new Vector3(x, y, inCoords.z);
    }

    /// <summary>
    /// Checks if the current selected grid object can be placed
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool IsValidLocation(Collider col)
    {
        foreach (Collider gridObj in gridObjects)
        {
            float dist;
            if (!Physics.ComputePenetration(col, col.transform.position, col.transform.rotation, gridObj, gridObj.transform.position, gridObj.transform.rotation, out _, out dist) || dist <= allowedOverlap)
            {
                return true;
            }
        }
        return false;
    }

    public void AddObjectToGrid(GameObject obj)
    {
        gridObjects.Add(obj.GetComponent<Collider>());
    }

    public void DrawGrid()
    {
        for (float i = -gridWidth / 2; i < gridWidth / 2; i+= gridSize)
        {
            GameObject newLine = Instantiate(gridLine, transform);
            newLine.transform.position = new Vector3(i, 0, gridOverlayZCoord);
            newLine.transform.localScale = new Vector3(1, gridHeight, 1);
            gridLines.Add(newLine);
        }

        for (float i = -gridHeight / 2; i < gridHeight / 2; i += gridSize)
        {
            GameObject newLine = Instantiate(gridLine, transform);
            newLine.transform.Rotate(new Vector3(0, 0, 90));
            newLine.transform.position = new Vector3(0, i, gridOverlayZCoord);
            newLine.transform.localScale = new Vector3(1, gridWidth, 1);
            gridLines.Add(newLine);
        }
    }
}
