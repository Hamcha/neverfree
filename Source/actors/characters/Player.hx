package actors.characters;

import openfl.events.Event;
import openfl.Assets;
import graphics.AnimatedSprite;

class Player extends Actor {
	private var sprite: AnimatedSprite;
	private var maxSpeed: Float = 30; // Max speed in any direction (px/s aka Pixels per second)
	private var damping: Float = 10; // Damping (px/s^2)
	private var xSpeed: Float = 0; // Current speed X (px/s)
	private var ySpeed: Float = 0; // Current speed Y (px/s)
	private var threshold: Float = 0.5; // Moving/Stopped threshold (px/s)

	public function new() {
		super();

		sprite = new AnimatedSprite(Assets.getBitmapData("assets/Characters/White/White.png"), 20, 16);
		sprite.addAnimation("idle", new SpriteAnimation([0], 100));
		sprite.addAnimation("walk", new SpriteAnimation([1,2,3,4,5,6], 0.1));
		sprite.playAnimation("idle");

		addChild(sprite);

		addEventListener(Event.ADDED_TO_STAGE, function(e: Event){
			stage.addEventListener(Event.ENTER_FRAME, update);
		});
	}

	private function update(e: Event) {
		// Get movement
		var vertical: Float = Input.getButton("Up") ? -1 : Input.getButton("Down") ? 1 : 0;
		var horizontal: Float = Input.getButton("Left") ? -1 : Input.getButton("Right") ? 1 : 0;

		// Decelerate if not moving, move at max speed otherwise
		if (vertical == 0) {
			if (Math.abs(ySpeed) < threshold) {
				ySpeed = 0;
			} else {
				ySpeed -= ySpeed * damping * Game.timeDelta;
			}
		} {
			ySpeed = vertical * maxSpeed;
		}

		if (horizontal == 0) {
			if (Math.abs(xSpeed) < threshold) {
				xSpeed = 0;
			} else {
				xSpeed -= xSpeed * damping * Game.timeDelta;
			}
		} else {
			xSpeed = horizontal * maxSpeed;
		}

		// Crop speed to maximum
		var speedlen: Float = Math.sqrt(xSpeed * xSpeed + ySpeed * ySpeed);
		if (speedlen > maxSpeed) {
			xSpeed = xSpeed / speedlen * maxSpeed;
			ySpeed = ySpeed / speedlen * maxSpeed;
		}

		x += xSpeed * Game.timeDelta;
		y += ySpeed * Game.timeDelta;

		// Set animation depending on status
		var moving: Bool = vertical != 0 || horizontal != 0;
		if (moving == false && sprite.currentAnimation == "walk") {
			sprite.playAnimation("idle");
		}
		if (moving == true && sprite.currentAnimation == "idle") {
			sprite.playAnimation("walk");
		}

		// Flip sprite depending on direction
		if (sprite.flipX && horizontal > 0) {
			sprite.flipX = false;
		}
		if (!sprite.flipX && horizontal < 0) {
			sprite.flipX = true;
		}
	}
}
