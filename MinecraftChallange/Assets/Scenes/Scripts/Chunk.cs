using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	private const int sizeX = 16;
	private const int maxHeight = 64;

	public BlockType[,,] blocks = new BlockType[sizeX + 2, maxHeight, sizeX + 2];

	

	public void UpdateMesh(int positionX, int positionZ)
    {
		int vertexIndex = 0;
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector2> uvs = new List<Vector2>();

		for (int x = positionX + 1; x < positionX + sizeX + 1; ++x)
		{
			for (int z = positionZ + 1; z < positionZ + sizeX + 1; ++z)
			{		
				for (int y = 0; y < maxHeight; ++y)
				{	
					var blockPos = new Vector3Int(x - 1, y, z - 1);
					if (blocks[x - positionX, y, z - positionZ] != BlockType.Air)
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

	public void GenerateChunk(int positionX, int positionZ)
    {
		for (int x = positionX + 1; x < positionX + sizeX + 1; ++x)
		{
			for (int z = positionZ + 1; z < positionZ + sizeX + 1; ++z)
			{
				int terrainHeight = 8;
				int height = (int)(Mathf.PerlinNoise(x * .035f, z * .035f) * terrainHeight) + maxHeight - terrainHeight - 32;
				blocks[x - positionX, height, z - positionZ] = BlockType.Dirt;
				blocks[x - positionX, height + 1, z - positionZ] = BlockType.Grass;		
			}
		}
		UpdateMesh(positionX, positionZ);
	}

}