using JetBrains.Annotations;
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
                        int topTextureIndex;
                        int textureIndex;
                        switch (blocks[x - positionX, y, z - positionZ])
                        {
                            case BlockType.Dirt:
                                textureIndex = 0;
                                topTextureIndex = textureIndex;
                                break;
                            case BlockType.Stone:
                                textureIndex = 1;
                                topTextureIndex = textureIndex;
                                break;
                            case BlockType.Grass:
                                textureIndex = 4;
                                topTextureIndex = 5;
                                break;
                            case BlockType.Log:
                                textureIndex = 8;
                                topTextureIndex = 9;
                                break;
                            case BlockType.Leaves:
                                textureIndex = 10;
                                topTextureIndex = textureIndex;
                                break;
                            default:
                                textureIndex = 0;
                                topTextureIndex = textureIndex;
                                break;
                        }
                        if (y < maxHeight - 1 && blocks[x - positionX, y + 1, z - positionZ] == BlockType.Air) // Top
						{
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[2, 0]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[2, 1]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[2, 2]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[2, 3]]);

							AddTexture(topTextureIndex, ref uvs);

							triangles.Add(vertexIndex);
							triangles.Add(vertexIndex + 1);
							triangles.Add(vertexIndex + 2);
							triangles.Add(vertexIndex + 2);
							triangles.Add(vertexIndex + 1);
							triangles.Add(vertexIndex + 3);
							vertexIndex += 4;
						}

						if (y > 0 && blocks[x - positionX, y - 1, z - positionZ] == BlockType.Air) // Bottom
						{
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[3, 0]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[3, 1]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[3, 2]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[3, 3]]);

							AddTexture(textureIndex, ref uvs);

							triangles.Add(vertexIndex);
							triangles.Add(vertexIndex + 1);
							triangles.Add(vertexIndex + 2);
							triangles.Add(vertexIndex + 2);
							triangles.Add(vertexIndex + 1);
							triangles.Add(vertexIndex + 3);
							vertexIndex += 4;
						}

						if (blocks[x - positionX + 1, y, z - positionZ] == BlockType.Air) // Right
						{
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[5, 0]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[5, 1]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[5, 2]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[5, 3]]);

							AddTexture(textureIndex, ref uvs);

							triangles.Add(vertexIndex);
							triangles.Add(vertexIndex + 1);
							triangles.Add(vertexIndex + 2);
							triangles.Add(vertexIndex + 2);
							triangles.Add(vertexIndex + 1);
							triangles.Add(vertexIndex + 3);
							vertexIndex += 4;
						}

						if (blocks[x - positionX - 1, y, z - positionZ] == BlockType.Air) // Left
						{
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[4, 0]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[4, 1]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[4, 2]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[4, 3]]);

							AddTexture(textureIndex, ref uvs);

							triangles.Add(vertexIndex);
							triangles.Add(vertexIndex + 1);
							triangles.Add(vertexIndex + 2);
							triangles.Add(vertexIndex + 2);
							triangles.Add(vertexIndex + 1);
							triangles.Add(vertexIndex + 3);
							vertexIndex += 4;
						}

						if (blocks[x - positionX, y, z - positionZ - 1] == BlockType.Air) // Front
						{
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[0, 0]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[0, 1]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[0, 2]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[0, 3]]);

							AddTexture(textureIndex, ref uvs);

							triangles.Add(vertexIndex);
							triangles.Add(vertexIndex + 1);
							triangles.Add(vertexIndex + 2);
							triangles.Add(vertexIndex + 2);
							triangles.Add(vertexIndex + 1);
							triangles.Add(vertexIndex + 3);
							vertexIndex += 4;
						}

						if (blocks[x - positionX, y, z - positionZ + 1] == BlockType.Air) // Back
						{
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[1, 0]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[1, 1]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[1, 2]]);
							vertices.Add(blockPos + VoxelData.voxelVerts[VoxelData.voxelTris[1, 3]]);

							AddTexture(textureIndex, ref uvs);

							triangles.Add(vertexIndex);
							triangles.Add(vertexIndex + 1);
							triangles.Add(vertexIndex + 2);
							triangles.Add(vertexIndex + 2);
							triangles.Add(vertexIndex + 1);
							triangles.Add(vertexIndex + 3);
							vertexIndex += 4;
						}
					}
				}

			}
		}

        Mesh mesh = new Mesh
        {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray(),
            uv = uvs.ToArray()
        };

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
				for(int i = 0; i < height; ++i)
                {
					blocks[x - positionX, i, z - positionZ] = BlockType.Stone;
				}
			}
		}
		GenerateTrees(positionX, positionZ);
		UpdateMesh(positionX, positionZ);
	}

	public void GenerateTrees(int positionX, int positionZ)
    {
		for (int x = positionX + 3; x < positionX + sizeX - 1; ++x)
		{
			for (int z = positionZ + 3; z < positionZ + sizeX - 1; ++z)
			{
				for(int y = 0; y < maxHeight; ++y)
                {		
					if(blocks[x - positionX, y, z - positionZ] != BlockType.Air)
                    {
						if (blocks[x - positionX, y, z - positionZ] == BlockType.Grass)
						{
							float chanceToSpawnTree = 0.125f;
							if (Random.Range(0f, 100f) < chanceToSpawnTree)
							{
								int treeHeight = Random.Range(4, 6);
								for(int i = 1; i < treeHeight; ++i)
                                {
									blocks[x - positionX, y + i, z - positionZ] = BlockType.Log;
                                }
								int leavesHeight = Random.Range(3, 5);
								int leavesWidth = Random.Range(4, 6);

								for(int m = -leavesWidth / 2 + 1; m < leavesWidth / 2; ++m)
                                {
									for(int n = -leavesWidth / 2 + 1; n < leavesWidth / 2; ++n)
                                    {
										for(int j = 0; j < leavesHeight; ++j)
                                        {
											blocks[x - positionX + m, y + treeHeight + j, z - positionZ + n] = BlockType.Leaves;
                                        }
                                    }
                                }

								blocks[x - positionX, y + treeHeight + leavesHeight, z - positionZ] = BlockType.Leaves;	
							}
						}
					}
                }
			}
		}

	}

	private void AddTexture(int _textureIndex, ref List<Vector2> uvs)
    {
		float uvsY = _textureIndex / VoxelData.TextureAtlasSizeInBlocks;
		float uvsX = _textureIndex - (uvsY * VoxelData.TextureAtlasSizeInBlocks);

		uvsX *= VoxelData.NormalizedBlockTextureSize;
		uvsY *= VoxelData.NormalizedBlockTextureSize;

		uvsY = 1f - uvsY - VoxelData.NormalizedBlockTextureSize;

		uvs.Add(new Vector2(uvsX, uvsY));
		uvs.Add(new Vector2(uvsX, uvsY + VoxelData.NormalizedBlockTextureSize));
		uvs.Add(new Vector2(uvsX + VoxelData.NormalizedBlockTextureSize, uvsY));
		uvs.Add(new Vector2(uvsX + VoxelData.NormalizedBlockTextureSize, uvsY + VoxelData.NormalizedBlockTextureSize));
	}
}