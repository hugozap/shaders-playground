uniform vec2 u_resolution;
uniform float u_time;
void main() {
	vec2 st=gl_FragCoord.xy / u_resolution.xy;
	st = sin(u_time) * st;
	gl_FragColor = vec4(st.x, st.y, 0.0, 1.0);
}