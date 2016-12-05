#version 130
precision highp float;

// basis Textur und Normalmap
uniform sampler2D color_texture;
uniform sampler2D normalmap_texture;

// "model_matrix" Matrix wird als Parameter erwartet, vom Typ Matrix4
uniform mat4 model_matrix;

// Parameter f�r direktionales Licht
uniform vec3 light_direction;
uniform vec4 light_ambient_color;
uniform vec4 light_diffuse_color;
uniform vec4 light_specular_color;

// Parameter f�r die Specalar-Intensit�t "Shininess"
uniform float specular_shininess;

uniform vec3 cam_sub_position;

// Fog Start, Ende und Farbe
uniform float fogStart;
uniform float fogEnd;
uniform vec3 fogColor;

// input vom Vertex-Shader
in vec2 fragTexcoord;
in mat3 fragTBN;
in vec4 fragV;
in vec4 viewPosition;

uniform float counter;

// die finale Farbe
out vec4 outputColor;

void main()
{	
	vec2 fragTexcoord2 = vec2(fragTexcoord.x, fragTexcoord.y + counter * 0.03);

	// die Vertex-Normale berechnen
    vec3 normal = texture(normalmap_texture, fragTexcoord2).rgb;
	normal = normalize(normal * 2.0 - 1.0); 
	normal = normalize(fragTBN * normal); 

	// den half-vector berechnen
	vec3 h = normalize(light_direction + vec3(fragV));
	float ndoth = clamp(dot( normal, h ), 0, 100.0);
	float specularIntensity = pow(ndoth, specular_shininess);

	// die Helligkeit berechnen, resultierund aus dem Winkel 
	float brightness = clamp(dot(normalize(normal), light_direction), 0, 1);
	
	// surfaceColor ist die farbe aus der Textur...
	vec4 surfaceColor = texture(color_texture, fragTexcoord2);

	//				 Ambiente Farbe						  + Diffuser Farbanteil								  + Speculares Licht
    // outputColor = (surfaceColor * light_ambient_color) + (surfaceColor * brightness * light_diffuse_color) + specularIntensity * light_specular_color;
	// obere Zeile surfaceColor ausgeklammert
	outputColor = surfaceColor * (light_ambient_color +  brightness * light_diffuse_color) + specularIntensity * light_specular_color;

	float fogFactor = (fogEnd - distance(viewPosition.xyz, cam_sub_position)) / (fogEnd - fogStart);
	fogFactor = clamp(fogFactor, 0, 1);
	outputColor = fogFactor * outputColor + ((1 - fogFactor) * vec4(fogColor, 1));
}