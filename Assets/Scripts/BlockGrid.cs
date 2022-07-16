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
    [SerializeField] private Transform linesParent;
    
    private List<GameObject> gridLines;

    public static BlockGrid currentGrid;

    private void Awake()
    {
        if (currentGrid != null)
        {
            Destroy(currentGrid);
        }

        currentGrid = this;
        gridLines = new List<GameObject>();
        DrawGrid();
    }

    public void ShowGrid(bool showGrid)
    {
        linesParent.gameObject.SetActive(showGrid);
    }

    public Vector3 SnapToGrid(Vector3 inCoords)
    {
        //Vector3 inCoords = obj.transform.position;
        float x = Mathf.Clamp(Mathf.Round(inCoords.x / gridSize) * gridSize, -gridWidth / 2, gridWidth / 2);
        float y = Mathf.Clamp(Mathf.Round(inCoords.y / gridSize) * gridSize, -gridHeight / 2, gridHeight / 2);

        return new Vector3(x, y, inCoords.z);
    }

    public void DrawGrid()
    {
        for (float i = -gridWidth / 2; i < gridWidth / 2; i+= gridSize)
        {
            GameObject newLine = Instantiate(gridLine, linesParent);
            newLine.transform.position = new Vector3(i, 0, gridOverlayZCoord);
            newLine.transform.localScale = new Vector3(1, gridHeight, 1);
            gridLines.Add(newLine);
        }

        for (float i = -gridHeight / 2; i < gridHeight / 2; i += gridSize)
        {
            GameObject newLine = Instantiate(gridLine, linesParent);
            newLine.transform.Rotate(new Vector3(0, 0, 90));
            newLine.transform.position = new Vector3(0, i, gridOverlayZCoord);
            newLine.transform.localScale = new Vector3(1, gridWidth, 1);
            gridLines.Add(newLine);
        }
    }
}
