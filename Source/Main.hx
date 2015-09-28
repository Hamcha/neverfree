package;

import openfl.events.Event;
import openfl.display.FPS;
import openfl.display.Sprite;

class Main extends Sprite {
	public function new() {
		super();
		addEventListener(Event.ADDED_TO_STAGE, added);
	}

	public function added(e: Event) {
		var game: Game = new Game();
		addChild(game);
		addChild(new FPS());
	}
}