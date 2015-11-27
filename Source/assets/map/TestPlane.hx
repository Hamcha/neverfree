package assets.map;

import nf.physics.TileCollider.TileCollisionType;
import nf.graphics.Tilemap;
import openfl.Assets;

class TestPlane extends Tilemap {
	public function new() {
		super("TestPlane", 10, 10, 16, 16);
		tilesets.push(new Tileset("Fields", Assets.getBitmapData("assets/Tilesets/Fields.png"), 16, 16, 1));

		layers.push(new MapLayer([0,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,1,1,1,1,1,1,1,1,1,2,2,1,1,1,1,1,1,1,1,1,2,2,1,2,1,1,1,1,1,1,1,2,1,2,2,1,1,1,1,1,1,2,1,2,2,1,1,1,1,1,1,2,1,2,2,1,1,1,1,1,1,2,1,2,2,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1], 10, 10, 16, 16));
		collision = new CollisionLayer([TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.FULL,
			TileCollisionType.FULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.FULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL,
			TileCollisionType.NULL], 10, 10, 16, 16);
	}
}

