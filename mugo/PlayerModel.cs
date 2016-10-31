﻿using System;
using Engine.cgimin.object3d;
using Engine.cgimin.texture;
using OpenTK;

namespace Mugo
{
    class PlayerModel : ObjLoaderObject3D
    {
        private const String objFilePath = "data/objects/lumberJack.model";
        private const String texturePath = "data/textures/lumberJack_diffuse.png";

        public PlayerModel() : base(objFilePath)
        {
            TextureId = TextureManager.LoadTexture(texturePath);
            Transformation *= Matrix4.CreateScale(0.6f);
            Transformation *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(180));
            Transformation *= Matrix4.CreateTranslation(1.07f, 0.6f, -0.8f);

			DefaultTransformation = Transformation;
        }

        public int TextureId { get; private set; }

		public Matrix4 DefaultTransformation { get; private set; }

		public void ResetTransformation()
		{
			Transformation = DefaultTransformation;
		}
    }
}
