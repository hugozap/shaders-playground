uniform vec2 u_resolution;
uniform float u_time;


void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;
	vec2 origuv = uv;
	vec3 finalColor = vec3(1., 0. ,0.);
	vec3 fgColor = vec3(0., .2, 0.);
	vec3 linesColor = vec3(0., 0., 0.2);
	/* utilizar smoothstep para determinar el factor
	   de variación */

	float d = distance(vec2(0.8), uv);
	vec2 luv = fract(uv*2.*d/max(0.001, random(uv*sin(u_time))));
	float l = 1. - smoothstep(0., 0.3, d);

	vec2 distortedSpace = luv;
	float lines = smoothstep(0.,0.5, distortedSpace.x);
	finalColor = mix(finalColor, linesColor, lines);
    //finalColor = mix(finalColor, fgColor, l);

	gl_FragColor = vec4(finalColor, 1.);
}