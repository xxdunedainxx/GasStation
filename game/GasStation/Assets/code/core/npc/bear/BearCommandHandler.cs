using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using gasstation.code.core.physics;
using gasstation.code.core.npc.bear;
using gasstation.code.core.player;
using gasstation.code.core.objects;
using gasstation.code.core.dialogue;

namespace gasstation.code.core.npc.bear
{

    public class BearHint
    {
        public string hint { get; }

        public static readonly string default_hint = "Bear looks at you with confusion";

        public static string DefaultHint()
        {
            return BearHint.default_hint;
        }

        public BearHint(string hint)
        {
            this.hint = hint;
        }

    }

    /* Processor for activated bear commands
     * Come, attack, stay, heel, help, break
     */
    public class BearCommandHandler : SingletonMonobehaviour
    {
        [SerializeField]
        private Canvas __bearCommandCanvas;
        [SerializeField]
        private Button __heelBtn;
        [SerializeField]
        private Button __comeBtn;
        [SerializeField]
        private Button __stayBtn;
        [SerializeField]
        private Button __closeCommandBtn;

        [SerializeField]
        private Bear __bearReference;
        [SerializeField]
        private GameObject __bearGameObjectReference;
        [SerializeField]
        private NpcController __bearController;
        [SerializeField]
        private GameObject playerReference;
        private Vector2 moveTowardsVector = Vector2.zero;
        private float speed = 1.5f;
        private bool heelActive = false;
        private Queue<BearHint> __hints = new Queue<BearHint>();

        public static BearCommandHandler GetBearCommandHandler()
        {
            return (BearCommandHandler)BearCommandHandler.getInstance("BearCommandHandler");
        }

        public static void QueueBearHint(BearHint hint)
        {
            BearCommandHandler.GetBearCommandHandler().AddToHintQueue(hint);
        }

        public static void ClearBearHints()
        {
            BearCommandHandler.GetBearCommandHandler().ClearHintQueue();
        }

        public void AddToHintQueue(BearHint hint)
        {
            this.__hints.Enqueue(hint);
        }

        public void ClearHintQueue()
        {
            this.__hints.Clear();
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        public bool IsCanvasOpen()
        {
            return this.__bearCommandCanvas.gameObject.activeSelf;
        }

        // Called After Parent 'Awake'
        public override void AfterStart()
        {
            this.__heelBtn.onClick.AddListener(this.Heel);
            this.__comeBtn.onClick.AddListener(this.Come);
            this.__stayBtn.onClick.AddListener(this.Stay);
            this.__closeCommandBtn.onClick.AddListener(this.DisableBearCommandCanvas);
            this.Stay();
        }

        // Update is called once per frame
        void Update()
        {
            if(this.moveTowardsVector != Vector2.zero)
            {
                this.__bearGameObjectReference.transform.position = Vector2.MoveTowards(this.__bearGameObjectReference.transform.position, this.moveTowardsVector, speed * Time.deltaTime);
            }

            if (this.heelActive && !this.__bearReference.CheckPlayerCollision())
            {
                this.Come();
            }
        }

        public void ShowBearCommandCanvas()
        {
            this.__bearCommandCanvas.gameObject.SetActive(true);
        }

        public void DisableBearCommandCanvas()
        {
            this.__bearCommandCanvas.gameObject.SetActive(false);
        }

        // bear comes over to you and will help if possible
        public void Help()
        {
            this.Come();
            string hintToPrint;

            if(this.__hints.Count > 0)
            {
                BearHint hint = this.__hints.Dequeue();
                hintToPrint = hint.hint;
            } else
            {
                hintToPrint = BearHint.DefaultHint();
            }

            DialogueManagement.GetDialogueManager().PrintSentence(hintToPrint);
        }

        /* Moves Bear a position 1/2 tile behind rod, depending on Rod's orientation (north/south/east/west)
         * Also utilized by heel (heel is contiuos come)
         */
        public void Come()
        {
            Transform pos1 = Player.GetPlayer().position;
            Transform pos2 = this.__bearReference.position;

            Player p = Player.GetPlayer();
            Bear b = this.__bearReference;

            // Get player position
            Vector2 playerPosition =  new Vector2(Player.GetPlayer().position.position.x, Player.GetPlayer().position.position.y);
            // Get Bear Current Position
            Vector2 bearPosition = new Vector2(this.__bearReference.position.position.x, this.__bearReference.position.position.y);
            this.UnfreezeBear();
            this.moveTowardsVector = this.playerReference.transform.position;
        }


        public void UnfreezeBear()
        {
            this.__bearReference.rb.constraints = RigidbodyConstraints2D.None;
            this.__bearReference.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public void Stay()
        {
            this.ClearCome();
            this.ClearHeel();
            this.__bearReference.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public void Attack()
        {

        }

        public void Heel()
        {
            this.heelActive = true;
        }

        public void Break()
        {
            this.ClearCome();
            this.ClearHeel();
        }

        public void ClearHeel()
        {
            this.heelActive = false;
        }

        public void ClearCome()
        {
            this.moveTowardsVector = Vector2.zero;
        }
    }
}