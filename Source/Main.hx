package;

import openfl.display.FPS;
import openfl.display.Sprite;

class Main extends Sprite {
	public function new() {
		super();
		var game: Game = new Game();
		addChild(game);
		addChild(new FPS());
	}
}