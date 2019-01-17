using Euclase;
using Euclase.ETransform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype {
    public class Character : PhysicsEBase {

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _verticalAcceleration;
        [SerializeField] private GunController _gun;
        [SerializeField] private Renderer _rend;

        private Vector2 _input;
        private Collider _col;
        private Material _mat;
        private bool _fly;

        void Start() {
            _col = GetComponent<Collider>();
            _mat = _rend.material;
        }

        private void Update() {
            _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if(Input.GetButtonDown("Run")) {
                _col.enabled = false;
                Rigidbody.useGravity = false;
                _mat.color = new Color(1, 1, 1, .5f);
            } else if(Input.GetButtonUp("Run")) {
                _col.enabled = true;
                Rigidbody.useGravity = true;
                _mat.color = new Color(1, 1, 1, 1f);
            }

            _fly = Input.GetButton("Jump");

            Transform.LerpLookAt(_gun.Target, Vector3.up, 10 * Time.deltaTime);
        }

        void FixedUpdate() {
            if(_fly)
                Rigidbody.AddForce(Vector3.up * _verticalAcceleration, ForceMode.Acceleration);

            Rigidbody.AddForce(Transform.TransformDirection(new Vector3(_input.x, 0, _input.y) * _moveSpeed) * Time.deltaTime, ForceMode.Acceleration);
        }
    }
}
