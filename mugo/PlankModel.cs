using System;
using Engine.cgimin.material.simpletexture;
using Engine.cgimin.object3d;
using Engine.cgimin.texture;
using OpenTK;
using Engine.cgimin.material.normalmappingfogshadowcascaded;

namespace Mugo
{
	class PlankModel : ClonedObject<PlankModelInternal>, ITunnelSegementElementModel
	{
		private static readonly NormalMappingMaterialFogShadowCascaded material = new NormalMappingMaterialFogShadowCascaded();

		public BaseObject3D BaseObject => internalObject;

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

	internal class PlankModelInternal : ObjLoaderObject3D
	{
		private const String objFilePath = "data/objects/woodplank.obj";
		private const String texturePath = "data/textures/Textura_tabla_2.jpg";

		public PlankModelInternal() : base (objFilePath, scaleFactor: 0.3f)
		{
			Textures = TextureLoader.Load(texturePath);
            //Transformation *= Matrix4.CreateScale (0.3f);
            Transformation *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(90));
            Transformation *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(90));
            Transformation *= Matrix4.CreateTranslation (0f, TunnelSegmentConfig.Height - 1.5f, -TunnelSegmentConfig.Depth / 2f);

			DefaultTransformation = Transformation;
		}

		public TextureHolder Textures { get; private set; }

		public Matrix4 DefaultTransformation { get; }
	}
}
