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
	//dividir espacio en 10
	float d = distance(uv, vec2(0.5, 0.5));
	uv += u_time/10.;
	//entre más cerca del centro más fuerte la distorsión
	uv = tile(uv + snoise(uv)*d, 10.);
	float l = max(smoothstep(0., 0.1, uv.x) +
		smoothstep(0., 0.1, uv.y), 1.);
	vec3 finalColor = vec3(1., 0.5, 0.1) * l;
	gl_FragColor = vec4(finalColor, 1.0);
}