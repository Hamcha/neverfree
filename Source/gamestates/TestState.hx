package gamestates;

import openfl.events.Event;
import actors.characters.Player;
import graphics.Scene;

class TestState extends GameState {
	public override function initialize(e: Event) {
		var scene: Scene = new Scene();
		addChild(scene);

		var player: Player = new Player();
		scene.addChild(player);
	}
}
