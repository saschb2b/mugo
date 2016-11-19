using Engine.cgimin.object3d;
using OpenTK;

namespace Mugo
{
	class ClonedObject<T> : BaseObject3D
		where T : BaseObject3D, new()
	{
		protected static readonly T internalOject = new T();

		public Matrix4 DefaultTransformation { get; protected set; }

		public ClonedObject()
		{
			DefaultTransformation = internalOject.Transformation;
			Transformation = internalOject.Transformation;
			Vao = internalOject.Vao;
			Indices = internalOject.Indices;
			Positions = internalOject.Positions;
			Normals = internalOject.Normals;
			UVs = internalOject.UVs;
			Tangents = internalOject.Tangents;
			BiTangents = internalOject.BiTangents;
		}
	}
	
}
