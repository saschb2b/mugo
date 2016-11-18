using System;
using Engine.cgimin.material.simpletexture;
using Engine.cgimin.object3d;
using Engine.cgimin.texture;
using OpenTK;

namespace Mugo
{
	class PizzaModel : BaseObject3D, ITunnelSegementElementModel
	{
		private static readonly PizzaModelInternal internalModel = new PizzaModelInternal();
		private static readonly SimpleTextureMaterial material = new SimpleTextureMaterial();

		public Matrix4 DefaultTransformation { get; private set;}

		public new Matrix4 Transformation {
			get { return base.Transformation; }
			set { base.Transformation = value; }
		}

		public PizzaModel ()
		{
			DefaultTransformation = internalModel.DefaultTransformation;
			Transformation = internalModel.Transformation;
			Vao = internalModel.Vao;
			Indices = internalModel.Indices;
		}

		public void Draw ()
		{
			material.Draw(this, internalModel.TextureId);
		}

		private class PizzaModelInternal : ObjLoaderObject3D
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
}
