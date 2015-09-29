package graphics;

import openfl.geom.Point;
import openfl.geom.Matrix;
import openfl.geom.Rectangle;
import openfl.events.Event;
import openfl.display.Sprite;
import openfl.Lib;
import openfl.display.BitmapData;
import openfl.display.Tilesheet;

class SpriteAnimation {
	public var frames: Array<Int>;
	public var speed: Float;

	public function new(argFrames: Array<Int>, argSpeed: Float) {
		frames = argFrames;
		speed = argSpeed;
	}
}

class AnimatedSprite extends Sprite {
	private var tilesheet: Tilesheet;

	private var animations: Map<String, SpriteAnimation>;
	private var animationTimeBase: Float;

	public var currentAnimation(default, null): String;

	public var flipX(default, set): Bool;
	public var flipY(default, set): Bool;

	private var pivot: Point;
	public function new(bitmap: BitmapData, tileWidth: Int, tileHeight: Int) {
		super();

		tilesheet = new Tilesheet(bitmap);

		var tileRows: Int = Math.floor(bitmap.width / tileWidth);
		var tileCols: Int = Math.floor(bitmap.height / tileHeight);

		// Get spritesheet frames
		for (y in 0...tileCols) {
			for (x in 0...tileRows) {
				tilesheet.addTileRect(new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight));
			}
		}

		animations = new Map<String, SpriteAnimation>();
		currentAnimation = "";
		animationTimeBase = Lib.getTimer();

		// Set pivot to center (default)
		pivot = new Point();
		pivot.x = -tileWidth/2;
		pivot.y = -tileHeight/2;

		// Set default orientation
		flipX = false;
		flipY = false;

		addEventListener(Event.ADDED_TO_STAGE, function(e: Event){
			stage.addEventListener(Event.ENTER_FRAME, render);
		});
	}

	public function addAnimation(animationName: String, animation: SpriteAnimation) {
		animations[animationName] = animation;
	}

	public function playAnimation(newAnimation: String) {
		animationTimeBase = Lib.getTimer();
		currentAnimation = newAnimation;
	}

	private function render(e: Event) {
		var timeOffset: Float = (Lib.getTimer() - animationTimeBase) / 1000;
		var animation: SpriteAnimation = animations[currentAnimation];
		var currentAnimationTile: Int = Math.floor(timeOffset / animation.speed) % animation.frames.length;

		this.graphics.clear();
		tilesheet.drawTiles(graphics, [pivot.x, pivot.y, animation.frames[currentAnimationTile]]);
	}

	private function set_flipX(value: Bool): Bool {
		scaleX = value ? -1 : 1;
		return this.flipX = value;
	}

	private function set_flipY(value: Bool): Bool {
		scaleY = value ? -1 : 1;
		return this.flipY = value;
	}
}
