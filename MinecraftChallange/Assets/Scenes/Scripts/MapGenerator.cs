﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject chunkPrefab;
    private int chunkMapSize = 6;
    private List<Chunk> chunkList = new List<Chunk>();
    //public static Dictionary<Chunk, ChunkPos> chunkList = new Dictionary<Chunk, ChunkPos>();

    private void Start()
    {
        GenerateMap(chunkMapSize, chunkMapSize);
    }

    void GenerateMap(int _chunkMapSizeX, int _chunkMapSizeY)
    {
        for(int x = 0; x < _chunkMapSizeX; ++x)
        { 
            for(int z = 0; z < _chunkMapSizeY; ++z)
            {
                var chunk = Instantiate(chunkPrefab, transform) as GameObject;
            }
        }
    }
}

public struct ChunkPos
{
    public int x, z;
    public ChunkPos(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
}
