using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype {
    public class Base : MonoBehaviour {
        public static Base Instance;

        [SerializeField] private float _hp;
        [SerializeField] private Slider _bar;
        [SerializeField] private Text _scoreText;

        public float HP {
            get { return _hp; }
            set {
                _bar.value = value;
                _hp = value;
            }
        }

        public int Score {
            get { return _score; }
            set {
                _scoreText.text = value.ToString();
                _score = value;
            }
        }

        private int _score;

        private void Awake() {
            Instance = this;
            _bar.maxValue = _hp;
            HP = _hp;
        }
    }
}
