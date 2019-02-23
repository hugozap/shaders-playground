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



float rect(vec2 st, float w) {
	return smoothstep(0.,w/2.,st.x) * smoothstep(1. - w, w/2., st.x);
}

void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;
	//colors
	vec3 bcolor = vec3(0.,1., 1.);
	vec3 rcolor = vec3(1., 0., 0.);
	vec3 fcolor = vec3(1.,1.,1.);

	//1. Grid base
	vec2 st = fract(uv*1.);
	float r = rect(st, 0.01);
	vec3 c = mix(bcolor, rcolor, r);

	//2. Cover with frame
	float m = 1. - rect(st, 0.2);
	c = mix(c, fcolor, m);
	gl_FragColor = vec4(c, 1.0);
}