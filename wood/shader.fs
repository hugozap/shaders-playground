uniform vec2 u_resolution;
uniform float u_time;


float woodframe(vec2 st) {
	return snoise(st* 45.);
}

float woodframe2(vec2 st) {
	float v1 = woodframe(vec2(st.x - 0.01, st.y));
	float v2 = woodframe(vec2(st.x, st.y));
	return (v1 + v2) / 2.;
}

float wood(vec2 st) {
	return snoise(st*100.);
}

float lines(vec2 st) {
	float val = sin(st.x*50.* snoise(st*3.0)+u_time);
	// if val is 1 draw line
	float f = step(0.99, val);
	return f;
}

void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;
	float fframe = 1. - frame(uv);
	vec3 woodframecolor = woodframe2(uv) * vec3(1, 1, 0);

	//content area
	vec3 baseWoodColor = vec3(183./255., 160./255., 136./255.);
	float content = lines(uv);
	vec3 contentColor = mix( 1. - content * vec3(1,1.,1.), baseWoodColor, 0.5);
	vec3 finalColor;

	// Frame filter
	// if(fframe == 1.) {
	// 	finalColor = woodframecolor;
	// } else {
	// 	finalColor = contentColor;
	// }

	// if fframe is 1 pass the frame color, otherwise pass the contentcolor
	finalColor = fframe * woodframecolor + (1. - fframe) * contentColor;
	gl_FragColor = vec4(finalColor, 1.0);
}