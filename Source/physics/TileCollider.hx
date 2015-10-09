package physics;

import graphics.Tilemap.TileCollisionType;

class TileCollider extends Collider {
	private var type: TileCollisionType;

	public function new(type: TileCollisionType) {
		this.type = type;
	}
}
