package;

import openfl.events.KeyboardEvent;
import openfl.display.Stage;

class Input {
	public static var instance(get, null): Input;

	public function bindEvents(stage: Stage) {
		stage.addEventListener(KeyboardEvent.KEY_DOWN, onKeyDown);
		stage.addEventListener(KeyboardEvent.KEY_DOWN, onKeyUp);
	}

	private function onKeyDown(e: KeyboardEvent) {
	}

	private function onKeyUp(e: KeyboardEvent) {
	}

	private static function get_instance(): Input {
		if (instance == null) {
			instance = new Input();
		}
		return instance;
	}

	private function new() {
	}
}
