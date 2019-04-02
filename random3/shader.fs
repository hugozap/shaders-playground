uniform vec2 u_resolution;
uniform float u_time;


void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;
	//usar impulso para variar
	vec2 nuv = fract(uv*10.);
	float y = impulse(13.0+sin(u_time), nuv.x);
	float c = circle(nuv, y, 0.001);
	vec3 circlecol = vec3(1.0);
	vec3 finalColor = vec3(0., 1., 0.);
	finalColor = mix(finalColor, circlecol, c);
	gl_FragColor = vec4(finalColor, 1.);
}