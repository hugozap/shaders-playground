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

//vec3 hsbLines = hsb2rgb(vec3(37./255., 89./255., 89./255.));

void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;
	vec3 finalColor = hsb2rgb(vec3(89./255., 89./255., 89./255.));
	vec2 st = fract(uv*13.);
	vec2 it = floor(uv*13.);

	float r = frame(st*snoise(uv*30.));
	vec3 frameColor = hsb2rgb(vec3(50./255., 200./255., 150./255.));
	if(it.x > 8.) {
		frameColor = hsb2rgb(vec3(0.2, 0.2, 0.9));
		finalColor = hsb2rgb(vec3(100./255., 89./255., 200./255.));
		if(it.y < 8.) {
			frameColor = hsb2rgb(vec3(0.2, 0.2, 0.9))/2.;
			finalColor = hsb2rgb(vec3(3./255., 89./255., 255./255.));	
		}
	}
	float ns = snoise(it*abs(sin(u_time*0.02)));
	finalColor = finalColor * ns;
	finalColor  = mix(frameColor, finalColor, r);

	// Add texture
	float t = snoise(uv*200. + sin(u_time/20.));
	vec3 cloudColor = hsb2rgb(vec3(100./255., 89./255., 10./255.));
	finalColor  = mix(finalColor, cloudColor, t);

	// Circular frame
	float c = circle(uv, 1.,0.8);
	vec3 outercirclecolor = hsb2rgb(vec3(0.1, 0.5, 0.1));

	finalColor = mix(outercirclecolor, finalColor, c/5.);

	gl_FragColor = vec4(finalColor, 1.0);
}