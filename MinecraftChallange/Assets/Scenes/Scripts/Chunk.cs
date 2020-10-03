using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	private const int sizeX = 16;
	private const int maxHeight = 10;
	private int positionX;
	private int positionZ;

	private BlockType[,,] blocks = new BlockType[sizeX + 2, maxHeight, sizeX + 2];

	public void SetPosition(int _positionX, int _positionZ)
    {
		positionX = _positionX;
		positionZ = _positionZ;
    }

	public void GenerateChunk()
    {
		int vertexIndex = 0;
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector2> uvs = new List<Vector2>();


		for (int x = positionX; x < positionX + sizeX; ++x)
		{
			for (int z = positionZ; z < positionZ + sizeX; ++z)
			{
				int height = (int)(Mathf.PerlinNoise(x * .05f, z * .05f) * 10 - 1);
				var blockPos = new Vector3(x, height, z);
				blocks[x - positionX, height, z - positionZ] = BlockType.Dirt;	
			}
		}

		for (int x = positionX + 1; x < positionX + sizeX; ++x)
		{
			for (int z = positionZ + 1; z < positionZ + sizeX; ++z)
			{
				for(int y = 0; y < maxHeight; ++y)
                {
					var blockPos = new Vector3Int(x - 1, y, z - 1);
					if(blocks[x - positionX, y, z - positionZ] == BlockType.Dirt)
                    {
						if (y < maxHeight - 1 && blocks[x - positionX, y + 1, z - positionZ] == BlockType.Air) // Top
						{
							for (int i = 0; i < 6; i++)
							{
								int triangleIndex = VoxelData.voxelTris[2, i];
								vertices.Add(blockPos + VoxelData.voxelVerts[triangleIndex]);
								triangles.Add(vertexIndex);

								uvs.Add(VoxelData.voxelUvs[i]);

								vertexIndex++;
							}
						}

						if (y > 0 && blocks[x - positionX, y - 1, z - positionZ] == BlockType.Air) // Bottom
						{
							for (int i = 0; i < 6; i++)
							{
								int triangleIndex = VoxelData.voxelTris[3, i];
								vertices.Add(blockPos + VoxelData.voxelVerts[triangleIndex]);
								triangles.Add(vertexIndex);

								uvs.Add(VoxelData.voxelUvs[i]);

								vertexIndex++;
							}
						}

						if (blocks[x - positionX + 1, y, z - positionZ] == BlockType.Air) // Right
                        {
							for (int i = 0; i < 6; i++)
							{
								int triangleIndex = VoxelData.voxelTris[5, i];
								vertices.Add(blockPos + VoxelData.voxelVerts[triangleIndex]);
								triangles.Add(vertexIndex);

								uvs.Add(VoxelData.voxelUvs[i]);

								vertexIndex++;
							}
						}

						if (blocks[x - positionX - 1, y, z - positionZ] == BlockType.Air) // Left
						{
							for (int i = 0; i < 6; i++)
							{
								int triangleIndex = VoxelData.voxelTris[4, i];
								vertices.Add(blockPos + VoxelData.voxelVerts[triangleIndex]);
								triangles.Add(vertexIndex);

								uvs.Add(VoxelData.voxelUvs[i]);

								vertexIndex++;
							}
						}

						if (blocks[x - positionX, y, z - positionZ - 1] == BlockType.Air) // Front
						{
							for (int i = 0; i < 6; i++)
							{
								int triangleIndex = VoxelData.voxelTris[0, i];
								vertices.Add(blockPos + VoxelData.voxelVerts[triangleIndex]);
								triangles.Add(vertexIndex);

								uvs.Add(VoxelData.voxelUvs[i]);

								vertexIndex++;
							}
						}

						if (blocks[x - positionX, y, z - positionZ + 1] == BlockType.Air) // Back
						{
							for (int i = 0; i < 6; i++)
							{
								int triangleIndex = VoxelData.voxelTris[1, i];
								vertices.Add(blockPos + VoxelData.voxelVerts[triangleIndex]);
								triangles.Add(vertexIndex);

								uvs.Add(VoxelData.voxelUvs[i]);

								vertexIndex++;
							}
						}
					}
					
                }
				
			}
		}

		Mesh mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.uv = uvs.ToArray();

		mesh.RecalculateNormals();

		GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}
}