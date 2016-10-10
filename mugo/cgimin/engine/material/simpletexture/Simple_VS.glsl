varying vec2 coord; 

void main()
{
    coord = vec2(gl_MultiTexCoord0);
	gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
}


