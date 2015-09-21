package assets.map;

import openfl.Assets;
import graphics.Tilemap;

class TestPlane extends Tilemap {
	public function new() {
		super("TestPlane", 10, 10, 16, 16);
		tilesets.push(new Tileset("Fields", Assets.getBitmapData("assets/Tilesets/Fields.png"), 16, 16, 1));
		layers.push(new MapLayer([1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1], 10, 10, 16, 16, 1));
	}
}

