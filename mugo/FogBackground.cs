using System;
using Engine.cgimin.object3d;
using OpenTK;
using Engine.cgimin.material.normalmappingfog;

namespace Mugo
{
	public class FogBackground : BaseObject3D
	{
		private TextureHolder textures;
		private NormalMappingMaterialFog material;

		public FogBackground (float width, float height)
		{
			textures = TextureLoader.Load ("data/textures/fog.jpg");
			material = new NormalMappingMaterialFog ();

			addTriangle (new Vector3 (0f, 0f, 0f), new Vector3 (width, height, 0f), new Vector3 (0f, height, 0f),
						 Vector3.One, Vector3.One, Vector3.One,
			             new Vector2 (0f, 0f), new Vector2 (1f, 1f), new Vector2 (0f, 1f)
						);
			
			addTriangle (new Vector3 (0f, 0f, 0f), new Vector3 (width, 0f, 0f), new Vector3 (width, height, 0f),
						 Vector3.One, Vector3.One, Vector3.One,
						 new Vector2 (0f, 0f), new Vector2 (1f, 0f), new Vector2 (1f, 1f)
						);

			averageTangents ();
			CreateVAO ();
		}

		public void Draw()
		{
			material.Draw (this, textures.TextureId, textures.NormalMapId, 1f);
		}
	}
}
