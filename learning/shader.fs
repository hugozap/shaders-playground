uniform vec2 u_resolution;
uniform float u_time;


/* Conceptos:
-  Mostrar una curva de bezier
- Marco circular 
- mix de colores
*/

void main() {
	vec2 uv=gl_FragCoord.xy / u_resolution.xy;
	float pct = 0.1;
	vec3 finalColor = vec3(1);

	vec2 p0 = vec2(0.0, 0.5);
	vec2 p1 = vec2(0.3, 0.4);
	vec2 p2 = vec2(0.8, 0.7);
	vec2 p3 = vec2(0.9, 0.5);


	//Get the bezier point using current x as t
	vec2 bpoint = bezier(uv.x, p0, p1, p2, p3 );
    vec3 curvecolor = vec3(1, 0.3, 0.4) * plot2(uv, bpoint.y, 0.2+sin(u_time)) ;
    float c2 = circle(uv, 0.5, 0.3);
    float c3 = circle(uv, 0.3, 0.3);

	finalColor = finalColor * (c2 - c3) * random(uv+sin(u_time));
	finalColor = mix(finalColor , curvecolor, abs(sin(u_time/2.)/3.));





gl_FragColor = vec4(finalColor, 1.0);
}