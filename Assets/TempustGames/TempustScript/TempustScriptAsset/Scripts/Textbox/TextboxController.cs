using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TempustScript
{
    public class TextboxController : MonoBehaviour
    {
        public static TextboxController singleton;
        
        [SerializeField] private QuestionBox questionBox;
        [SerializeField] private Text textArea;
        [SerializeField] private GameObject speakerBox;
        [SerializeField] private Image leftPortrait;
        [SerializeField] private Image rightPortrait;

        [SerializeField] private GameObject textBoxObject;

        private bool isTyping;
        protected bool cancelTyping;

        public event UnityAction onClose;
        public event UnityAction onOpen;
        public bool isOpen { get; private set; }

        [Header("Sounds")]
        [SerializeField] protected AudioClip typeSound;
        [SerializeField] protected AudioClip continueSound;
        protected AudioSource audioSource;

        public enum TypeSpeed { SLOW, MEDIUM, FAST }

        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Continue()
        {
            if (isTyping)
            {
                isTyping = false;
                cancelTyping = true;
            }
            else
            {
                shouldContinue = true;
            }
        }

        public void Close()
        {
            if (isOpen)
            {
                textBoxObject.SetActive(false);
                onClose?.Invoke();
                isOpen = false;
            }
        }
        public void Open()
        {
            textBoxObject.SetActive(true);
            onOpen?.Invoke();
            isOpen = true;
        }

        /// <summary>
        /// Displays the given image on a character portrait. Setting the portrait to null will hide it.
        /// </summary>
        /// <param name="image">Sprite to display for the portrait.</param>
        /// <param name="isLeftPortrait">True for left portrait, false for right portrait.</param>
        public void SetPortrait(Sprite image, bool isLeftPortrait)
        {
            Image portrait = isLeftPortrait ? leftPortrait : rightPortrait;
            if (image == null)
                portrait.gameObject.SetActive(false);
            else
            {
                portrait.gameObject.SetActive(true);
                portrait.sprite = image;
            }
        }

        public int GetSelectedOption(bool closeBox)
        {
            return questionBox.GetSelectedOption(closeBox);
        }

        public void SelectOption(int value)
        {
            if (questionBox.gameObject.activeSelf)
            {
                questionBox.MoveSelection(value);
                audioSource.clip = continueSound;

                if (audioSource.clip != null)
                    audioSource.Play();
            }
        }

        public IEnumerator AskQuestion(string speaker, string question, List<string> options)
        {
            if (!isOpen)
                Open();
            yield return Type(speaker, question, false);
            questionBox.ShowBox(options);
            yield return WaitForContinue();
        }
        public IEnumerator Type(string speaker, string text, bool requireContinue = true)
        {
            if (speaker.Equals("none"))
            {
                speakerBox.gameObject.SetActive(false);
            }
            else if (speaker != "")
            {
                speakerBox.GetComponentInChildren<Text>().text = speaker;
                speakerBox.gameObject.SetActive(true);
            }
            
            if (!isOpen)
                Open();

            textArea.text = "";

            isTyping = true;
            audioSource.clip = typeSound;

            //This needs updated... if it finds an opening tag, add new letters inside of closing tag until closing tag is found.
            int curLetterIndex = 0;

            while (curLetterIndex < text.Length && !cancelTyping)
            {
                textArea.text += text[curLetterIndex];
                curLetterIndex++;
                
                if (audioSource.clip != null)
                    audioSource.Play();

                if (curLetterIndex < text.Length)
                    yield return new WaitForSeconds(.04f);
                else 
                    break;
            }

            textArea.text = text;
            isTyping = false;
            cancelTyping = false;

            if (requireContinue)
            {
                yield return WaitForContinue();
                audioSource.clip = continueSound;
                if (audioSource.clip != null)
                    audioSource.Play();
            }
        }

        private IEnumerator WaitForContinue()
        {
            shouldContinue = false;
            while (!shouldContinue)
            {
                yield return null;
            }
            shouldContinue = false;
        }

        private bool shouldContinue;
    }
}