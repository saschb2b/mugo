using Engine.cgimin.object3d;
using OpenTK;

namespace Mugo
{
	class ClonedObject<T> : BaseObject3D
		where T : BaseObject3D, new()
	{
		protected static readonly T internalObject = new T();

		public Matrix4 DefaultTransformation { get; protected set; }

		public ClonedObject()
		{
			DefaultTransformation = internalObject.Transformation;
			Transformation = internalObject.Transformation;
			Vao = internalObject.Vao;
			Indices = internalObject.Indices;
			Positions = internalObject.Positions;
			Normals = internalObject.Normals;
			UVs = internalObject.UVs;
			Tangents = internalObject.Tangents;
			BiTangents = internalObject.BiTangents;
		}
	}
	
}
