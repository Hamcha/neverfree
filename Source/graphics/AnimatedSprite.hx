package graphics;

import openfl.events.Event;
import openfl.display.Sprite;
import openfl.Lib;
import openfl.display.BitmapData;
import openfl.display.Tilesheet;

class Animation {
	public var frames: Array<Int>;
	public var speed: Float;

	public function new(argFrames: Array<Int>, argSpeed: Float) {
		frames = argFrames;
		speed = argSpeed;
	}
}

class AnimatedSprite extends Sprite {
	private var tilesheet: Tilesheet;
	private var tileWidth: Int;
	private var tileHeight: Int;
	private var tileRows: Int;
	private var tileCols: Int;

	private var animations: Map<String, Animation>;
	private var animationTimeBase: Float;

	public var currentAnimation(default, null): String;

	public function new(bitmap: BitmapData, argTileWidth: Int, argTileHeight: Int) {
		super();

		tilesheet = new Tilesheet(bitmap);
		tileWidth = argTileWidth;
		tileHeight = argTileHeight;

		tileRows = Math.floor(bitmap.width / tileWidth);
		tileCols = Math.floor(bitmap.height / tileHeight);
		//TODO Create tiles

		animations = new Map<String, Animation>();
		currentAnimation = "";
		animationTimeBase = Lib.getTimer();

		addEventListener(Event.ADDED_TO_STAGE, function(e: Event){
			stage.addEventListener(Event.ENTER_FRAME, render);
		});
	}

	public function addAnimation(animationName: String, animation: Animation) {
		animations[animationName] = animation;
	}

	public function playAnimation(newAnimation: String) {
		animationTimeBase = Lib.getTimer();
		currentAnimation = newAnimation;
	}

	private function render(e: Event) {
		var timeOffset: Float = (Lib.getTimer() - animationTimeBase) / 1000;
		var animation: Animation = animations[currentAnimation];
		var currentAnimationTile: Int = Math.floor(timeOffset * animation.speed) % animation.frames.length;
		tilesheet.drawTiles(graphics, [0, 0, currentAnimationTile]);
	}
}
