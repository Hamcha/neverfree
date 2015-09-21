package gamestates;

import assets.map.TestPlane;
import openfl.events.Event;
import actors.characters.Player;
import graphics.Scene;

class TestState extends GameState {
	public override function initialize(e: Event) {
		var scene: Scene = new Scene();
		scene.addChild(new TestPlane());
		scene.addChild(new Player());
		scene.camera.zoom(2, 2);
		scene.camera.moveToCoords(0, 0);

		addChild(scene);
	}
}
