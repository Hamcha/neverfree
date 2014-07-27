﻿using UnityEngine;

public class Transition : MonoBehaviour {

    public bool isVertical;
    public bool side;

    public Animator[] Vertical;
    public Animator[] Horizontal;

    Animator master;
    string moveTo;

    void Awake() {
        master = GetComponent<Animator>();
    }

    public void FadeIn(int id) {
        int i = side ? 3 - id : id;
        Animator block = isVertical ? Vertical[i] : Horizontal[i];
        block.SetTrigger("FadeIn");
    }

    public void FadeOut(int id) {
        int i = side ? 3 - id : id;
        Animator block = isVertical ? Vertical[i] : Horizontal[i];
        block.SetTrigger("FadeOut");
    }

    public void Goto(string levelName, bool _side, bool _vertical) {
        moveTo = levelName;
        side = _side;
        isVertical = _vertical;
        master.SetTrigger("FadeIn");
    }

    public void FinallyMove() {
        Application.LoadLevel(moveTo);
    }

    void OnLevelWasLoaded() {
        master.SetTrigger("FadeOut");
    }
}
