package gamestates;

import openfl.events.Event;
import actors.characters.Player;

class TestState extends GameState {
	public override function initialize(e: Event) {
		var player: Player = new Player();
		addChild(player);
	}
}
