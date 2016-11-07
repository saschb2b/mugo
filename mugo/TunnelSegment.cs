using System.Collections.Generic;
using Engine.cgimin.object3d;
using Engine.cgimin.texture;
using OpenTK;

namespace Mugo
{
    public class TunnelSegment : BaseObject3D
	{
		public const float Width = 6;
        public const float RailCount = 3;
		public const int Height = 3;
		public const int Depth = 10;

		public TunnelSegment()
        {
			Textures = TextureLoader.Load("data/textures/textures.jpg");

			//left
			addTriangle(
				new Vector3(0, Height, -Depth), new Vector3(0, Height, 0), new Vector3(0, 0, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0.5f, 0.4f), new Vector2(0, 0.4f), new Vector2(0f, 0.7f));

			addTriangle(
				new Vector3(0, 0, 0), new Vector3(0, 0, -Depth), new Vector3(0, Height, -Depth), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0f, 0.7f), new Vector2(0.5f, 0.7f), new Vector2(0.5f, 0.4f));

			//right
			addTriangle(
				new Vector3(Width, 0, 0), new Vector3(Width, Height, 0), new Vector3(Width, Height, -Depth), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0f, 0.7f), new Vector2(0f, 0.4f), new Vector2(0.5f, 0.4f));

			addTriangle(
				new Vector3(Width, Height, -Depth), new Vector3(Width, 0, -Depth), new Vector3(Width, 0, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0.5f, 0.4f), new Vector2(0.5f, 0.7f), new Vector2(0f, 0.7f));

            //bottom
            for (float i = 0; i < RailCount; i++)
            {
                addTriangle(
                new Vector3(Width / RailCount + (Width / RailCount * i), 0, 0), new Vector3(Width / RailCount * i, 0, -Depth), new Vector3(Width / RailCount * i, 0, 0),
                Vector3.One, Vector3.One, Vector3.One,
                new Vector2(0.3f, 0.4f), new Vector2(0, 0), new Vector2(0, 0.4f));

                addTriangle(
                    new Vector3(Width / RailCount + (Width / RailCount * i), 0, 0), new Vector3(Width / RailCount + (Width / RailCount * i), 0, -Depth), new Vector3(Width / RailCount * i, 0, -Depth),
                    Vector3.One, Vector3.One, Vector3.One,
                    new Vector2(0.3f, 0.4f), new Vector2(0.3f, 0f), new Vector2(0f, 0f));
            }
			

			//top
			addTriangle(
				new Vector3(0, Height, 0), new Vector3(0, Height, -Depth), new Vector3(Width, Height, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0f, 0.4f), new Vector2(0.5f, 0.4f), new Vector2(0f, 0.7f));

			addTriangle(
				new Vector3(0, Height, -Depth), new Vector3(Width, Height, -Depth), new Vector3(Width, Height, 0), 
				Vector3.One, Vector3.One, Vector3.One,
				new Vector2(0.5f, 0.4f), new Vector2(0.5f, 0.7f), new Vector2(0f, 0.7f));

            averageTangents();

            CreateVAO();
        }

		public TextureHolder Textures
		{
			get;
			set;
		}
    }
}