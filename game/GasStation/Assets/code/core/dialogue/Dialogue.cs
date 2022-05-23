namespace gasstation.code.core.dialogue
{
    [System.Serializable]
    public class Dialogue
    {

        public string sentence;

        public Dialogue(string sentence)
        {
            this.sentence = sentence;
        }

        public virtual string Sentence()
        {
            DialogueManagement.GetDialogueManager().DisableYesNoButtons();
            return sentence;
        }
    }
}
