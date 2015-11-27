package;

import nf.Game;
import gamestates.TestState;

class NFGame extends Game {
	public function new() {
		super();

		// Test state (for now)
		this.setState(new TestState());
	}
}
