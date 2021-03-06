﻿using System;
using Engine.cgimin.material.simpletexture;
using Engine.cgimin.object3d;
using Engine.cgimin.texture;
using OpenTK;
using Engine.cgimin.material.normalmappingfogshadowcascaded;

namespace Mugo
{
	class PizzaModel : ClonedObject<PizzaModelInternal>, ITunnelSegementElementModel
	{
		private static readonly NormalMappingMaterialFogShadowCascaded material = new NormalMappingMaterialFogShadowCascaded();

		public BaseObject3D BaseObject => this;

		public float Radius => radius;

		public new Matrix4 Transformation {
			get { return base.Transformation; }
			set { base.Transformation = value; }
		}

		public void Draw ()
		{
			material.Draw(this, internalObject.TextureId, 0, 1f);
		}
	}

	internal class PizzaModelInternal : ObjLoaderObject3D
	{
		private const String objFilePath = "data/objects/Pizza.model";
		private const String texturePath = "data/textures/Pizza.png";

		public PizzaModelInternal () : base (objFilePath, 0.2f)
		{
			TextureId = TextureManager.LoadTexture (texturePath);
			Transformation *= Matrix4.CreateRotationY (MathHelper.DegreesToRadians (90));
			Transformation *= Matrix4.CreateRotationX (MathHelper.DegreesToRadians (90));
			Transformation *= Matrix4.CreateTranslation (0f, 1.5f, -TunnelSegmentConfig.Depth / 2f);

			DefaultTransformation = Transformation;
		}

		public int TextureId { get; private set; }

		public Matrix4 DefaultTransformation { get; }
	}
}
