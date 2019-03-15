uniform vec2 u_resolution;
uniform float u_time;


float l1(in vec2 st){
  return circle(st*(1. - snoise(st)*2.), 0.5, 0.5);
}

vec3 layer1(in vec2 uv, vec3 currentColor) {
	float t = u_time * 0.05;
	for(float i=1.; i<4.; i+=1.) {
		vec3 color = hsb2rgb(vec3(52./360., 25.5/100., sin(i+u_time/10.)+cos(uv.y)));
		float contribution = l1(uv+snoise(uv+t/i));
		currentColor = mix(currentColor, color, contribution); 
	}
	return currentColor;
}

float noisy(vec2 st){
	return random(st)*10.;
}

void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;
	vec2 origuv = uv;
	vec3 finalColor = hsb2rgb(vec3(182./360., 27./100., 99./100.));
	vec3 frameColor = hsb2rgb(vec3(182./360., 27./100., 10./100.));
	finalColor = layer1(uv, finalColor);
	finalColor = layer1(uv*1.1, 1.-finalColor);
	finalColor = mix(finalColor, vec3(0.3), noisy(origuv+sin(u_time)));
	// finalColor = mix(frameColor, finalColor, circle(origuv, 0.8, 0.5));


	gl_FragColor = vec4(finalColor, 1.);
}