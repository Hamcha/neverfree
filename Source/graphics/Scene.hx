package graphics;

import openfl.geom.Rectangle;
import openfl.display.Sprite;
import openfl.display.DisplayObject;

class Camera extends Rectangle {
	private var container: DisplayObject;

	public function new(argContainer: DisplayObject){
		super(0, 0, Screen.width, Screen.height);
		container = argContainer;
	}

	public function moveToCoords(x: Float, y: Float) {
		var baseX: Float = -width * 0.5;
		var baseY: Float = -height * 0.5;

		this.x = baseX + x;
		this.y = baseY + y;

		container.x = -this.x;
		container.y = -this.y;
	}

	public function moveToObject(object: DisplayObject) {
		var baseX: Float = -width * 0.5;
		var baseY: Float = -height * 0.5;

		this.x = baseX + object.x;
		this.y = baseY + object.y;

		container.x = -this.x;
		container.y = -this.y;
	}

	public function zoom(scaleX: Float, scaleY: Float) {
		container.scaleX = scaleX;
		container.scaleY = scaleY;

		width = Screen.width / scaleX;
		height = Screen.height / scaleY;
	}
}

class Scene extends Sprite {
	public var camera: Camera;

	public function new() {
		super();
		camera = new Camera(this);
	}
}
