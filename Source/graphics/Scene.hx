package graphics;

import openfl.display.Sprite;
import openfl.display.DisplayObject;

class Camera {
	private var container: DisplayObject;

	public function new(argContainer: DisplayObject) {
		container = argContainer;
	}

	public function moveToCoords(x: Float, y: Float) {
		container.scrollRect.x = x;
		container.scrollRect.y = y;
	}

	public function moveToObject(object: DisplayObject) {
		container.scrollRect.x = object.x;
		container.scrollRect.y = object.y;
	}
}

class Scene extends Sprite {
	public var camera: Camera;

	public function new() {
		super();
		camera = new Camera(this);
	}
}
