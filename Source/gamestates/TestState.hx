package gamestates;

import graphics.Tilemap;
import assets.map.TestPlane;
import openfl.events.Event;
import actors.characters.Player;
import graphics.Scene;

class TestState extends GameState {
	private var map: Tilemap;
	private var scene: Scene;
	private var player: Player;

	public override function initialize(e: Event) {
		map = new TestPlane();

		scene = new Scene();
		scene.addChild(map);
		scene.camera.zoom(2, 2);

		player = new Player();
		scene.addActor(player);
		player.x = 75;
		player.y = 75;

		addChild(scene);
	}

	public override function update(e: Event) {
		scene.camera.moveToObject(player);
	}
}
