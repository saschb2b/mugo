using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace cgimin.engine.object3d
{
    class CubeObject3D : BaseObject3D
    {

		public CubeObject3D()  
        {
            /*
            Vertices = new List<Vector3>
            {
                new Vector3(-1.0f, -1.0f,  1.0f),
                new Vector3( 1.0f, -1.0f,  1.0f),
                new Vector3( 1.0f,  1.0f,  1.0f),
                new Vector3(-1.0f,  1.0f,  1.0f),
                new Vector3(-1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f, -1.0f, -1.0f),
                new Vector3( 1.0f,  1.0f, -1.0f),
                new Vector3(-1.0f,  1.0f, -1.0f)
            };

            UVs = new List<Vector2>
            {
                new Vector2(0.0f, 0.0f),
                new Vector2(1.0f, 0.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(0.0f, 1.0f),
                new Vector2(0.0f, 0.0f),
                new Vector2(1.0f, 0.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(0.0f, 1.0f)
            };

            Indices = new List<int>
            {
                0, 1, 2, 2, 3, 0,
                3, 2, 6, 6, 7, 3,
                7, 6, 5, 5, 4, 7,
                4, 0, 3, 3, 7, 4,
                0, 1, 5, 5, 4, 0,
                1, 5, 6, 6, 2, 1,
            };
            */

            Vertices = new List<Vector3>();
            UVs = new List<Vector2>();
            Normals = new List<Vector3>();
            Indices = new List<int>();

            addTriangle(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1),
                        new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1));


            addTriangle(new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 2, 0), new Vector3(1, 1, 1), new Vector3(1, 1, 1), new Vector3(1, 1, 1),
                        new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1));

            CreateVertexBuffer();
            CreateUVBuffer();
            CreateIndexBuffer();

        }


    }
}
