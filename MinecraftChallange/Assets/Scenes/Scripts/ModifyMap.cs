using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ModifyMap : MonoBehaviour
{
    public LayerMask groundLayerMask;

    private const float maxDistance = 5.5f;
    public ItemManager itemManager;

    // Update is called once per frame
    void Update()
    {
        bool leftClick = Input.GetMouseButtonDown(0);
        bool rightClick = Input.GetMouseButtonDown(1);
        if (leftClick || rightClick)
        {
            
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistance, groundLayerMask))
            {
                BlockType currentBlock;
                switch (itemManager.slotIndex)
                {
                    case 0:
                        currentBlock = BlockType.Dirt;
                        break;
                    case 1:
                        currentBlock = BlockType.Grass;
                        break;
                    case 2:
                        currentBlock = BlockType.Stone;
                        break;
                    default:
                        currentBlock = BlockType.Dirt;
                        break;
                }
                
                Vector3 pointInTargetBlock;

                if (leftClick)
                    pointInTargetBlock = hitInfo.point + transform.forward * .01f;
                else
                    pointInTargetBlock = hitInfo.point - transform.forward * .01f;

                int chunkPosX = Mathf.FloorToInt(pointInTargetBlock.x / 16f);
                int chunkPosZ = Mathf.FloorToInt(pointInTargetBlock.z / 16f);

                ChunkPosition cp = new ChunkPosition(chunkPosX, chunkPosZ);
                Chunk tc = MapGenerator.chunkDictionary[cp];

                int bix = Mathf.FloorToInt(pointInTargetBlock.x) - chunkPosX * 16 + 1;
                int biy = Mathf.FloorToInt(pointInTargetBlock.y);
                int biz = Mathf.FloorToInt(pointInTargetBlock.z) - chunkPosZ * 16 + 1;

                if (leftClick)
                {
                    tc.blocks[bix, biy, biz] = BlockType.Air;
                    tc.UpdateMesh(chunkPosX * 16, chunkPosZ * 16);
                }
                else if (rightClick)
                {
                    tc.blocks[bix, biy, biz] = currentBlock;
                    tc.UpdateMesh(chunkPosX * 16, chunkPosZ * 16);
                }
            }
        }
    }
}