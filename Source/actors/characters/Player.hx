package actors.characters;

import graphics.Screen;
import openfl.Assets;
import graphics.AnimatedSprite;

class Player extends Actor {
	private var sprite: AnimatedSprite;

	public function new() {
		super();

		sprite = new AnimatedSprite(Assets.getBitmapData("assets/Character/White/White.png"), 20, 16);
		sprite.addAnimation("idle", new SpriteAnimation([0], 100));
		sprite.addAnimation("walk", new SpriteAnimation([1,2,3,4,5,6], 100));
		sprite.playAnimation("idle");

		addChild(sprite);

		this.x = Screen.width / 2;
		this.y = Screen.height / 2;
		this.scaleX = 2;
		this.scaleY = 2;
	}
}
