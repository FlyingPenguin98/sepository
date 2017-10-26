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
    protected float speed;
    void Start() {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void UpdatePosition() {
        velocity += force;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        position += velocity;
        transform.Translate(velocity);
    }

    protected void ApplyForce(Vector3 newforce) {
        force += (newforce / mass * speed) * Time.deltaTime;
        force = Vector3.ClampMagnitude(force, maxForce);
    }

    protected void ApplyDrag() {
        if (direction == Vector3.zero)
            return;
        Vector3 friction = -1 * direction; //* Time.deltaTime;
        friction *= coef;
        friction *= velocity.sqrMagnitude * area;
        force += friction;
    }

    protected void ProcessInputs() {
        direction = Vector3.zero;
        force = Vector3.zero;
        velocity = Vector3.zero;
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            direction += Vector3.up;
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            direction += Vector3.left;
            if (!this.gameObject.GetComponent<SpriteRenderer>().flipX)
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            direction += Vector3.down;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            direction += Vector3.right;
            if (this.gameObject.GetComponent<SpriteRenderer>().flipX)
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        ApplyForce(direction);

    }

    protected void StopShip() {
        if (screenMax.x < position.x + myBounds.extents.x) {
            position.x = screenMax.x - myBounds.extents.x;
        }
        else if (screenMin.x > position.x - myBounds.extents.x) {
            position.x = screenMin.x + myBounds.extents.x;
        }
        if(screenMax.y < position.y + myBounds.extents.y) {
            position.y = screenMax.y - myBounds.extents.y;
        }
        else if(screenMin.y > position.y - myBounds.extents.y) {
            position.y = screenMin.y + myBounds.extents.y;
        }
        transform.SetPositionAndRotation(position, transform.rotation);
    }
}
