using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using gasstation.code.core.logging;
using gasstation.code.core.objects;
using gasstation.code.core.constants;
using gasstation.code.gameplay.inventory;
using gasstation.code.core.npc.bear;

namespace gasstation.code.core.player
{
    public class PlayerController : GasStationChildSingleton
    {
        public Transform feet;
        public Rigidbody2D rb;
        public LayerMask groundLayer;
        public bool sideScrolling = true;
        public bool goingUpStairs = false;
        public bool goingDownStairs = false;
        public float currentPositionX;
        public float currentPositionY;
        public float movementSpeed = 10f;
        public float jumpForce = 5f;
        public Action TButtonDownAction = null;
        public Action TButtonUpAction = null;
        private bool playerFreeze = false;
        private Player playerRef;
        private float upSlope = 0;
        private float downSlope = 0;

        public static PlayerController GetPlayerController()
        {
            return (PlayerController)PlayerController.getInstance("PlayerController");
        }

        public override void AfterGasStationStart()
        {
            LogFactory.INFO("Start PlayerController");
            this.groundLayer = Layers.GROUND_LAYER;
        }

        public void DefaultTButton()
        {
            LogFactory.INFO("they pressed T, lol");
        }

        private void Start()
        {
            this.TButtonDownAction = this.DefaultTButton;
            this.TButtonUpAction = this.DefaultTButton;
            this.playerRef = Player.GetPlayer();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (goingDownStairs)
                {
                    this.ApplyUpStairs(this.upSlope, this.downSlope);
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (goingUpStairs)
                {
                    this.ApplyDownStairs(this.downSlope, this.upSlope);
                }
            }

            if (AllowPlayerMovement() == true)
            {
                currentPositionX = Input.GetAxisRaw("Horizontal");
                currentPositionY = Input.GetAxisRaw("Vertical");
                if (sideScrolling && Input.GetKeyDown(KeyCode.Space) && IsGrounded())
                {
                    Jump();
                }
            }

            if (Input.GetKeyDown(KeyCode.I) && InventoryController.GetInventoryController().IsReady())
            {
                if (InventoryController.GetInventoryController().isOpen)
                {
                    LogFactory.INFO("Closing inventory...");
                    InventoryController.GetInventoryController().Close();
                }
                else
                {
                    LogFactory.INFO("Opening inventory...");
                    InventoryController.GetInventoryController().Open();
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                this.TButtonDownAction();
            }
            else if (Input.GetKeyUp(KeyCode.T))
            {
                this.TButtonUpAction();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                if (BearCommandHandler.GetBearCommandHandler().IsCanvasOpen())
                {
                    BearCommandHandler.GetBearCommandHandler().DisableBearCommandCanvas();
                }
                else
                {
                    BearCommandHandler.GetBearCommandHandler().ShowBearCommandCanvas();
                }
            }
        }


        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "Slope")
            {
                //this.DisableDownStairs();
                //this.DisableUpStairs();
            }
        }

        public void DisableUpStairs()
        {
            this.upSlope = 0;
            goingUpStairs = false;
            Player.GetPlayer().body.gravityScale = 1;
        }

        public void DisableDownStairs()
        {
            this.downSlope = 0;
            goingDownStairs = false;
            Player.GetPlayer().body.gravityScale = 1;
        }

        public void ApplyUpStairs(float upSlope, float downSlope)
        {
            this.upSlope = upSlope;
            this.downSlope = downSlope;
            goingUpStairs = true;
            Player.GetPlayer().body.gravityScale = 0;
        }

        public void ApplyDownStairs(float downSlope, float upSlope)
        {
            this.downSlope = downSlope;
            this.upSlope = upSlope;
            goingDownStairs = true;
            Player.GetPlayer().body.gravityScale = 0;
        }

        private void Jump()
        {
            Vector2 movement = new Vector2(rb.velocity.x, this.jumpForce);

            rb.velocity = movement;
        }

        public bool IsGrounded()
        {
            Collider2D groundChecking = Physics2D.OverlapCircle(this.feet.position, 0.1f, this.groundLayer);

            if (groundChecking != null)
            {
                return true;
            }
            return false;
        }

        private float ApplyStairsUp()
        {
            if (currentPositionX != 0)
            {
                return (1);
            }
            else
            {
                return 0;
            }
        }

        private float ApplyStairsDown()
        {
            if (currentPositionX != 0)
            {
                return (-2f);
            }
            else
            {
                return 0;
            }
        }

        private void FixedUpdate()
        {
            if (AllowPlayerMovement() == false)
            {
                return;
            }
            float xValue = currentPositionX * this.movementSpeed;
            float yValue = sideScrolling ? rb.velocity.y : currentPositionY * this.movementSpeed;
            if (goingUpStairs)
            {
                xValue = currentPositionX;
                yValue = ApplyStairsUp();
            }
            else if (goingDownStairs)
            {
                yValue = ApplyStairsDown();
                xValue = currentPositionX;
            }
            Vector2 movement = new Vector2(xValue, yValue);  //rb.velocity.y  my * movementSpeed

            rb.velocity = movement;
        }

        public bool AllowPlayerMovement()
        {
            return this.playerFreeze == false;
        }

        public void FreezePlayer()
        {
            this.playerFreeze = true;
        }

        public void UnFreezePlayer()
        {
            this.playerFreeze = false;
        }

    }

}