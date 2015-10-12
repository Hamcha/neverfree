package physics;

import graphics.Tilemap.CollisionLayer;
import differ.shapes.Polygon;

enum TileCollisionType {
	NULL;
	FULL;
	TOPHALF;
	BOTTOMHALF;
	LEFTHALF;
	RIGHTHALF;
	TRI_DR;
	TRI_DL;
	TRI_UR;
	TRI_UL;
}

class TileCollider extends Collider {
	public var layer: CollisionLayer;
	private var mapWidth: Int;

	public function new(layer: CollisionLayer, mapWidth: Int) {
		this.layer = layer;
		this.mapWidth = mapWidth;
	}

	public override function test(collider: Collider): CollisionData {
		if (collider == null || collider.shape == null) {
			return new CollisionData(null);
		}

		var tileX: Int = Math.floor(collider.shape.x / layer.tileWidth);
		var tileY: Int = Math.floor(collider.shape.y / layer.tileHeight);
		var tileId: Int = tileY * mapWidth + tileX;

		var tileCollisionType: TileCollisionType = layer.layerData[tileId];
		trace(tileCollisionType);
		switch(tileCollisionType) {
			case TileCollisionType.FULL:
				shape = Polygon.rectangle(0, 0, layer.tileWidth, layer.tileHeight, false);
			case TileCollisionType.NULL:
			default:
				return new CollisionData(null);
		}
		setOffset(tileX, tileY);

		return collider.test(this);
	}
}
