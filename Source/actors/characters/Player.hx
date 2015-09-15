package actors.characters;

import openfl.Assets;
import graphics.AnimatedSprite;

class Player extends Actor {
	private var sprite: AnimatedSprite;

	public function new() {
		super();

		sprite = new AnimatedSprite(Assets.getBitmapData("assets/Character/White/White.png"), 20, 16);
		sprite.addAnimation("idle", new Animation([0], 100));
		sprite.addAnimation("walk", new Animation([1,2,3,4,5,6], 100));
		sprite.playAnimation("idle");
	}
}
