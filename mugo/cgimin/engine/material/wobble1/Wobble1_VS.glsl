varying vec2 coord; 

void main()
{
    coord = vec2(gl_MultiTexCoord0);

	vec4 v = vec4(gl_Vertex);
	v.z = v.z + sin(5.0*v.x)*0.5;
	gl_Position = gl_ModelViewProjectionMatrix * v;
}





