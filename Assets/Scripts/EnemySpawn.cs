using Euclase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype {
    public class EnemySpawn : EBase {

        [SerializeField] private GameObject _enemy;

        private int amount;

        void Start() {
            StartCoroutine(Spawn());
        }

        void Update() {

        }

        private IEnumerator Spawn() {
            while(true) {
                amount++;
                for(int i = 0; i < amount; i++) {
                    Instantiate(_enemy, Transform.position, Quaternion.identity);
                }

                yield return new WaitForSeconds(10);
            }
        }
    }
}
