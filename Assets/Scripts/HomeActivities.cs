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
        public float energyIncrease = 1;
        public float timerLength;
        private float _timer;
        private bool _moveButtonHolder;
        public bool canpush;
        public Player_walking walk;

        private Vector3 _altPos;
        private Vector3 _originalPos;
        private AudioSource _audioSource;

        //animation
        public Animator clockAnimation;
        public GameObject clock;

   
        void Awake()
        {
            if (GetComponent<AudioSource>() == false) gameObject.AddComponent<AudioSource>();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.volume = 0.25f;
            _altPos = new Vector3(buttonHolder.transform.position.x, -buttonHolder.GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.height, buttonHolder.transform.position.z);
            _originalPos = buttonHolder.transform.position;
            buttonHolder.transform.position = _altPos;
            buttonHolder.SetActive(false);
         
            //animation
           clockAnimation = clock.GetComponent<Animator>();
           clockAnimation.SetBool("clockStart", false);
          //  walk.movementspeed = 300;
        }

        void Update()
        {
            if (_moveButtonHolder && _timer < timerLength)
            {
                _timer += Time.deltaTime;
                buttonHolder.transform.position = Vector3.Lerp(_altPos, _originalPos, _timer / timerLength);
            }
            else _moveButtonHolder = false;
        }

        public void ResetActivities()
        {
            //renable all button pushing.
            buttonHolder.transform.position = _altPos;
            buttonHolder.SetActive(true);
            _timer = 0;
            canpush = true;
            _moveButtonHolder = true;
            walk.movementspeed = 0;
            //Clock animation
            //clockAnimation.SetBool("clockStart", false);
        }

        public void Click(int i)
        {

            if (canpush == false)
                return;
            GameManager.currentMaxStamina += energyIncrease;
            ChooseActivity(i, buttonHolder);
            //Disable all button pushing.
            canpush = false;
            //disable player
          
            //Clock animation
            clockAnimation.SetBool("clockStart", true);
        
        }

        void ChooseActivity(int i, GameObject obj = default(GameObject))
        {
            if (audioClips[i] != null)
            {
                _audioSource.clip = audioClips[i];
                _audioSource.Play();
                IEnumerator rou = FadInLoading.LoadTransition(audioClips[i].length, obj);
                StartCoroutine(rou);
              //  walk.movementspeed = 300;
            }
            else Debug.Log($"{i} is out of bounds");
        }

        public void ResetActivitesBar(Transform trans)
        {
            if (Vector3.Distance(trans.position, GameManager.instance.playerWalking.gameObject.transform.position) <= 30)
            {
                ResetActivities();
                trans.gameObject.SetActive(false);
             
            }
        }
    }
}