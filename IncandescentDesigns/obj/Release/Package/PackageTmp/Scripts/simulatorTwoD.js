// Our Javascript will go here.
var scene = new THREE.Scene();
var camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);

var renderer = new THREE.WebGLRenderer({
    antialias: true,
    alpha: true
});
//renderer.setSize(window.innerWidth, window.innerHeight);
var WIDTH = document.getElementById("sim").clientWidth;
var HEIGHT = document.getElementById("sim").clientHeight;
renderer.setSize(WIDTH, HEIGHT);
var sim = document.getElementById("sim");
sim.appendChild(renderer.domElement);
//document.body.appendChild(renderer.domElement);
renderer.setClearColor(0xffffff, 1);
//renderer.setClearColor(0x000000, 0);

//clock object for keeping track of time
var timer = new THREE.Clock(true);
//hold the current time as of right now
var oldTime = timer.getElapsedTime();

var flip = 1;

var color;
var count = 0;

var geometry = new THREE.SphereGeometry(.1, .1, .1);

var sphereArray = [];

for (z = 0; z < 1; z++) {
    sphereArray[z] = [];
    for (x = 0; x < 8; x++) {
        sphereArray[z][x] = [];

        for (y = 0; y < 8; y++) {
            sphereArray[z][x][y] = [];
            sphereArray[z][x][y] = new THREE.Mesh(geometry, new THREE.MeshBasicMaterial({ color: 0xC4D0DB }))
            sphereArray[z][x][y].position.set(z / 2, x / 2, y / 2);
            scene.add(sphereArray[z][x][y]);
        }
    }
}




camera.position.x = 6;
// Add OrbitControls so that we can pan around with the mouse.
controls = new THREE.OrbitControls(camera, renderer.domElement);

function render() {
    requestAnimationFrame(render);

    //animate2();
    twoDimShapes("0xFF4500", 100, 100);

    renderer.render(scene, camera);
    controls.update();
}

function animate() {
    if ((count % 200) == 0) {
        color = !color;
    }

    for (z = 0; z < 1; z++) {
        for (x = 0; x < 8; x++) {
            for (y = 0; y < 8; y++) {
                if ((count % 25) == 0) {
                    randColors();
                    setPixelZXY(z, x, y, randomColor);
                    count = 0;
                }
                
                count = (count + 1);
            }
        }
    }
}

//generate a random RGB color
function randColors() {
    randomColor = '0x' + (Math.random() * 0xFFFFFF << 0).toString(16)
}

//randomly change colors of the cube at a standard interval
function animate2() {
    for (z = 0; z < 1; z++) {
        for (x = 0; x < 8; x++) {
            for (y = 0; y < 8; y++) {
                if ((count % 25) == 0) {
                    randColors();
                    setPixelZXY(z, x, y, randomColor);
                    count = 0;
                }

                count = (count + 1);
            }
        }
    }
}

//set the color of a specific pixel on the cube
function setPixelZXY(z, x, y, color) {
    sphereArray[z][y][x].material.color.setHex(color);
}


/****************************************/
/* CODE TO CREATE A PATTERN ON THE CUBE */
/****************************************/

//Make all of the cubes this color
//x is an int value representing the x-axis
//y is an int value representing the x-axis
//color is a hex value stored as a string, which is the color to set the cube to
function setSolidColor(color) {
} //not needed?

//allows a shape to be drawn on the LED cube
//parameters are color = int, speed = long, duration = string (a hex value)
function twoDimShapes(color, speed, duration) {

    newDuration = duration;

    while ((timer.getElapsedTime() - oldTime) < .25) {
        a = 5;
    }

    for (z = 0; z < 1; z++) {
        for (x = 0; x < 8; x++) {
            for (y = 0; y < 8; y++) {
                setPixelZXY(z, x, y, color);
            }
        }
    }

    //Clear the LEDs (make all blank)
        blankDisplay();

    if ((timer.getElapsedTime() - oldTime) > 1) {
        //if ((count % 25) == 0) {
        //draw a circle of radius 2 from the center point (4,4)
        drawCircle(4, 4, 2, color);
        //  count = 0;
        //}
        //count = (count + 1);
        oldTime = timer.getElapsedTime();
    }
}

//draw a basic circle on the simulator
//posX is an int, which is the x-axis
//posY is an int, which is the y-axis
//rad is an int, which the area around the radius to draw the circle
//color is a hex value stored as a string, which is the color to set the cube to
function drawCircle(posX, posY, rad, color) {
    a = -rad;
    b = 0;
    err = 2 - 2 * rad;
    e2 = 0;

    do {
        //setPixelZXY(0, 1, 1, color);
        setPixelZXY(0, posX - a, posY + b, color);
        setPixelZXY(0, posX + a, posY + b, color);
        setPixelZXY(0, posX + a, posY - b, color);
        setPixelZXY(0, posX - a, posY - b, color);
        e2 = err;
        if (e2 <= b) {
            err += ++b * 2 + 1;
            if (-a == b && e2 <= a) {
                e2 = 0;
            }
        }
        if (e2 > a) {
            err += ++a * 2 + 1;
        }
    }
    while (a <= 0);
}

//reset all of the cubes to regular color
function blankDisplay() {
    for (z = 0; z < 1; z++) {
        for (x = 0; x < 8; x++) {
            for (y = 0; y < 8; y++) {
                setPixelZXY(z, x, y, 0x000000);
            }
        }
    }
}

/* END CODE TO CREATE A PATTERN ON THE CUBE */
/********************************************/

//set the color of a cube based on button click
function setCol(cube, color) {
    var cube = scene.getChildByName(cube14);
    changeColor(cube45, material0);
    if (cube45.material.color.getHexString() == "000000") {
        changeColor(cube45, 0xFFF55F);

    } else {
        changeColor(cube45, material1);

    }
}
// functions to write
// changeColor(getCubeByID(), getColorCodeByID());

//function to change the color of the cube
function changeColor(cube, val) {
    cube.material.color.setHex(val);
}


render();

