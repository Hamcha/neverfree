package actors.characters;

import graphics.Screen;
import openfl.Assets;
import graphics.AnimatedSprite;

class Player extends Actor {
	private var sprite: AnimatedSprite;

	public function new() {
		super();

		sprite = new AnimatedSprite(Assets.getBitmapData("assets/Characters/White/White.png"), 20, 16);
		sprite.addAnimation("idle", new SpriteAnimation([0], 100));
		sprite.addAnimation("walk", new SpriteAnimation([1,2,3,4,5,6], 100));
		sprite.playAnimation("idle");

		addChild(sprite);
	}
}
