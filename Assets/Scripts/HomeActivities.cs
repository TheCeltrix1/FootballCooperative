using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace HomeCode
{
    public class HomeActivities : MonoBehaviour
    {
        public AudioClip[] audioClips;
        public GameObject buttonHolder;
        public float energyIncrease = 5;
        public float timerLength;
        private float _timer;
        private bool _moveButtonHolder;

        private Vector2 _altPos;
        private Vector2 _originalPos;
        private AudioSource _audioSource;

        void Awake()
        {
            if (GetComponent<AudioSource>() == false) gameObject.AddComponent<AudioSource>();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.volume = 0.25f;
            _altPos = new Vector2(buttonHolder.transform.position.x, -buttonHolder.GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.height);
            _originalPos = buttonHolder.transform.position;
            buttonHolder.transform.position = _altPos;
        }

        void Update()
        {
            if (Input.GetKeyDown("k")) ResetActivities();
            if (_moveButtonHolder && _timer < timerLength)
            {
                _timer += Time.deltaTime;
                buttonHolder.transform.position = Vector2.Lerp(_altPos, _originalPos, _timer / timerLength);
            }
            else _moveButtonHolder = false;
        }

        public void ResetActivities()
        {
            buttonHolder.transform.position = _altPos;
            buttonHolder.SetActive(true);
            _timer = 0;
            _moveButtonHolder = true;
        }

        public void Click(int i)
        {
            GameManager.maxenergy += energyIncrease;
            ChooseActivity(i, buttonHolder);
        }

        void ChooseActivity(int i, GameObject obj = default(GameObject))
        {
            if (audioClips[i] != null)
            {
                _audioSource.clip = audioClips[i];
                _audioSource.Play();
                IEnumerator rou = FadInLoading.LoadTransition(audioClips[i].length, obj);
                StartCoroutine(rou);
            }
            else Debug.Log($"{i} is out of bounds");
        }
    }
}