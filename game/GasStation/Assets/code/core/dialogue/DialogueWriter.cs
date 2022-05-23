using gasstation.code.core.objects;
using gasstation.code.core.logging;
using gasstation.code.core.constants;

using UnityEngine;
using UnityEngine.UI;


namespace gasstation.code.core.dialogue
{
    public class DialogueWriter : SingletonMonobehaviour
    {
        [SerializeField]
        public Text textBox;
        public static float reasonableWriterSpeed = 0.1f;
        private float timer;
        private float timePerCharacter = DialogueWriter.reasonableWriterSpeed;
        private int characterIndex = 0;
        private bool isWriting = false;
        private bool writeToEnd = false;
        private bool autoCompletedLast = false;
        public string sentenceToPrint = null;
        public static bool isReady = false;

        private void Update()
        {
            if (textBox != null && this.ValidSentence())
            {
                if (writeToEnd == true)
                {
                    this.textBox.text = this.sentenceToPrint;
                    this.UnsetVars();
                    this.autoCompletedLast = true;
                }
                else
                {
                    timer -= Time.deltaTime;
                    if (timer <= 0f)
                    {
                        this.textBox.text = this.sentenceToPrint.Substring(0, characterIndex);
                        timer += timePerCharacter;
                        characterIndex++;
                    }

                    if (characterIndex > this.sentenceToPrint.Length)
                    {
                        this.UnsetVars();
                    }

                }
            }
        }

        public void ClearTextBox()
        {
            this.textBox.text = "";
        }

        public bool ValidSentence()
        {
            return this.sentenceToPrint != null && this.sentenceToPrint != "";
        }

        public void UnsetVars()
        {
            LogFactory.INFO("unsetting vars");
            this.characterIndex = 0;
            this.sentenceToPrint = null;
            this.isWriting = false;
            this.writeToEnd = false;
        }

        public void SetToEnd()
        {
            this.writeToEnd = true;
        }

        public bool IsWriting()
        {
            LogFactory.INFO($"DialogueWriter is writing {this.isWriting}");
            return this.isWriting;
        }

        public bool AutoCompletedLast()
        {
            return this.autoCompletedLast;
        }

        public void UnsetAutoComplete()
        {
            this.autoCompletedLast = false;
            this.UnsetVars();
        }

        public void PrintSentence(string sentence, float writeTimer)
        {
            this.UnsetVars();
            this.timePerCharacter = writeTimer;
            LogFactory.INFO("printing sentence???");
            this.sentenceToPrint = sentence;
            this.isWriting = true;
            this.Update();
        }

        public override void AfterStart()
        {
            DialogueWriter.isReady = true;
        }

    }
}
