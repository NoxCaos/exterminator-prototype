using Euclase;
using Euclase.EVector3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Prototype {
    public class Enemy : EBase {

        [SerializeField] private int _maxHP;

        private NavMeshAgent _agent;
        private float _maxSpeed;
        private int _hp;

        void Start() {
            _agent = GetComponent<NavMeshAgent>();
            _maxSpeed = _agent.speed;
            _agent.SetDestination(Vector3.zero);
            _hp = _maxHP;
        }

        void Update() {
            if(_agent.speed < _maxSpeed)
                _agent.speed += Time.deltaTime;

            var mag = Transform.position.Mul(1, 0, 1).sqrMagnitude;
            //Debug.Log(mag);
            if(mag < 5) {
                Destroy(gameObject);
                Base.Instance.HP--;
            }
        }

        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Eatable" && _agent.speed > .5f) {
                _agent.speed = Mathf.Clamp(_agent.speed - 1, 0, _maxSpeed);
                Destroy(other.gameObject);
                _hp--;

                if(_hp <= 0) {
                    Destroy(gameObject);
                    Base.Instance.Score++;
                }
            }
        }
    }
}
