
float plot(vec2 st, float pct) {
	return smoothstep(pct-0.02, pct, st.y) -
		   smoothstep(pct, pct + 0.02, st.y);

}

float plot2(vec2 st, float pct, float thick) {
	return smoothstep(pct-thick, pct, st.y) -
		   smoothstep(pct, pct + thick, st.y);

}

/* Maps a value from one scale to another */
float map(float v, float s1, float s2, float t1, float t2) {
	//get the percentage for the first interval
	float l = (s2 - s1); //length of the first interval
	float nv = v - s1; // translate value to 0
	float factor = nv / l; // Get the % for the value in the first interval
	float l2 = (t2 - t1); //length of the second interval
	return t1 + l2 * factor; // calculate point in 2 interval for the same factor.
}

float random (vec2 st) {
    return fract(sin(dot(st.xy,
                         vec2(12.9898,78.233)))*
        43758.5453123);
}



float circle(vec2 st, vec2 center, float r) {
	float d = distance(st, center);
	return 1. - smoothstep(0., r, d);
	//return d < r ? 1. : 0.;
}


float circle(in vec2 _st, in float _radius){
    vec2 dist = _st-vec2(0.5);
	return 1.-smoothstep(_radius-(_radius*0.01),
                         _radius+(_radius*0.01),
                         dot(dist,dist)*4.0);
}

float circle(in vec2 _st, in float _radius, float diffuse){
    vec2 dist = _st-vec2(0.5);
	return 1.-smoothstep(_radius-(_radius*diffuse),
                         _radius+(_radius*diffuse),
                         dot(dist,dist)*4.0);
}

vec2 bezier(in float t, vec2 p0, vec2 p1, vec2 p2, vec2 p3) {

	//Get the position (x,y) for t (where t goes from 0 to 1)
	//using the bezier parametric formula
	return pow(1.0 - t, 3.) * p0
			  +  3. * pow(1.0-t, 2.)*t*p1
			  + 3. * (1. - t) * pow(t, 2.)*p2
			  + pow(t, 3.)*p3;
}

vec4 toBezier(float delta, int i, vec4 P0, vec4 P1, vec4 P2, vec4 P3)
{
    float t = delta * float(i);
    float t2 = t * t;
    float one_minus_t = 1.0 - t;
    float one_minus_t2 = one_minus_t * one_minus_t;
    return (P0 * one_minus_t2 * one_minus_t + P1 * 3.0 * t * one_minus_t2 + P2 * 3.0 * t2 * one_minus_t + P3 * t2 * t);
}

// float bezier(vec2 st, vec2 p0, vec2 p1, vec2 p2, vec2 p3) {

// 	// calculrar el t equivalente para la posicion
// 	float t = map(st.x, 0., 1., p0.x, p3.x);

// 	// si estamos fuera
// 	if ( t < 0 || t > 1) {
// 		return 0;
// 	}

// 	vec2 b = pow(1.0 - t, 3.) * p0
// 			  +  3. * pow(1.0-t, 2.)*t*p1
// 			  + 3. * (1. - t) * pow(t, 2.)*p2
// 			  + pow(t, 3.)*p3;


// }