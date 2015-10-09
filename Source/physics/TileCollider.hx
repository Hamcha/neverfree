package physics;

import differ.shapes.Polygon;
import graphics.Tilemap.TileCollisionType;

class TileCollider extends Collider {
	public function new(type: TileCollisionType, tileWidth: Float, tileHeight: Float) {
		switch(type) {
			case TileCollisionType.FULL:
				shape = Polygon.rectangle(0, 0, tileWidth, tileHeight, false);
			default:
				shape = null;
		}
	}
}
