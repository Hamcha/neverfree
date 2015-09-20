package assets.map;

import openfl.Assets;
import graphics.Tilemap;

class BlankMap extends Tilemap{
	public function new() {
		super("Blank map", 3, 2, 16, 16);
		tilesets.push(new Tileset("test", Assets.getBitmapData("assets/Tilesets/Fields.png"), 16, 16, 1));
		layers.push(new MapLayer([1, 1, 1, 1, 1, 1], 3, 2, 16, 16, 1));
	}
}
