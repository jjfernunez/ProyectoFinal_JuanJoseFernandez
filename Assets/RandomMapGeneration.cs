using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomMapGeneration : MonoBehaviour
{
    [Header("Map Generation")]
    public Texture2D tileSheet; // Tilesheet que contiene los sprites de los tiles
    public int mapMaxWidth = 10; // Ancho del mapa en tiles
    public int mapMaxHeight = 10; // Alto del mapa en tiles
    public int mapMinWidth;
    public int mapMinHeight;
    public Sprite[] sprites;

    [Header("Tree Generation")]
    public Sprite [] treeSprites; // Prefab del árbol
    public int numberOfTrees = 10; // Número de árboles a generar
    public Vector2 range = new Vector2(10f, 10f); // Rango de generac

    private void Awake()
    {
        GenerateRandomMap();
        TreeGeneration();
    }

    private void GenerateRandomMap()
    {
         // Carga todos los sprites del tilesheet
        
        for (int x = mapMinWidth; x < mapMaxWidth; x++)
        {
            for (int y = mapMinHeight; y < mapMaxHeight; y++)
            {
                int randomIndex = UnityEngine.Random.Range(0, sprites.Length); // Obtiene un índice aleatorio para seleccionar un sprite
                Sprite randomSprite = sprites[randomIndex]; // Selecciona un sprite aleatorio

                // Crea un GameObject para el tile y lo coloca en la posición correspondiente
                GameObject tile = new GameObject("Tile_" + x + "_" + y);
                tile.transform.position = new Vector3(x, y, 0f);
                tile.transform.parent = GameObject.Find("Map").transform;
                // Añade un SpriteRenderer al GameObject y establece el sprite
                SpriteRenderer spriteRenderer = tile.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = randomSprite;
                spriteRenderer.sortingOrder = -1;
                
            }
        }
    }

    private void TreeGeneration()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            Vector2 randomPosition = new Vector2(UnityEngine.Random.Range(-range.x, range.x), UnityEngine.Random.Range(-range.y, range.y));
            Sprite randomSprite = treeSprites[UnityEngine.Random.Range(0, treeSprites.Length)];

            GameObject tree = new GameObject("Tree_" + i);
            tree.transform.position = randomPosition;
            tree.transform.parent = GameObject.Find("Map").transform;
            SpriteRenderer spriteRenderer = tree.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = randomSprite;
            spriteRenderer.sortingOrder = 7;
        }
    }
}


