var canvas, stage, exportRoot;

function init() {
    canvas = document.getElementById("graphiccanvas");
    images = images || {};

    var manifest = [
		{ src: "images/_1.jpg", id: "_1" },
		{ src: "images/_2.jpg", id: "_2" },
		{ src: "images/_3.jpg", id: "_3" },
		{ src: "images/_4.jpg", id: "_4" },
		{ src: "images/_5.jpg", id: "_5" },
		{ src: "images/a.jpg", id: "a" },
		{ src: "images/b.jpg", id: "b" },
		{ src: "images/c.jpg", id: "c" },
		{ src: "images/d.jpg", id: "d" },
		{ src: "images/e.jpg", id: "e" }
	];

    var loader = new createjs.LoadQueue(false);
    loader.addEventListener("fileload", handleFileLoad);
    loader.addEventListener("complete", handleComplete);
    loader.loadManifest(manifest);
}

function handleFileLoad(evt) {
    if (evt.item.type == "image") { images[evt.item.id] = evt.result; }
}

function handleComplete() {
    //TODO generate for each JS object 
    exportRoot = new lib.srdce_real_hybajici2();

    stage = new createjs.Stage(canvas);
    stage.addChild(exportRoot);
    stage.update();
    stage.enableMouseOver();

    createjs.Ticker.setFPS(30);
    createjs.Ticker.addEventListener("tick", stage);
}

          