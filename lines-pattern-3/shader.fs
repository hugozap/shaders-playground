uniform vec2 u_resolution;
uniform float u_time;

/* Usar rutina que usa sin para pintar lineas */

float lines(vec2 st, float thick) {
	float val = sin(st.x*150.);
	// if val is 1 draw line
	return smoothstep(1. - thick, 1., val);
}

float grid(vec2 st, float thick) {
	float lh = lines(st, thick);
	float lv = lines(st * rotate2d(PI/2.), thick);
	return lv + lh;
}


vec3 mixLines(vec2 st, vec3 currentColor, float u_time) {
	vec3 hsbLines = hsb2rgb(vec3(32./360., 98./100., 80./100.));
	vec3 white = vec3(1);
	float chaos = -1.;
	chaos = snoise(st);

	if(st.y > 0.5)
    {
    	//chaos aumenta con st.y
    	float factor = pow(smoothstep(0.5, 1., st.y),1.5);
    	float offset = sin(u_time);
		st = st + factor * (snoise(st*offset));
	}

	st = st+random(st)/100.;
	float l = lines(st, 3.);
	float li = lines(st, 0.5);

	currentColor = mix(currentColor, hsbLines, l);
	currentColor = mix(currentColor, white, li);
	return currentColor;
}


void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;
	vec3 finalColor = vec3(0.);
	vec3 frameColor = vec3(0.1)*random(uv);	
	vec3 white = vec3(1);
	//TODO: optimizar, quitar conversion, precalcular.
	vec3 hsbLines = hsb2rgb(vec3(32./360., 98./100., 80./100.));

	//Inspired in the vitalic flash mob cover.
	/* Process
	- pintar lineas
	- despues de 0.5 empieza a aumentar el factor distorsion
	*/
	finalColor = mixLines(uv, finalColor, 0.5);
	finalColor = mixLines(uv, finalColor, 0.8*snoise(uv));
	finalColor = mix(frameColor,finalColor, circle(uv,0.5, 0.8));

	gl_FragColor = vec4(finalColor, 1.);
}