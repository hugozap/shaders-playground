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
	vec3 finalColor = vec3(232./255., 226./255., 219./255.);
	vec2 st = fract(uv*13.);
	vec2 it = floor(uv*13.);

	float r = smoothframe(st + snoise(uv*5.+u_time/25.)/10.)/uv.y;
	vec3 frameColor = 1. - vec3(232./255., 226./255., 219./255.);
	if(it.x > 8.) {
		frameColor = frameColor*0.4;
		finalColor = vec3(26./255., 50./255., 99./255.);
		if(it.y < 8.) {
			frameColor = frameColor* 0.3;
			finalColor = 1. - vec3(26./255., 50./255., 99./255.);	
		}
	}
	float ns = snoise(it*abs(sin(u_time*0.02)));
	finalColor = finalColor * ns;
	finalColor  = mix(frameColor, finalColor, r);

	// Add lines
	float l = sketchylines(st);
	vec3 linesColor = 1. - vec3(232./255., 226./255., 219./255.);
	finalColor = mix(finalColor, linesColor, l);

	// Add texture
	float t = snoise(uv*2. + sin(u_time/20.));
	vec3 cloudColor = frameColor/4.;
	finalColor  = mix(finalColor, cloudColor, t/5.);

	// Circular frame
	float c = circle(uv+snoise(uv*5.+ u_time/50.), 0.5,0.8);
	vec3 outercirclecolor = hsb2rgb(vec3(0.6, 0.5, 0.2));

	finalColor = mix(outercirclecolor, finalColor, 1. -c/2.);

	gl_FragColor = vec4(finalColor, 1.0);
}