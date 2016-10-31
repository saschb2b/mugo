using System.Collections.Generic;
using Engine.cgimin.object3d;
using Engine.cgimin.texture;
using OpenTK;

namespace Mugo
{
    class TunnelSegment : BaseObject3D
	{
		public const int Width = 2;
		public const int Height = 3;
		public const int Depth = 10;

		public TunnelSegment()
		{
			TextureId = TextureManager.LoadTexture("data/textures/road_rails_0039_01_s.png");

			Positions = new List<Vector3>();
			UVs = new List<Vector2>();
			Normals = new List<Vector3>();
			Indices = new List<int>();

			//left
			addTriangle(
				new Vector3(0, Height, -Depth), new Vector3(0, Height, 0), new Vector3(0, 0, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(1, 0), new Vector2(0, 0), new Vector2(0, 1));

			addTriangle(
				new Vector3(0, 0, 0), new Vector3(0, 0, -Depth), new Vector3(0, Height, -Depth), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0));

			//right
			addTriangle(
				new Vector3(Width, 0, 0), new Vector3(Width, Height, 0), new Vector3(Width, Height, -Depth), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0));

			addTriangle(
				new Vector3(Width, Height, -Depth), new Vector3(Width, 0, -Depth), new Vector3(Width, 0, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1));

			//bottom
			addTriangle(
				new Vector3(Width, 0, 0), new Vector3(0, 0, -Depth), new Vector3(0, 0, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, 0));

			addTriangle(
				new Vector3(Width, 0, 0), new Vector3(Width, 0, -Depth), new Vector3(0, 0, -Depth), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0));

			//top
			addTriangle(
				new Vector3(0, Height, 0), new Vector3(0, Height, -Depth), new Vector3(Width, Height, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1));

			addTriangle(
				new Vector3(0, Height, -Depth), new Vector3(Width, Height, -Depth), new Vector3(Width, Height, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1));

			CreateVAO();
		}

		public int TextureId { get; private set; }
	}
}