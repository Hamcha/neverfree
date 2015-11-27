package gamestates;

import nf.GameState;
import nf.graphics.Tilemap;
import nf.graphics.Scene;
import assets.map.TestPlane;
import actors.characters.Player;
import openfl.events.Event;

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
