using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gasstation.code.core.dialogue
{
    public class YesNoDialogue : Dialogue
    {
        public YesNoButtonConfig yesNoConfig;
        public YesNoDialogue(string sentence, YesNoButtonConfig config) : base(sentence)
        {
            this.yesNoConfig = config;
        }

        public override string Sentence()
        {
            DialogueManagement.GetDialogueManager().PrepareYesNoButtons(this.yesNoConfig);
            return this.sentence;
        }
    }
}
