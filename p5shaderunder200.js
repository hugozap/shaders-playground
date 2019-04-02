
var p,setu,stu='uniform',fu='void main(){';
function setup() {
 pixelDensity(1);
 createCanvas(windowWidth, windowHeight,WEBGL);
 gl=this.canvas.getContext('webgl');
  noStroke();
  fill(1);
  p = createShader(v,f);
  su = p.setUniform;
}

function draw() {
  shader(p);
  setu('r',[width,height]);
  setu('t',millis()/20);
  rect(0,0,width,height);
}

var v=`
attribute vec3 aPosition;
${fu}
gl_Position = vec4(aPosition, 1.0 );
    }
`;
var f=`
precision highp float;
${stu} vec2 r;
${stu} float t;
${fu}
    vec2 p = gl_FragCoord.xy / r.xy;
    gl_FragColor = vec4(1.0, 0.5, 0.2,1.0);
}`
