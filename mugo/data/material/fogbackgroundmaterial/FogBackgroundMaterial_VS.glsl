#version 130
precision highp float;

// input aus der VAO-Datenstruktur
in vec3 in_position;
in vec3 in_normal; 
in vec2 in_uv;
in vec3 in_tangent;
in vec3 in_bitangent;

// "modelview_projection_matrix" wird als Parameter erwartet, vom Typ Matrix4
uniform mat4 modelview_projection_matrix;

// Die "Model-Matrix" wird als Parameter erwaretet, um die TBN-Matrix in den World-Space zu transformieren
uniform mat4 model_matrix;

// Kamera-Position wird �bergeben
uniform vec4 camera_position;

// "texcoord" wird an den Fragment-Shader weitergegeben, daher als "out" deklariert
out vec2 fragTexcoord;

// die Blickrichtung wird dem Fragment-Shader �bergeben
out vec4 fragV;

// die TBN-Matrix wird an den Fragment-Shader �bergeben
out mat3 fragTBN;

out vec4 viewPosition;

void main()
{
	vec3 T = normalize(vec3(model_matrix * vec4(in_tangent, 0.0)));
    vec3 B = normalize(vec3(model_matrix * vec4(in_bitangent, 0.0)));
    vec3 N = normalize(vec3(model_matrix * vec4(in_normal, 0.0)));
    fragTBN = mat3(T, B, N);

	// "in_uv" (Texturkoordinate) wird direkt an den Fragment-Shader weitergereicht.
	fragTexcoord = in_uv;

	// position �bergeben
	fragV = normalize(camera_position - model_matrix *  vec4(in_position,1));

	// in gl_Position die finalan Vertex-Position geschrieben ("modelview_projection_matrix" * "in_position")
	gl_Position = modelview_projection_matrix * vec4(in_position, 1);
	viewPosition = gl_Position;
}


