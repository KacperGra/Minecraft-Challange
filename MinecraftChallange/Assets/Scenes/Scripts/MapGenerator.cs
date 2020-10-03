using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject chunkPrefab;
    private int chunkMapSize = 16;
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
                if (x > 0 || z > 0)
                {
                    chunk.GetComponent<Chunk>().SetPosition(x * (16 - 1), z * (16 - 1));
                }
                else
                {
                    chunk.GetComponent<Chunk>().SetPosition(x * 16, z * 16);
                }
                chunk.GetComponent<Chunk>().GenerateChunk();
            }
        }
    }
}

