uniform vec2 u_resolution;
uniform float u_time;

/* Usar rutina que usa sin para pintar lineas */


float ground(vec2 uv, float y) {
	return  (1. - smoothstep(y,y + 0.01, uv.y));
}


float sky(vec2 uv) {
	return snoise(uv*200.+u_time)-uv.y;
}

float planet(vec2 uv) {
	return  circle(uv-vec2(0.3,0.5),1., 0.1)+distance(uv, vec2(0.3,0.5));
}


float getLights(in vec2 uv) {
	float units = 100.;
	float buildingHeight = random(vec2(floor(uv.x*units), 1.))*0.4;
	vec2 uv2 = fract(uv*units);
	vec2 ix = floor(uv*units);
	if(ix.y == 20.) {
		return circle(uv2, 0.8*buildingHeight, 0.5);

	} else {
		return 0.;
	}
}

float getSubway(in vec2 uv) {
	uv.x = uv.x + u_time/80.;
	float units = 100.;
	float buildingHeight = random(vec2(floor(uv.x*units), 1.))*0.4;
	vec2 uv2 = fract(uv*units);
	vec2 ix = floor(uv*units);
	if(ix.y == 22.) {
		return snoise(uv2*90.)*box(uv2, vec2(0.9,0.9))+circle(uv2, 0.8*buildingHeight, 0.5);

	} else {
		return 0.;
	}
}


float calcBuilding(vec2 uv,float w, float h) {
	return box(uv, vec2(w, h));
}


float getStreet(in vec2 uv, float posy) {
	float w = 0.04;
	return (1. - smoothstep(posy, posy+w, uv.y)) -
		   (1. - smoothstep(posy-0.005, posy + w, uv.y));
}

float getHorizon(in vec2 uv, float posy) {
	float w = 0.001;
	return (1. - smoothstep(posy, posy+w, uv.y)) -
		   (1. - smoothstep(posy-0.005, posy + w, uv.y));
}



float city(in vec2 uv) {
	//Dividir el espacio en 100 partes
	//uv = uv - (0., 0.5);
	vec2 uv2 = vec2(fract(uv.x*100.), uv.y);
	float ix = fract(uv.x*100.);
	float buildingHeight = random(vec2(floor(uv.x*100.), 1.))*0.4;
	float buildingWidth = max(0.3, random(vec2(floor(uv.x*100.), 1.)));
	//En cada parte usar una altura diferente
	float b =  calcBuilding(uv2, buildingWidth, buildingHeight)*snoise(uv*300.);
	return b * step(0.2, uv.y) + getStreet(uv, 0.2);
}

float blur(float p) {
	return smoothstep(0., 1., p);
}


void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;
	vec2 origUv = uv;

	//uv = (2.0*gl_FragCoord.xy-u_resolution.xy)/min(u_resolution.y,u_resolution.x);
	uv = vec2(uv.x + u_time/100., uv.y);


	vec3 finalColor = vec3(0.1);
	vec3 planetColor = vec3(0.08);
	vec3 groundColor = vec3(0.1);
	vec3 skyColor = vec3(0.2);
	vec3 cityColor = vec3(0.8);
	vec3 reflectionColor = vec3(0.5);
	vec3 streetColor = vec3(1.0);

	float p = planet(origUv);
	float g = ground(uv, 0.2);
	float sk = sky(uv);
	float buildings = city(uv);
	float buildingReflection = city(vec2(uv.x, -uv.y+0.4 + sin(u_time)/1000.))/2.;
	float backBuildingds = city(uv * 0.9);
	float backBuildingds2 = city(uv * 0.8);
	float street = getStreet(uv, 0.2)+ buildings;
	float street2 = getStreet(uv, 0.25) + backBuildingds;
	float horizon = getHorizon(uv, 0.2);
	float lights = getLights(uv);
	float subway = getSubway(uv);

	finalColor = mix(finalColor, skyColor, sk);
	finalColor = mix(finalColor, planetColor, p);
	finalColor = mix(finalColor, groundColor, g);
	//finalColor = mix(finalColor, cityColor, subway);
	finalColor = mix(finalColor, cityColor/2.5, backBuildingds2/2.);
	finalColor = mix(finalColor, streetColor/2.5, street2);
	finalColor = mix(finalColor, cityColor/2., backBuildingds);
	// finalColor = mix(finalColor, streetColor, street);
	finalColor = mix(finalColor, cityColor, lights);
	finalColor = mix(finalColor, cityColor, buildings);
	finalColor = mix(finalColor, cityColor/3., horizon);
	finalColor = mix(finalColor, reflectionColor, buildingReflection);

	gl_FragColor = vec4(finalColor, 1.);
}