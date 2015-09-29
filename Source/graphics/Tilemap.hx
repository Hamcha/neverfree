package graphics;

import openfl.events.Event;
import openfl.geom.Rectangle;
import openfl.display.Tilesheet;
import openfl.display.BitmapData;
import openfl.display.Sprite;

class Tileset {
	public var name: String;
	public var basegid: Int;
	public var endgid: Int;
	public var tilesheet: Tilesheet;

	public function new(argName: String, bitmap: BitmapData, tileWidth: Int, tileHeight: Int, startgid: Int) {
		basegid = startgid;
		name = argName;

		tilesheet = new Tilesheet(bitmap);

		var tileRows: Int = Math.floor(bitmap.width / tileWidth);
		var tileCols: Int = Math.floor(bitmap.height / tileHeight);

		endgid = basegid + tileRows * tileCols;

		// Get spritesheet frames
		for (y in 0...tileCols) {
			for (x in 0...tileRows) {
				tilesheet.addTileRect(new Rectangle(x * tileWidth, y * tileHeight, (x + 1) * tileWidth, (y + 1) * tileHeight));
			}
		}
	}
}

class MapLayer {
	public var layerData: Array<Float>;
	public var maxgid: Int;
	public var mingid: Int;
	public function new(tiles: Array<Int>, layerWidth: Int, layerHeight: Int, tileWidth: Int, tileHeight: Int, basegid: Int) {
		layerData = new Array<Float>();
		var index: Int = 0;
		var maxgid: Int = 0;
		var mingid: Int = 99999;
		for (y in 0...layerHeight) {
			for (x in 0...layerWidth) {
				var tileId: Int = tiles[index];

				// Check for mingid/maxgid
				if (mingid > tileId) {
					mingid = tileId;
				}
				if (maxgid < tileId) {
					maxgid = tileId;
				}

				// Order is: X, Y, TileID, …
				layerData.push(x * tileWidth);
				layerData.push(y * tileHeight);
				layerData.push(tileId - basegid);
				index++;
			}
		}
	}
}

class Tilemap extends Sprite {
	private var tilewidth: Int;
	private var tileheight: Int;
	private var mapwidth: Int;
	private var mapheight: Int;

	public var tilesets: Array<Tileset>;
	public var layers: Array<MapLayer>;

	public function new(
			argName: String,
			argWidth: Int,
			argHeight: Int,
			argTileWidth: Int,
			argTileHeight: Int) {
		super();
		name = argName;
		mapwidth = argWidth;
		mapheight = argHeight;
		tilewidth = argTileWidth;
		tileheight = argTileHeight;

		tilesets = new Array<Tileset>();
		layers = new Array<MapLayer>();

		addEventListener(Event.ADDED_TO_STAGE, function(e: Event){
			stage.addEventListener(Event.ENTER_FRAME, render);
		});
	}

	private function render(e: Event) {
		this.graphics.clear();
		// Draw all layers
		for (layer in layers) {
			// Get matching tileset
			for (tileset in tilesets) {
				//TODO Gid magic (?)
				tileset.tilesheet.drawTiles(graphics, layer.layerData, false);
				break;
			}
		}
	}

	public function mapWidth(): Float {
		return mapwidth * tilewidth;
	}

	public function mapHeight(): Float {
		return mapheight * tileheight;
	}
}
