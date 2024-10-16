using UnityEngine;

public class SmoothShadingDemo : MonoBehaviour
{

    public void CreateMesh(VertexData[] vertexData, int numVertices, bool useFlatShading)
	{

		vertexIndexMap.Clear();
		processedVertices.Clear();
		processedNormals.Clear();
		processedTriangles.Clear();

		int triangleIndex = 0;

		for (int i = 0; i < numVertices; i++)
		{
			VertexData data = vertexData[i];

			int sharedVertexIndex;
			if (!useFlatShading && vertexIndexMap.TryGetValue(data.id, out sharedVertexIndex))
			{
				processedTriangles.Add(sharedVertexIndex);
			}
			else
			{
				if (!useFlatShading)
				{
					vertexIndexMap.Add(data.id, triangleIndex);
				}
				processedVertices.Add(data.position);
				processedNormals.Add(data.normal);
				processedTriangles.Add(triangleIndex);
				triangleIndex++;
			}
		}

		collider.sharedMesh = null;

		mesh.Clear();
		mesh.SetVertices(processedVertices);
		mesh.SetTriangles(processedTriangles, 0, true);

		if (useFlatShading)
		{
			mesh.RecalculateNormals();
		}
		else
		{
			mesh.SetNormals(processedNormals);
		}

		collider.sharedMesh = mesh;
	} 
}

