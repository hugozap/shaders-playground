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
	float val = sin(st.x*50. + snoise(st)*0.2);
	// if val is 1 draw line
	float f = step(0.99, val);
	return f;
}


float sketchylinesdistorted(vec2 st, float u_time) {
    float val = sin(st.x*100. + snoise(st)*0.2 );
    // if val is 1 draw line
    float f = smoothstep(0.99 * snoise(st)*3., 1., val);
    return f;
}

float grid(vec2 st, float u_time) {
	float v = sketchylinesdistorted(st, u_time);
	vec2 rotated = st * rotate2d(PI/2.);
	float h = sketchylinesdistorted(rotated, u_time);
	return h + v;
}

vec2 tile(vec2 st, float size) {
	return fract(st * size);
}


void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;
	vec2 uvorig = uv;
	uv = uv * rotate2d(PI/3.);
	uv = tile(uv * snoise(uv + u_time) , 40.);
	float g = grid(uv);
	float c =  twirllines(uv, u_time) * g;
	vec3 finalColor = vec3(snoise(uvorig)*3., snoise(uvorig)*5., snoise(uvorig)*2.)  * c;
	gl_FragColor = vec4(finalColor, 1.0);
}