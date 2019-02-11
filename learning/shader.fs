uniform vec2 u_resolution;
uniform float u_time;



void main() {
	vec2 uv=gl_FragCoord.xy / u_resolution.xy;
	float pct = 0.1;

	vec2 p0 = vec2(0.0, 0.0);
	vec2 p1 = vec2(0.3, 0.8);
	vec2 p2 = vec2(0.7, 0.6);
	vec2 p3 = vec2(0.5, 0.3);

	//map uv.x to t so we have the real t for the current uv.x
	float t = map(uv.x, 0., 1., p0.x, p3.x);

	//only take into account if t in the 0 - 1
	float validT = 1.;

	if( t < 0. || t > 1.) {
		validT = 0.;
	}

	//Get the bezier point using current x as t
	vec2 bpoint = bezier(t, p0, p1, p2, p3 );
	vec3 color = vec3(0.5, 0., 0.) * plot(uv, bpoint.y);


	gl_FragColor = vec4(color, 1.0);
}