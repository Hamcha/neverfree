package graphics;

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

	public function new(bitmap: BitmapData, tileWidth: Int, tileHeight: Int) {
		super();

		tilesheet = new Tilesheet(bitmap);

		var tileRows: Int = Math.floor(bitmap.width / tileWidth);
		var tileCols: Int = Math.floor(bitmap.height / tileHeight);

		// Get spritesheet frames
		for (y in 0...tileCols) {
			for (x in 0...tileRows) {
				tilesheet.addTileRect(new Rectangle(x * tileWidth, y * tileHeight, (x + 1) * tileWidth, (y + 1) * tileHeight));
			}
		}

		animations = new Map<String, SpriteAnimation>();
		currentAnimation = "";
		animationTimeBase = Lib.getTimer();

		// Set pivot to center (default)
		this.x = -tileWidth/2;
		this.y = -tileHeight/2;

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
		var currentAnimationTile: Int = Math.floor(timeOffset * animation.speed) % animation.frames.length;
		tilesheet.drawTiles(graphics, [0, 0, currentAnimationTile]);
	}
}
