using System;
using Engine.cgimin.material.simpletexture;
using Engine.cgimin.material;
using OpenTK;
using System.Collections.Generic;

namespace Mugo
{
	public class Tunnel
	{
		private readonly List<TunnelSegment> segments = new List<TunnelSegment>();
		private readonly SimpleTextureMaterial material;

		public Tunnel()
		{
			segments = new List<TunnelSegment>() {
				new TunnelSegment(),
				new TunnelSegment(),
				new TunnelSegment()
			};

			material = new SimpleTextureMaterial();

			for (int i = 0; i < segments.Count; i++) {
				segments[i].Transformation *= Matrix4.CreateTranslation(0, 0, (i - 1) * -TunnelSegment.Depth);
			}
		}

		public void Draw()
		{
			foreach(var segment in segments) {
				material.Draw(segment, segment.TextureId);
			}
		}

		public void UnLoad() 
		{
			foreach(var segment in segments) {
				segment.UnLoad();
			}
		}
	}
}

