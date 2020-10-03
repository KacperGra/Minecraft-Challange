using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform player;
    public GameObject chunkPrefab;
    private int chunkMapSize = 2;
    public static Dictionary<ChunkPosition, Chunk> chunkDictionary = new Dictionary<ChunkPosition, Chunk>();
    private Vector2Int currentChunk = new Vector2Int();
    private int renderDistance = 4;


    private void Start()
    {
        currentChunk = new Vector2Int(0, 0);
        GenerateMap(chunkMapSize, chunkMapSize);
    }

    private void Update()
    {
        CheckChunk(ref currentChunk); 
    }

    void GenerateChunk(int _posX, int _posZ)
    {
        var newChunk = Instantiate(chunkPrefab, transform) as GameObject;

        newChunk.GetComponent<Chunk>().GenerateChunk(_posX * 15, _posZ * 15);

        chunkDictionary.Add(new ChunkPosition(_posX, _posZ), newChunk.GetComponent<Chunk>());
    }

    void GenerateMap(int _chunkMapSizeX, int _chunkMapSizeY)
    {
        for(int x = -8; x < _chunkMapSizeX; ++x)
        { 
            for(int z = -8; z < _chunkMapSizeY; ++z)
            {
                GenerateChunk(x, z);
            }
        }
    }

    void DisplayChunk(int _chunkPositionX, int _chunkPositionZ)
    {
        if(chunkDictionary.ContainsKey(new ChunkPosition(_chunkPositionX, _chunkPositionZ)))
        {
            chunkDictionary[new ChunkPosition(_chunkPositionX, _chunkPositionZ)].gameObject.SetActive(true);
        }
        else
        {
            GenerateChunk(_chunkPositionX, _chunkPositionZ);
        }
    }

    void HideChunk(int _chunkPositionX, int _chunkPositionZ)
    {

    }

    void CheckChunk(ref Vector2Int _currentChunk)
    {
        var acctualChunk = new Vector2Int((int)player.position.x / 16, (int)player.position.z / 16);
        if(acctualChunk != _currentChunk)
        {
            _currentChunk = new Vector2Int((int)player.position.x / 16, (int)player.position.z / 16);
            RenderChunks();
        }
    }

    void RenderChunks()
    {
        for (int x = currentChunk.x - renderDistance; x < currentChunk.x + renderDistance; ++x)
        {
            for (int z = currentChunk.y - renderDistance; z < currentChunk.y + renderDistance; ++z)
            {
                DisplayChunk(x, z);
            }
        }
    }
}

public struct ChunkPosition
{
    public int x, z;
    public ChunkPosition(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
}

