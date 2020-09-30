using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	private int sizeX = 16;
	private List<Vector3> blockPos;
	void Start()
	{
		int vertexIndex = 0;
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector2> uvs = new List<Vector2>();

		blockPos = new List<Vector3>();
		for(int x = (int)transform.position.x; x < transform.position.x + sizeX; ++x)
        {
			for(int z = (int)transform.position.z; z < transform.position.z + sizeX; ++z)
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

	}

}