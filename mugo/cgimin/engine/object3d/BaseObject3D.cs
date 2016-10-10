using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace cgimin.engine.object3d
{

    class BaseObject3D
    {
        public List<Vector3> Vertices;
        public List<Vector3> Normals;
        public List<Vector2> UVs;
        public List<int> Indices;

        public int VertexBufferObject;
        public int NormalBufferObject;
        public int UVBufferObject;
        public int IndexBuffer;

        public void CreateVertexBuffer()
        {
            GL.GenBuffers(1, out VertexBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertices.Count * 3 * sizeof(float)), Vertices.ToArray(), BufferUsageHint.StaticDraw);
        }


        public void CreateNormalBuffer()
        {
            GL.GenBuffers(1, out NormalBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, NormalBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertices.Count * 3 * sizeof(float)), Normals.ToArray(), BufferUsageHint.StaticDraw);
        }


        public void CreateUVBuffer()
        {
            GL.GenBuffers(1, out UVBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, UVBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertices.Count * 2 * sizeof(float)), UVs.ToArray(), BufferUsageHint.StaticDraw);
        }


        public void CreateIndexBuffer()
        {
            GL.GenBuffers(1, out IndexBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Indices.Count * sizeof(Int32)), Indices.ToArray(), BufferUsageHint.StaticDraw);
        }


        public void addTriangle(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 n1, Vector3 n2, Vector3 n3, Vector2 uv1, Vector2 uv2, Vector2 uv3)
        {
			Debug.WriteLine ("Addtrinagle");
            int index = Vertices.Count;

            Vertices.Add(v1);
            Vertices.Add(v2);
            Vertices.Add(v3);

            Normals.Add(n1);
            Normals.Add(n2);
            Normals.Add(n3);

            UVs.Add(uv1);
            UVs.Add(uv2);
            UVs.Add(uv3);

            Indices.Add(index);
            Indices.Add(index + 2);
            Indices.Add(index + 1);
        }


        // Gibt den Grafikspeicher wieder frei
        public void UnLoad()
        {
            if (VertexBufferObject != 0) GL.DeleteShader(VertexBufferObject);
            if (NormalBufferObject != 0) GL.DeleteShader(NormalBufferObject);
            if (UVBufferObject != 0) GL.DeleteShader(UVBufferObject);
            if (IndexBuffer != 0) GL.DeleteShader(IndexBuffer);
        }






    }
}
