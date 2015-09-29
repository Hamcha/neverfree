package gamestates;

import assets.map.TestPlane;
import openfl.events.Event;
import actors.characters.Player;
import graphics.Scene;

class TestState extends GameState {
	private var scene: Scene;
	private var player: Player;

	public override function initialize(e: Event) {
		scene = new Scene();
		scene.addChild(new TestPlane());
		scene.camera.zoom(2, 2);

		player = new Player();
		scene.addChild(player);
		scene.camera.moveToObject(player);

		addChild(scene);
	}

	public override function update(e: Event) {
		scene.camera.moveToObject(player);
	}
}
