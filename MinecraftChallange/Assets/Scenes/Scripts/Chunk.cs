using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	private int sizeX = 16;
	private int positionX;
	private int positionZ;
	private List<Vector3> blockPos;

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

		blockPos = new List<Vector3>();
		for (int x = positionX; x < positionX + sizeX; ++x)
		{
			for (int z = positionZ; z < positionZ + sizeX; ++z)
			{
				int height = (int)(Mathf.PerlinNoise(x * .08f, z * .08f) * 5);

				var blockPos = new Vector3(x, height, z);

				for (int p = 0; p < 6; p++)
				{
					for (int i = 0; i < 6; i++)
					{

						int triangleIndex = VoxelData.voxelTris[p, i];
						vertices.Add(blockPos + VoxelData.voxelVerts[triangleIndex]);
						triangles.Add(vertexIndex);

						uvs.Add(VoxelData.voxelUvs[i]);

						vertexIndex++;

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