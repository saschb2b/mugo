using System;
using Engine.cgimin.material.simpletexture;
using Engine.cgimin.object3d;
using Engine.cgimin.texture;
using OpenTK;
using Engine.cgimin.material.normalmapping;

namespace Mugo
{
	class RockModel : ClonedObject<RockModelInternal>, ITunnelSegementElementModel
	{
		private static readonly NormalMappingMaterial material = new NormalMappingMaterial();

		public float Radius => radius;

		public new Matrix4 Transformation {
			get { return base.Transformation; }
			set { base.Transformation = value; }
		}

		public void Draw ()
		{
			material.Draw(this, internalObject.Textures.TextureId, internalObject.Textures.NormalMapId, 1);
		}
	}

	internal class RockModelInternal : ObjLoaderObject3D
	{
		private const String objFilePath = "data/objects/Rock.model";
		private const String texturePath = "data/textures/Rock.png";

		public RockModelInternal () : base (objFilePath, scaleFactor: 0.3f)
		{
			Textures = TextureLoader.Load(texturePath);
			//Transformation *= Matrix4.CreateScale (0.3f);
			Transformation *= Matrix4.CreateTranslation (0f, 0f, -TunnelSegmentConfig.Depth / 2f);

			DefaultTransformation = Transformation;
		}

		public TextureHolder Textures { get; private set; }

		public Matrix4 DefaultTransformation { get; }
	}
}
