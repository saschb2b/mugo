
// This code was written for the OpenTK library and has been released
// to the Public Domain.
// It is provided "as is" without express or implied warranty of any kind.

#region --- Using Directives ---

using System;
using System.IO;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using cgimin.engine.object3d;
using cgimin.engine.texture;
using cgimin.engine.material.simpletexture;
using cgimin.engine.material.wobble1;

#endregion --- Using Directives ---

namespace Examples.Tutorial
{
	
 
    public class CubeExample : GameWindow
    {

        static float angle = 0.0f;

        private ObjLoaderObject3D minecart;
        private int texture;

        private SimpleTextureMaterial simpleTextureMaterial;
        private Wobble1Material wobble1Material;
        private Wobble2Material wobble2Material;
		private TunnelObject3D tunnel;

        public CubeExample()
            : base(800, 600, GraphicsMode.Default)
        { }

       
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

			minecart = new ObjLoaderObject3D("data/objects/cart.obj");
			texture = TextureManager.LoadTexture("data/textures/road_rails_0039_01_s.png");
			tunnel = new TunnelObject3D();

            simpleTextureMaterial = new SimpleTextureMaterial();
//            wobble1Material = new Wobble1Material();
//            wobble2Material = new Wobble2Material();
//
            GL.Enable(EnableCap.DepthTest);
        }
 

        protected override void OnUnload(EventArgs e)
        {
			tunnel.UnLoad();
			minecart.UnLoad ();
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
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear( ClearBufferMask.ColorBufferBit |
                       ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(1f, 0.5f, 1.0f, 1f, 0.5f, -10, 0, 1, 0);

            GL.MatrixMode(MatrixMode.Modelview);

            GL.LoadMatrix(ref lookat);

      //      angle += (float)e.Time;
//            GL.Rotate(angle * 4, 0f, 1.0f, 0.0f);
//            GL.Rotate(angle * 5, 0.0f, 1.0f, 0.0f);
//            GL.Rotate(angle * 5, 0.0f, 0.0f, 1.0f);

			simpleTextureMaterial.draw(tunnel, texture);
			//simpleTextureMaterial.draw(minecart, 0);

            SwapBuffers();
        }

        
        [STAThread]
        public static void Main()
        {
            using (CubeExample example = new CubeExample())
            {
                // Get the title and category  of this example using reflection.
                //ExampleAttribute info = ((ExampleAttribute)example.GetType().GetCustomAttributes(false)[0]);
                //example.Title = String.Format("OpenTK | {0} {1}: {2}", info.Category, info.Difficulty, info.Title);
                example.Run(30.0, 0.0);
            }
        }

      
    }
}
