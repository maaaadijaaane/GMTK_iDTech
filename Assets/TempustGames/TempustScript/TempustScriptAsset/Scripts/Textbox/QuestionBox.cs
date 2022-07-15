using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TempustScript
{
    public class QuestionBox : MonoBehaviour
    {
        public GameObject template;

        private List<string> options;
        [SerializeField] private List<GameObject> optionObjects;
        private int selectedOption;
        public Transform pointer;

        private void Update()
        {
            MovePointer(selectedOption);
        }

        public int GetSelectedOption(bool closeBox)
        {
            if (closeBox)
                HideBox();
            return selectedOption;
        }

        public void MoveSelection(int direction)
        {
            selectedOption = selectedOption - direction;
            if (selectedOption >= optionObjects.Count)
            {
                selectedOption = 0;
            }
            else if (selectedOption < 0)
            {
                selectedOption = optionObjects.Count - 1;
            }

            MovePointer(selectedOption);
        }

        private void MovePointer(int position)
        {
            RectTransform box = GetComponent<RectTransform>();
            Vector2 pos = new Vector2(box.rect.x + 30, optionObjects[position].transform.localPosition.y);
            pointer.transform.localPosition = pos;
        }

        public void HideBox()
        {
            gameObject.SetActive(false);
        }

        public void ShowBox(List<string> options)
        {
            this.options = options;
            selectedOption = 0;
            MakeOptions();
            MovePointer(0);
            gameObject.SetActive(true);
        }

        private void MakeOptions()
        {
            foreach (GameObject option in optionObjects)
            {
                Destroy(option);
            }
            optionObjects.Clear();
            foreach (string option in options)
            {
                GameObject newOption = Instantiate(template, template.transform.parent);
                newOption.GetComponent<Text>().text = option;
                optionObjects.Add(newOption);
                newOption.SetActive(true);
            }
        }
    }
}