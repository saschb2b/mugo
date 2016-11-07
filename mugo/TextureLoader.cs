using System;
using System.Collections.Generic;
using Engine.cgimin.texture;
using System.IO;

namespace Mugo
{
	public static class TextureLoader
	{
		private static readonly Dictionary<String, TextureHolder> textures = new Dictionary<string, TextureHolder>();

		public static TextureHolder Load(String path)
		{
			TextureHolder texture;
			if(!textures.TryGetValue(path, out texture))
			{
				var normalTexturePath = Path.ChangeExtension(path, "normals" + Path.GetExtension(path));
				var normalMapId = 0;
				if (File.Exists(normalTexturePath)) {
					normalMapId = TextureManager.LoadTexture(normalTexturePath);
				}

				texture = new TextureHolder(TextureManager.LoadTexture(path), normalMapId);
			}

			return texture;
		}
	}

	public class TextureHolder
	{
		public TextureHolder(int textureId, int normalMapId)
		{
			TextureId = textureId;
			NormalMapId = normalMapId;
		}

		public int TextureId {
			get;
			private set;
		}

		public int NormalMapId {
			get;
			private set;
		}
	}
}

