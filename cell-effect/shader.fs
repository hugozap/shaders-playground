uniform vec2 u_resolution;
uniform float u_time;


vec2 tile(vec2 st, float size) {
	st = st * size;
	st = fract(st);
	return st;
}

float grid(vec2 uv) {
	float l = max(smoothstep(0., 0.1, uv.x) +
		smoothstep(0., 0.1, uv.y), 1.);
	return l;
}

float box(vec2 _st, vec2 _size, float _smoothEdges){
    _size = vec2(0.5)-_size*0.5;
    vec2 aa = vec2(_smoothEdges*0.5);
    vec2 uv = smoothstep(_size,_size+aa,_st);
    uv *= smoothstep(_size,_size+aa,vec2(1.0)-_st);
    return uv.x*uv.y;
}

void main() {
	vec2 uv= gl_FragCoord.xy / u_resolution.xy;
	vec2 orig = uv;

	uv = uv * rotate2d(PI);
	//dividir espacio en 10
	float d = distance(uv, vec2(0.5, 0.5));

	uv += u_time/45.;
	//entre más cerca del centro más fuerte la distorsión
	uv = tile(uv + snoise(uv)*d/3., 10.);
	float l = max(smoothstep(0., 0.1, uv.x) +
		smoothstep(0., 0.1, uv.y), 1.);

	float c = circle(orig, 0.5 + snoise(orig)/8., 0.02);
	float c2 = circle(orig, 0.55, 0.4);

	float cc = circle(orig + 0.5, 0.15 + snoise(orig)/5., 0.02);
	float cc2 = circle(orig + 0.5, 0.15, 0.4);
	float cframe = cc2- cc;

	float frame = c2 - c;
	float content = c ;
	vec3 frameColor = vec3(255., 157., 118.)/255.;

	vec3 finalColor = cframe + (frame * frameColor)
	  + vec3(0.8*sin(uv.x)*sin(uv.y),
	         0.24 * (sin(uv.y) + sin(uv.x)),
	        sin(uv.x) + cos(uv.y)) * content;
	vec3 fc = mix(snoise(orig*100.)+finalColor, finalColor, 0.9);
	gl_FragColor = vec4( fc, 1.0);
}