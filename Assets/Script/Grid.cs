using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering;

public class Grid : MonoBehaviour
{
    [SerializeField] public Vector2 size;
    [SerializeField] public GameObject[] blockPrefab;
    [SerializeField] public GameObject[,] cells;

    private void Start()
    {
        cells = new GameObject[(int)size.x, (int)size.y];
        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                int tmp = (i + j) % 2;
                GameObject cell = (GameObject)Instantiate(blockPrefab[tmp]);
                cell.GetComponent<Cell>().Init(i, j);
                cells[i, j] = cell;
                GameManager.instance.cells.Add(cell.GetComponent<Cell>());
                cell.transform.parent = transform;
            }
        }
    }



}