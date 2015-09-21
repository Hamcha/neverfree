package graphics;

import openfl.geom.Rectangle;
import openfl.display.Sprite;
import openfl.display.DisplayObject;

class Camera  extends Rectangle {
	private var container: DisplayObject;

	public function new(argContainer: DisplayObject){
		super(0, 0, Screen.width, Screen.height);
		container = argContainer;

		container.scrollRect = this;
	}

	public function moveToCoords(x: Float, y: Float) {
		var baseX: Float = -width * 0.5 / container.scaleX;
		var baseY: Float = -height * 0.5 / container.scaleY;

		this.x = baseX + x;
		this.y = baseY + y;

#if flash
		container.scrollRect = this;
#end
	}

	public function moveToObject(object: DisplayObject) {
		var baseX: Float = -width * 0.5 / container.scaleX;
		var baseY: Float = -height * 0.5 / container.scaleY;

		this.x = baseX + object.x;
		this.y = baseY + object.y;

#if flash
		container.scrollRect = this;
#end
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
