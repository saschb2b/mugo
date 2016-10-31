
// This code was written for the OpenTK library and has been released
// to the Public Domain.
// It is provided "as is" without express or implied warranty of any kind.

#region --- Using Directives ---

using System;
using Engine.cgimin.camera;
using Engine.cgimin.material.simpletexture;
using Engine.cgimin.texture;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

#endregion --- Using Directives ---

namespace Mugo
{


    public class CubeExample : GameWindow
	{
        private PlayerModel player;
        private CartModel cart;
        private int texture;
        private Matrix4 initialPlayerTransformation;
        private Matrix4 initialCartTransformation;

        private SimpleTextureMaterial simpleTextureMaterial;
		private TunnelObject3D tunnel;

		private float zMover = 0.0f;

		public CubeExample()
			: base(800, 600, GraphicsMode.Default)
		{
		}
       
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			Camera.Init();
			Camera.SetWidthHeightFov(800, 600, 60);

            player = new PlayerModel();
            initialPlayerTransformation = player.Transformation;

            cart = new CartModel();
            initialCartTransformation = cart.Transformation;

            texture = TextureManager.LoadTexture("data/textures/road_rails_0039_01_s.png");
			tunnel = new TunnelObject3D();

			simpleTextureMaterial = new SimpleTextureMaterial();

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);

            GL.Enable(EnableCap.DepthTest);
			GL.ClearColor(0.5f, 0.5f, 0.5f, 0.5f);

			Camera.SetLookAt(new Vector3(1f, 0.5f, 1.5f), new Vector3(1f, 0.5f, -10), new Vector3(0, 1, 0));
		}

		protected override void OnUnload(EventArgs e)
		{
			tunnel.UnLoad();
            player.UnLoad();
            cart.UnLoad();
		}
       
		protected override void OnResize(EventArgs e)
		{
			GL.Viewport(0, 0, Width, Height);

			float aspect_ratio = Width / (float)Height;
			Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref perpective);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			if (Keyboard[OpenTK.Input.Key.Escape])
				this.Exit();

			if (Keyboard[OpenTK.Input.Key.F11])
			if (WindowState != WindowState.Fullscreen)
				WindowState = WindowState.Fullscreen;
			else
				WindowState = WindowState.Normal;

            const float step = 0.1f;

            if (zMover <= -10.0f)
            {
                zMover = 0.0f;
                player.Transformation = initialPlayerTransformation;
                cart.Transformation = initialCartTransformation;
            }
            else
            {
                zMover -= step;
            }

            Camera.SetLookAt(new Vector3(1f, 2.0f, 3.0f + zMover), new Vector3(1f, 0.5f, -5 + zMover), new Vector3(0, 1, 0));
            player.Transformation *= Matrix4.CreateTranslation(0, 0, -step);
            cart.Transformation *= Matrix4.CreateTranslation(0, 0, -step);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit |
			ClearBufferMask.DepthBufferBit);

            simpleTextureMaterial.Draw(tunnel, texture);
            simpleTextureMaterial.Draw(player, player.TextureId);
            simpleTextureMaterial.Draw(cart, cart.TextureId);

            SwapBuffers();
		}
        
		[STAThread]
		public static void Main()
		{
			using (CubeExample example = new CubeExample()) {
				// Get the title and category  of this example using reflection.
				//ExampleAttribute info = ((ExampleAttribute)example.GetType().GetCustomAttributes(false)[0]);
				//example.Title = String.Format("OpenTK | {0} {1}: {2}", info.Category, info.Difficulty, info.Title);
				example.Run(30.0, 0.0);
			}
		}
	}
}