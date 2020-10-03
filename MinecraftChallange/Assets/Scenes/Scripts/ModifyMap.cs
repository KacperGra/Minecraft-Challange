using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyMap : MonoBehaviour
{
    public LayerMask groundLayerMask;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit rayCastHit;
            if (Physics.Raycast(transform.position, transform.forward, out rayCastHit, 5, groundLayerMask))
            {
                Vector3 clickedBlock;

                clickedBlock = rayCastHit.point + transform.forward * .01f;

                int chunkPosX = (int)clickedBlock.x / 16;
                int chunkPosZ = (int)clickedBlock.z / 16;
                Debug.Log("Clicked: X: " + chunkPosX.ToString() + " Y: " + chunkPosZ.ToString());

                ChunkPosition cp = new ChunkPosition(chunkPosX, chunkPosZ);

                Chunk chunk = MapGenerator.chunkDictionary[cp];

                //index of the target block
                int bix = Mathf.FloorToInt(clickedBlock.x) - chunkPosX + 1;
                int biy = Mathf.FloorToInt(clickedBlock.y) - 1;
                int biz = Mathf.FloorToInt(clickedBlock.z) - chunkPosZ + 1;
                Debug.Log("Clicked pos: X: " + bix.ToString() + " Y: " + biy.ToString() + " Z: " + biz.ToString());

                chunk.blocks[bix, biy, biz] = BlockType.Air;
                chunk.UpdateMesh(chunkPosX, chunkPosZ);
            }
        }
    }
}
