package graphics;

import openfl.display.Sprite;
import openfl.display.DisplayObject;

class Camera {
	private var container: DisplayObject;

	public function new(argContainer: DisplayObject) {
		container = argContainer;
	}

	public function moveToCoords(x: Float, y: Float) {
		var baseX: Float = -container.stage.stageWidth / 2;
		var baseY: Float = -container.stage.stageHeight / 2;

		container.scrollRect.x = baseX + x;
		container.scrollRect.y = baseY + y;
	}

	public function moveToObject(object: DisplayObject) {
		var baseX: Float = -container.stage.stageWidth / 2;
		var baseY: Float = -container.stage.stageHeight / 2;

		container.scrollRect.x = baseX + object.x;
		container.scrollRect.y = baseY + object.y;
	}

	public function zoom(scaleX: Float, scaleY: Float) {
		container.scaleX = scaleX;
		container.scaleY = scaleY;
	}
}

class Scene extends Sprite {
	public var camera: Camera;

	public function new() {
		super();
		camera = new Camera(this);
	}
}
