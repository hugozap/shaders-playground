
float plot(vec2 st, float pct) {
	return smoothstep(pct-0.02, pct, st.y) -
		   smoothstep(pct, pct + 0.02, st.y);

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


float circle(vec2 st, vec2 center, float r) {
	float d = distance(st, center);
	return d < r ? 1. : 0.;
}

vec2 bezier(in float t, vec2 p0, vec2 p1, vec2 p2, vec2 p3) {

	//Get the position (x,y) for t (where t goes from 0 to 1)
	//using the bezier parametric formula
	return pow(1.0 - t, 3.) * p0
			  +  3. * pow(1.0-t, 2.)*t*p1
			  + 3. * (1. - t) * pow(t, 2.)*p2
			  + pow(t, 3.)*p3;
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