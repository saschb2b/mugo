using System;
using Engine.cgimin.material.simpletexture;
using Engine.cgimin.material;

namespace Mugo
{
	public class Tunnel
	{
		private TunnelSegment segment;
		private SimpleTextureMaterial material;

		public Tunnel()
		{
			segment = new TunnelSegment();
			material = new SimpleTextureMaterial();
		}

		public void Draw()
		{
			material.Draw(segment, segment.TextureId);
		}

		public void UnLoad() 
		{
			segment.UnLoad();
		}
	}
}

