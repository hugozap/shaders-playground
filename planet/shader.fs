uniform vec2 u_resolution;
uniform float u_time;

/* Usar rutina que usa sin para pintar lineas */


float ground(vec2 uv, float y) {
	return  1. - smoothstep(y,y + 0.01, uv.y);
}


float sky(vec2 uv) {
	return snoise(uv*30.);
}

float planet(vec2 uv) {
	return  circle(uv-vec2(0.3,0.5),1., 0.1);
}



float calcBuilding(vec2 uv,float w, float h) {
	return box(uv, vec2(w, h));
}

float city(in vec2 uv) {
	//Dividir el espacio en 100 partes
	//uv = uv - (0., 0.5);
	vec2 uv2 = vec2(fract(uv.x*100.), uv.y);
	float ix = fract(uv.x*100.);
	float buildingHeight = random(vec2(floor(uv.x*100.), 1.))*0.4;
	//En cada parte usar una altura diferente
	float b =  calcBuilding(uv2, 0.8, buildingHeight);
	return b * step(0.2, uv.y);
}

float blur(float p) {
	return smoothstep(0., 1., p);
}


void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;

	//uv = (2.0*gl_FragCoord.xy-u_resolution.xy)/min(u_resolution.y,u_resolution.x);

	vec3 finalColor = vec3(0.1);
	vec3 planetColor = vec3(0., 1.,0.);
	vec3 groundColor = vec3(0.2, 0.2, 0.2);
	vec3 skyColor = vec3(0.1, 0.1, 0.3);
	vec3 cityColor = vec3(0.7);

	float p = planet(uv);
	float g = ground(uv, 0.2);
	float sk = sky(uv);
	float buildings = city(uv);
	float backBuildingds = city(uv * 0.9);

	finalColor = mix(finalColor, skyColor, sk);
	finalColor = mix(finalColor, planetColor, p);
	finalColor = mix(finalColor, groundColor, g);
	finalColor = mix(finalColor, cityColor/2., backBuildingds);
	finalColor = mix(finalColor, cityColor, buildings);

	gl_FragColor = vec4(finalColor, 1.);
}