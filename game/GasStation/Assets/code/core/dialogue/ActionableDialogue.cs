using System;

namespace gasstation.code.core.dialogue
{
    [System.Serializable]
    public class ActionableDialogue : Dialogue
    {
        private Action actionToRun;

        public ActionableDialogue(string sentence, Action actionToRun) : base(sentence)
        {
            this.actionToRun = actionToRun;
        }

        public override string Sentence()
        {
            this.actionToRun();
            return base.Sentence();
        }
    }
}
