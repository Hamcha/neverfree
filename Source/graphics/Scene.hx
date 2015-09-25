package graphics;

import openfl.display.Sprite;

class Scene extends Sprite {
	public var camera: Camera;

	public function new() {
		super();
		camera = new Camera(this);
	}
}
