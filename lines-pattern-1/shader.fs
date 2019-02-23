uniform vec2 u_resolution;
uniform float u_time;

/* Usar rutina que usa sin para pintar lineas */

float lines(vec2 st, float thick) {
	float val = sin(st.x*100.);
	// if val is 1 draw line
	return smoothstep(1. - thick, 1., val);
}

float grid(vec2 st, float thick) {
	float lh = lines(st, thick);
	float lv = lines(st * rotate2d(PI/2.), thick);
	return lv + lh;
}


void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;
	vec3 finalColor = vec3(0.);
	//TODO: optimizar, quitar conversion, precalcular.
	vec3 hsbLines = hsb2rgb(vec3(37./255., 89./255., 89./255.));

	//Inspired in the vitalic flash mob cover.
	/* Process
	- pintar lineas
	- despues de 0.5 empieza a aumentar el factor distorsion
	*/

	float l = lines(uv, 20.);
	finalColor = mix(finalColor, hsbLines, l);

	gl_FragColor = vec4(finalColor, 1.0);
}