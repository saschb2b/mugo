using System;
using Engine.cgimin.material.simpletexture;
using Engine.cgimin.object3d;
using Engine.cgimin.texture;
using OpenTK;

namespace Mugo
{
	class PizzaModel : ClonedObject<PizzaModelInternal>, ITunnelSegementElementModel
	{
		private static readonly SimpleTextureMaterial material = new SimpleTextureMaterial();

		public new Matrix4 Transformation {
			get { return base.Transformation; }
			set { base.Transformation = value; }
		}

		public void Draw ()
		{
			material.Draw(this, internalOject.TextureId);
		}
	}

	internal class PizzaModelInternal : ObjLoaderObject3D
	{
		private const String objFilePath = "data/objects/Pizza.model";
		private const String texturePath = "data/textures/Pizza.png";

		public PizzaModelInternal () : base (objFilePath)
		{
			TextureId = TextureManager.LoadTexture (texturePath);
			Transformation *= Matrix4.CreateScale (0.2f);
			Transformation *= Matrix4.CreateRotationY (MathHelper.DegreesToRadians (90));
			Transformation *= Matrix4.CreateRotationX (MathHelper.DegreesToRadians (90));
			Transformation *= Matrix4.CreateTranslation (0f, 1.5f, -TunnelSegment.Depth / 2f);

			DefaultTransformation = Transformation;
		}

		public int TextureId { get; private set; }

		public Matrix4 DefaultTransformation { get; }
	}
}
