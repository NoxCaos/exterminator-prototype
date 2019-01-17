using Euclase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype {
    public class GunController : EBase {

        public Vector3 Target { get; private set; }

        [SerializeField] private float _shootPower;
        [SerializeField] private float _curScale;
        [SerializeField] private float _reload;
        [SerializeField] private LayerMask _interactLayers;
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private Transform _ref;

        private int _curShape;
        private float _curTime;
        private GameObject _curRef;

        void Update() {
            //RaycastHit hit;
            //var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * .5f, Screen.height * .5f));
            //if(Physics.Raycast(ray, out hit, 1000, _interactLayers)) {
            //    Target = hit.point;
            //} else {
                Target = Transform.position +  Camera.main.transform.forward * 100;
            //}

            if(Input.GetButtonUp("Fire2")) {
                _curShape++;
                if(_curShape >= _prefabs.Length)
                    _curShape = 0;
            }

            var delta = Input.GetAxis("Mouse ScrollWheel") * .1f;
            _curScale = Mathf.Clamp(_curScale + delta, .2f, 100);
            _ref.localScale = Vector3.one * _curScale;

            Transform.LookAt(Target);

            if(Input.GetButton("Fire1") && _curTime < Time.time) {
                var rb = Instantiate(_prefabs[_curShape], Transform.position, Transform.rotation)
                    .GetComponent<Rigidbody>();
                rb.AddForce(Transform.TransformDirection(new Vector3(0, .1f, 1)) * _shootPower, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere, ForceMode.Impulse);
                rb.transform.localScale = Vector3.one * _curScale;
                _curTime = Time.time + _reload;
            }
        }
    }
}
