using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    private int health;

    // Use this for initialization
    void Start () {
        position = transform.position;
        direction = Vector3.zero;
        force = Vector3.zero;
        velocity = Vector3.zero;
        screenMin = Camera.main.ScreenToWorldPoint(Vector3.zero);
        screenMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        myBounds = GetComponent<SpriteRenderer>().bounds;
        area = myBounds.size.x * myBounds.size.y;
        maxSpeed = 1f;
        maxForce = 2f;
        coef = .2f;
        mass = 1f;
        drag = .98f;
    }

    private void FixedUpdate() {
        ProcessInputs();
        //ApplyDrag();
        UpdatePosition();
        //StopShip();
        this.force = Vector3.zero;
    }
    // Update is called once per frame
    void Update () {

	}
}
