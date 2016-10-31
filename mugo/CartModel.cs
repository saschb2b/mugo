﻿using System;
using Engine.cgimin.object3d;
using Engine.cgimin.texture;
using OpenTK;

namespace Mugo
{
    class CartModel : ObjLoaderObject3D
    {
        private const String objFilePath = "data/objects/cart.model";
        private const String texturePath = "data/textures/minecart_texture.jpg";

        public CartModel() : base(objFilePath)
        {
            TextureId = TextureManager.LoadTexture(texturePath);
            Transformation *= Matrix4.CreateScale(0.4f);
            Transformation *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(90));
            Transformation *= Matrix4.CreateTranslation(1.07f, 0.1f, -0.5f);
        }

        public int TextureId { get; private set; }
    }
}
