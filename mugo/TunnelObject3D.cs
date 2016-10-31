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

            const int width = 2, height = 3, depth = 10;

			//left
			addTriangle(
				new Vector3(0, height, -depth), new Vector3(0, height, 0), new Vector3(0, 0, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(1, 0), new Vector2(0, 0), new Vector2(0, 1));

			addTriangle(
				new Vector3(0, 0, 0), new Vector3(0, 0, -depth), new Vector3(0, height, -depth), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0));

			//right
			addTriangle(
				new Vector3(width, 0, 0), new Vector3(width, height, 0), new Vector3(width, height, -depth), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0));

			addTriangle(
				new Vector3(width, height, -depth), new Vector3(width, 0, -depth), new Vector3(width, 0, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1));

			//bottom
			addTriangle(
				new Vector3(width, 0, 0), new Vector3(0, 0, -depth), new Vector3(0, 0, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, 0));

			addTriangle(
				new Vector3(width, 0, 0), new Vector3(width, 0, -depth), new Vector3(0, 0, -depth), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0));

			//top
			addTriangle(
				new Vector3(0, height, 0), new Vector3(0, height, -depth), new Vector3(width, height, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1));

			addTriangle(
				new Vector3(0, height, -depth), new Vector3(width, height, -depth), new Vector3(width, height, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1));

			CreateVAO();
		}
	}
}