
#define PI 3.14159265359

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


// Some useful functions
vec3 mod289(vec3 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
vec2 mod289(vec2 x) { return x - floor(x * (1.0 / 289.0)) * 289.0; }
vec3 permute(vec3 x) { return mod289(((x*34.0)+1.0)*x); }

//
// Description : GLSL 2D simplex noise function
//      Author : Ian McEwan, Ashima Arts
//  Maintainer : ijm
//     Lastmod : 20110822 (ijm)
//     License :
//  Copyright (C) 2011 Ashima Arts. All rights reserved.
//  Distributed under the MIT License. See LICENSE file.
//  https://github.com/ashima/webgl-noise
//
float snoise(vec2 v) {

    // Precompute values for skewed triangular grid
    const vec4 C = vec4(0.211324865405187,
                        // (3.0-sqrt(3.0))/6.0
                        0.366025403784439,
                        // 0.5*(sqrt(3.0)-1.0)
                        -0.577350269189626,
                        // -1.0 + 2.0 * C.x
                        0.024390243902439);
                        // 1.0 / 41.0

    // First corner (x0)
    vec2 i  = floor(v + dot(v, C.yy));
    vec2 x0 = v - i + dot(i, C.xx);

    // Other two corners (x1, x2)
    vec2 i1 = vec2(0.0);
    i1 = (x0.x > x0.y)? vec2(1.0, 0.0):vec2(0.0, 1.0);
    vec2 x1 = x0.xy + C.xx - i1;
    vec2 x2 = x0.xy + C.zz;

    // Do some permutations to avoid
    // truncation effects in permutation
    i = mod289(i);
    vec3 p = permute(
            permute( i.y + vec3(0.0, i1.y, 1.0))
                + i.x + vec3(0.0, i1.x, 1.0 ));

    vec3 m = max(0.5 - vec3(
                        dot(x0,x0),
                        dot(x1,x1),
                        dot(x2,x2)
                        ), 0.0);

    m = m*m ;
    m = m*m ;

    // Gradients:
    //  41 pts uniformly over a line, mapped onto a diamond
    //  The ring size 17*17 = 289 is close to a multiple
    //      of 41 (41*7 = 287)

    vec3 x = 2.0 * fract(p * C.www) - 1.0;
    vec3 h = abs(x) - 0.5;
    vec3 ox = floor(x + 0.5);
    vec3 a0 = x - ox;

    // Normalise gradients implicitly by scaling m
    // Approximation of: m *= inversesqrt(a0*a0 + h*h);
    m *= 1.79284291400159 - 0.85373472095314 * (a0*a0+h*h);

    // Compute final noise value at P
    vec3 g = vec3(0.0);
    g.x  = a0.x  * x0.x  + h.x  * x0.y;
    g.yz = a0.yz * vec2(x1.x,x2.x) + h.yz * vec2(x1.y,x2.y);
    return 130.0 * dot(m, g);
}


// Shapes

float frame(vec2 st) {
	// bottom-left
    vec2 bl = step(vec2(0.1),st);
    float pct = bl.x * bl.y;

    // top-right
    vec2 tr = step(vec2(0.1),1.0-st);
    pct *= tr.x * tr.y;
    return pct;
}

float sdBox( in vec2 p, in vec2 b )
{
    vec2 d = abs(p)-b;
    return length(max(d,vec2(0))) + min(max(d.x,d.y),0.0);
}

float box(in vec2 p, in vec2 b) {
    return (1. - step(b.x, p.x))
    * (1. - step(b.y, p.y));
}

float smoothframe(vec2 st) {
    // bottom-left
    vec2 bl = smoothstep(0., 0.1, st);
    float pct = bl.x * bl.y;

    // top-right
    vec2 tr = smoothstep(0., 0.1, 1.0-st);
    pct *= tr.x * tr.y;
    return pct;
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

float horizontallines(vec2 st) {
    float val = sin(st.x*50.* snoise(st*3.0));
    // if val is 1 draw line
    float f = step(0.99, val);
    return f;
}

mat2 rotate2d(float _angle){
    return mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle));
}

float sketchylines(vec2 st) {
    float val = sin(st.x*50. + snoise(st)*0.2);
    // if val is 1 draw line
    float f = smoothstep(0.9, 1., val);
    return f;
}


float twirllines(vec2 st, float u_time) {
    float val = sin(st.x*50.* snoise(st)* 3.);
    // if val is 1 draw line
    float f = smoothstep(0.99 * sin(u_time), 1. , val);
    return f;
}


/* Color utils */

//  Function from Iñigo Quiles
//  https://www.shadertoy.com/view/MsS3Wc
vec3 hsb2rgb( in vec3 c ){
    vec3 rgb = clamp(abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),
                             6.0)-3.0)-1.0,
                     0.0,
                     1.0 );
    rgb = rgb*rgb*(3.0-2.0*rgb);
    return c.z * mix(vec3(1.0), rgb, c.y);
}


//  Function from Iñigo Quiles
//  www.iquilezles.org/www/articles/functions/functions.htm
float impulse( float k, float x ){
    float h = k*x;
    return h*exp(1.0-h);
}

