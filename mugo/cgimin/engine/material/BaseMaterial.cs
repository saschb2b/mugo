using System;
using OpenTK.Graphics.OpenGL;
using cgimin.engine.object3d;
using System.IO;

namespace cgimin.engine.material
{
    class BaseMaterial
    {

        public int vertexObject;
        public int fragmentObject;
        public int program;

        public void CreateShaders(string pathVS, string pathFS)
        {

            string vs = File.ReadAllText(pathVS);
            string fs = File.ReadAllText(pathFS);

            int status_code;
            string info;

            vertexObject = GL.CreateShader(ShaderType.VertexShader);
            fragmentObject = GL.CreateShader(ShaderType.FragmentShader);

            // Compile vertex shader
            GL.ShaderSource(vertexObject, vs);
            GL.CompileShader(vertexObject);
            GL.GetShaderInfoLog(vertexObject, out info);
            GL.GetShader(vertexObject, ShaderParameter.CompileStatus, out status_code);

            if (status_code != 1)
                throw new ApplicationException(info);

            // Compile fragment shader
            GL.ShaderSource(fragmentObject, fs);
            GL.CompileShader(fragmentObject);
            GL.GetShaderInfoLog(fragmentObject, out info);
            GL.GetShader(fragmentObject, ShaderParameter.CompileStatus, out status_code);

            if (status_code != 1)
                throw new ApplicationException(info);

            program = GL.CreateProgram();
            GL.AttachShader(program, fragmentObject);
            GL.AttachShader(program, vertexObject);

            GL.LinkProgram(program);
        }



    }
}
