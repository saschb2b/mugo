using System.Collections.Generic;
using Engine.cgimin.object3d;
using OpenTK;

namespace Mugo
{
    class TunnelObject3D : BaseObject3D
	{
		public TunnelObject3D()
		{
			Positions = new List<Vector3>();
			UVs = new List<Vector2>();
			Normals = new List<Vector3>();
			Indices = new List<int>();

			//left
			addTriangle(
				new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 1, -10), 
				new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1),
				new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0));

			addTriangle(
				new Vector3(0, 1, -10), new Vector3(0, 0, -10), new Vector3(0, 0, 0), 
				new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1),
				new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1));

			//right
			addTriangle(
				new Vector3(2, 0, 0), new Vector3(2, 1, 0), new Vector3(2, 1, -10), 
				new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1),
				new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0));

			addTriangle(
				new Vector3(2, 1, -10), new Vector3(2, 0, -10), new Vector3(2, 0, 0), 
				new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1),
				new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1));

			//bottom
			addTriangle(
				new Vector3(0, 0, 0), new Vector3(0, 0, -10), new Vector3(2, 0, 0), 
				new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1),
				new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1));

			addTriangle(
				new Vector3(2, 0, 0), new Vector3(2, 0, -10), new Vector3(0, 0, -10), 
				new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1),
				new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0));

			//top
			addTriangle(
				new Vector3(0, 1, 0), new Vector3(0, 1, -10), new Vector3(2, 1, 0), 
				new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1),
				new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1));

			addTriangle(
				new Vector3(2, 1, 0), new Vector3(2, 1, -10), new Vector3(0, 1, -10), 
				new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1),
				new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0));

			CreateVAO();
		}
	}
}