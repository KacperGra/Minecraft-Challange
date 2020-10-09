using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyMap : MonoBehaviour
{
    public LayerMask groundLayerMask;

    private const float maxDistance = 5.5f;

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
                    tc.blocks[bix, biy, biz] = BlockType.Dirt;
                    tc.UpdateMesh(chunkPosX * 16, chunkPosZ * 16);
                }
            }
        }
    }
}