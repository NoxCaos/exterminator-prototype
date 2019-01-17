using Euclase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype {
    public class Bullet : EBase {

        private Collider _collider;
        private float _time;

        private void Start() {
            _collider = GetComponent<Collider>();
            _time = Time.time + .5f;
            Destroy(gameObject, 30);
        }

        private void Update() {
            if(Time.time < _time)
                Transform.localScale += Vector3.one * 2f * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision collision) {
            if(collision.gameObject.layer != 10) {
                _collider.enabled = false;
                StartCoroutine(DisableObject());
            }
        }

        private IEnumerator DisableObject() {
            yield return new WaitForSeconds(.1f);
            Destroy(GetComponent<Rigidbody>());
            gameObject.layer = 0;
            gameObject.tag = "Eatable";
            _collider.enabled = true;
            enabled = false;
        }
    }
}
