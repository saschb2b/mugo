﻿using System;
using Engine.cgimin.object3d;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Mugo
{
	public class FogBackground : BaseObject3D
	{
		private TextureHolder textures;
		private FogBackgroundMaterial material;
		private float counter = 0;

		public FogBackground (float width, float height)
		{
			textures = TextureLoader.Load ("data/textures/fog.jpg");
			material = new FogBackgroundMaterial ();

			const int parts = 10;

			for (int i = 1; i <= parts; i++) {
				float w = (width / parts) * i;
				float h = (height / parts) * i;
				float uv = (1f / parts) * i;

				addTriangle (new Vector3 (0f, 0f, 0f), new Vector3 (w, h, 0f), new Vector3 (0f, h, 0f),
							 Vector3.One, Vector3.One, Vector3.One,
							 new Vector2 (0f, 0f), new Vector2 (uv, uv), new Vector2 (0f, uv)
							);

				addTriangle (new Vector3 (0f, 0f, 0f), new Vector3 (w, 0f, 0f), new Vector3 (w, h, 0f),
							 Vector3.One, Vector3.One, Vector3.One,
							 new Vector2 (0f, 0f), new Vector2 (uv, 0f), new Vector2 (uv, uv)
							);
			}

			averageTangents ();
			CreateVAO ();
		}

		public void Draw()
		{
			material.Draw (this, textures.TextureId, textures.NormalMapId, 1f, counter);
			counter += 0.1f;
		}
	}
}
