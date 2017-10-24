using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Entity : MonoBehaviour {

    protected Vector3 position;
    protected Vector3 direction;
    protected Vector3 force;
    protected Vector3 velocity;
    protected Vector3 screenMin;
    protected Vector3 screenMax;
    protected Bounds myBounds;
    protected Material skin;
    protected float maxSpeed;
    protected float maxForce;
    protected float area;
    protected float coef;
    protected float mass;
    protected float drag;
    void Start() {
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
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void UpdatePosition() {
        velocity += force;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        position += velocity;
        transform.position = position;
    }

    protected void ApplyForce(Vector3 newforce) {
        force += newforce / mass;
        force = Vector3.ClampMagnitude(force, maxForce);
        
    }

    protected void ApplyDrag() {
        Vector3 friction = -1 * direction;
        friction *= coef;
        friction *= velocity.sqrMagnitude * area;
        force += friction;
    }

    protected void ProcessInputs() {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            direction = Vector3.up;
            ApplyForce(direction);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            direction = Vector3.left;
            ApplyForce(direction);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            direction = Vector3.down;
            ApplyForce(direction);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            direction = Vector3.right;
            ApplyForce(direction);
        }
        
    }

    protected void StopShip() {
        if (screenMax.x < position.x - myBounds.extents.x) {
            position.x = screenMin.x;
        }
        else if (screenMin.x > position.x + myBounds.extents.x) {
            position.x = screenMax.x;
        }
        if(screenMax.y > position.y - myBounds.extents.y) {
            position.y = screenMax.y;
        }
        else if(screenMin.y > position.y + myBounds.extents.y) {
            position.y = screenMin.y;
        }
    }
}
