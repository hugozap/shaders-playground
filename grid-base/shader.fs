uniform vec2 u_resolution;
uniform float u_time;


float lines(vec2 st, float thick) {
	float val = sin(st.x*100.);
	// if val is 1 draw line
	return smoothstep(1. - thick, 1., val);
}

float grid(vec2 st, float thick) {
	float lh = lines(st, thick);
	float lv = lines(st * rotate2d(PI/2.), thick)
	return lh + lv;)
}

void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;
	float l = lines(uv, 0.01);
	vec3 finalColor = vec3(1.,1.,1.) * (1. - l);
	gl_FragColor = vec4(finalColor, 1.0);
}