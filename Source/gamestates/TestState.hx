package gamestates;

import assets.map.TestPlane;
import openfl.events.Event;
import actors.characters.Player;
import graphics.Scene;

class TestState extends GameState {
	public override function initialize(e: Event) {
		var scene: Scene = new Scene();
		scene.addChild(new TestPlane());
		scene.camera.zoom(2, 2);

		var player: Player = new Player();
		scene.addChild(player);
		scene.camera.moveToObject(player);

		addChild(scene);
	}
}
