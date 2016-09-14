// Our Javascript will go here.
var scene = new THREE.Scene();
var camera = new THREE.PerspectiveCamera(30, (window.innerWidth/2) / (window.innerHeight/2), 0.1, 1000);

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
renderer.setClearColor(0x151515, 1);


//clock object for keeping track of time
var timer = new THREE.Clock(true);
//hold the current time as of right now
var oldTime = 0;
var newTime = timer.getElapsedTime();
var randomColor;
var color;
var count = 0;

var geometry = new THREE.SphereGeometry(.1, .1, .1);

var sphereArray = [];

for (z = -2; z < 2; z++) {
    sphereArray[z] = [];
    for (x = -2; x < 2; x++) {
        sphereArray[z][x] = [];

        for (y = -2; y < 2; y++) {
            sphereArray[z][x][y] = [];
            sphereArray[z][x][y] = new THREE.Mesh(geometry, new THREE.MeshBasicMaterial({ color: 0xC4D0DB }))
            sphereArray[z][x][y].position.set(z / 2, x / 2, y / 2);
            scene.add(sphereArray[z][x][y]);
        }
    }
}




camera.position.z = 5;
camera.position.x = 2;
camera.position.y = 2;
// Add OrbitControls so that we can pan around with the mouse.
controls = new THREE.OrbitControls(camera, renderer.domElement);

function render() {
    requestAnimationFrame(render);

    animator(pattern);

    
    controls.update();
}

function animator(pattern) {
    switch (pattern) {
        case "random":
            random();
            break;
        case "flash":
            flash();
            break;
            //case value2:
            //    //Statements executed when the result of expression matches value2
            //        [break;]
            //...
            //case valueN:
            //    //Statements executed when the result of expression matches valueN
            //        [break;]
        default:
            animate();
            break;
    }
}


function animate() {
    if ((count % 20) == 0) {
        color = !color;
    }

    for (z = -2; z < 2; z++) {
        for (x = -2; x < 2; x++) {
            for (y = -2; y < 2; y++) {
                if (color) {
                    setPixelZXY(z, x, y, 0x800080);
                    count = (count + 1);
                    newTime = timer.getElapsedTime();
                    renderer.render(scene, camera);
                }
                else {
                    setPixelZXY(z, x, y, 0xC4D0DB)
                    count = (count + 1);
                    newTime = timer.getElapsedTime();
                    renderer.render(scene, camera);
                }
            }
        }
    }
}


function random() {
    if (count % 20 == 0) {
        for (z = -2; z < 2; z++) {
            for (x = -2; x < 2; x++) {
                for (y = -2; y < 2; y++) {

                    setPixelZXY(z, x, y, randomColor);
                    count = 0;

                    randColors();

                    newTime = timer.getElapsedTime();
                    renderer.render(scene, camera);
                    count = count + 1;
                }
            }
        }
    }
    else {
        count = count + 1;
    }
}

function flash() {
        if ((count % 22) == 0) {
            color = !color;
        }

        for (z = -2; z < 2; z++) {
            for (x = -2; x < 2; x++) {
                for (y = -2; y < 2; y++) {
                    if (color) {
                        setPixelZXY(z, x, y, 0x800080);
                        count = (count + 1);
                        newTime = timer.getElapsedTime();
                        renderer.render(scene, camera);
                    }
                    else {
                        setPixelZXY(z, x, y, 0x64D0AB)
                        count = (count + 1);
                        newTime = timer.getElapsedTime();
                        renderer.render(scene, camera);
                    }
                }
            }
        }

}


function random1() {
    if ((newTime - oldTime) < 5) {
        for (z = -2; z < 2; z++) {
            for (x = -2; x < 2; x++) {
                for (y = -2; y < 2; y++) {

                    setPixelZXY(z, x, y, randomColor);
                    count = 0;

                    randColors();

                    newTime = timer.getElapsedTime();
                    renderer.render(scene, camera);
                }
            }
        }
    }
    else {
        if ((count % 22) == 0) {
            color = !color;
        }

        for (z = -2; z < 2; z++) {
            for (x = -2; x < 2; x++) {
                for (y = -2; y < 2; y++) {
                    if (color) {
                        setPixelZXY(z, x, y, 0x800080);
                        count = (count + 1);
                        newTime = timer.getElapsedTime();
                        renderer.render(scene, camera);
                    }
                    else {
                        setPixelZXY(z, x, y, 0xC4D0DB)
                        count = (count + 1);
                        newTime = timer.getElapsedTime();
                        renderer.render(scene, camera);
                    }
                }
            }
        }

    }

}


function randColors() {
    randomColor = '0x' + (Math.random() * 0xFFFFFF << 0).toString(16)
}


function setPixelZXY(z, x, y, color) {
    sphereArray[z][y][x].material.color.setHex(color);
}



function setCol(cube, color) {
    var cube = scene.getChildByName(cube14);
    changeColor(cube45, material0);
    if (cube45.material.color.getHexString() == "000000") {
        changeColor(cube45, 0xFFF55F);

    } else {
        changeColor(cube45, material1);

    }

    // functions to write
    // changeColor(getCubeByID(), getColorCodeByID());

}
render();
