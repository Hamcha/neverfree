package;

import openfl.events.Event;
import openfl.display.Sprite;
import openfl.Lib;
import gamestates.TestState;

class Game extends Sprite {
	private var currentState: GameState;
	private var lastFrameTime: Int;

	static public var timeDelta: Float;

	public function new() {
		super();

		// Test state (for now)
		this.setState(new TestState());

		// Reset variables
		lastFrameTime = 0;

		// Setup rendering event
		addEventListener(Event.ADDED_TO_STAGE, function(e: Event){
			stage.addEventListener(Event.ENTER_FRAME, update);
		});
	}

	public function update(e: Event) {
		// Calculate time delta
		var currentTime: Int = Lib.getTimer();
		timeDelta = (currentTime - lastFrameTime) * 0.001;
		lastFrameTime = currentTime;
	}

	public function setState(state: GameState) {
		if (currentState != null) {
			removeChild(currentState);
			currentState.unload();
		}
		state.load();
		currentState = state;
		addChild(currentState);
	}
}
