using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Grid grid;
    public List<Cell> cells;
    private List<Vector2> vector2s = new List<Vector2> { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
    public int countDouble = 0;
    public GameObject[] pieces;
    public RectTransform canvasRect;
    public Text point;
    private int score=0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        GeneratePiece();
    }
    private void Update()
    {
        GamePlay();
        if (countDouble == 0)
        {
            GeneratePiece();
        }
    }
    private void GamePlay()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            CheckCell(cells[i]);
        }
        point.text = score.ToString();
    }
    private void CheckCell(Cell cell)
    {
        if (cell.type == TypePiece.None) { return; }
        for (int i = 0; i < vector2s.Count; i++)
        {
            if ((cell.x + vector2s[i].x < 0 || cell.x + vector2s[i].x >= grid.size.x) || (cell.y + vector2s[i].y < 0 || cell.y + vector2s[i].y >= grid.size.y))
            {
                break;
            }
            int x = (int)vector2s[i].x;
            int y = (int)vector2s[i].y;
            if (cell.type == grid.cells[cell.x + x, cell.y + y].GetComponent<Cell>().type)
            {
                Debug.Log("ok");
                Destroy(grid.cells[cell.x, cell.y].transform.GetChild(0).GetComponent<Image>().gameObject);
                Destroy(grid.cells[cell.x + x, cell.y + y].transform.GetChild(0).GetComponent<Image>().gameObject);
                cell.type = TypePiece.None;
                grid.cells[cell.x + x, cell.y + y].GetComponent<Cell>().type = TypePiece.None;
                countDouble--;
                score += 5;
                return;
            }
        }
    }

    public void GeneratePiece()
    {
        countDouble = (int)grid.size.x * (int)grid.size.y / 2;
        for (int i = 0; i < 18; i++)
        {
            int tmp = UnityEngine.Random.Range(0, pieces.Length);
            Vector2 randomPosition = GenerateRandomPosition();
            SpawnImageAtPosition(randomPosition, pieces[tmp]);
            randomPosition = GenerateRandomPosition();
            SpawnImageAtPosition(randomPosition, pieces[tmp]);

        }

    }

    Vector3 GenerateRandomPosition()
    {
        float x = UnityEngine.Random.Range(40, canvasRect.rect.width / 2);
        float y = UnityEngine.Random.Range(0, canvasRect.rect.height / 2) +  canvasRect.rect.height / 4;
        return new Vector3(x - canvasRect.rect.width / 2, y - canvasRect.rect.height / 2, -10);
    }

    void SpawnImageAtPosition(Vector3 position, GameObject piecePrefab)
    {
        GameObject newImage = Instantiate(piecePrefab, canvasRect);
        RectTransform rectTransform = newImage.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = position;
        newImage.transform.parent = transform;

    }

}

