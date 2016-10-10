using System;
using OpenTK.Graphics.OpenGL;
using cgimin.engine.object3d;

namespace cgimin.engine.material.wobble1
{
    class Wobble1Material : BaseMaterial
    {

        public Wobble1Material()
        {
            CreateShaders("cgimin/engine/material/wobble1/Wobble1_VS.glsl",
                          "cgimin/engine/material/wobble1/Wobble1_FS.glsl");
        }

        public void draw(BaseObject3D object3d, int textureID)
        {

            GL.BindTexture(TextureTarget.Texture2D, textureID);

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, object3d.VertexBufferObject);
            GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);

            GL.BindBuffer(BufferTarget.ArrayBuffer, object3d.UVBufferObject);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, IntPtr.Zero);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, object3d.IndexBuffer);

            GL.UseProgram(program);

            GL.DrawElements(BeginMode.Triangles, object3d.Indices.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
        }



    }
}
