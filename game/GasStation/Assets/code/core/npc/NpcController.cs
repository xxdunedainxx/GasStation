using UnityEngine;
using gasstation.code.core.objects;
using gasstation.code.core.logging;
using gasstation.code.core.item;

using gasstation.code.core.dialogue;
using gasstation.code.core.movement;
using System.Collections.Generic;
using System.Collections;
using System;

namespace gasstation.code.core.npc
{
    public class NpcController : GasStationChild
    {
        [SerializeField]
        Npc npcToControl;
        private bool chunkedMovement = false;
        private bool chunkedMovementLocked = false;
        private MovementChunk movementChunks = null;
        private Vector2 moveTowardsVector = Vector2.zero;
        private Action endVectorMovementCallback = null;
        public float speed = 1.5f;

        public override void AfterStart()
        {
            npcToControl.controller = this;
        }

        private IEnumerator ExecuteChunkedMovement()
        {
            if (this.movementChunks.IsComplete())
            {
                this.EndChunkedMovement();
            }
            else
            {
                this.LockChunkedMovement();
                this.Move(
                    x: this.movementChunks.XChunkSize,
                    y: this.movementChunks.YChunkSize
                );
                this.movementChunks.IterateChunk();
                yield return new WaitForSeconds(this.movementChunks.WaitTimeMilliseconds / 1000);
                this.UnlockChunkedMovement();
            }
            
        }

        public void FixedUpdate()
        {
            if (this.IsChunkedMovement() && !this.IsChunkedMovementLocked())
            {
                StartCoroutine(this.ExecuteChunkedMovement());
            }

            if (this.moveTowardsVector != Vector2.zero)
            {
                if (this.moveTowardsVector.x == this.npcToControl.transform.position.x 
                    && this.moveTowardsVector.y == this.npcToControl.transform.position.y)
                {
                    this.moveTowardsVector = Vector2.zero;
                    this.ProcessEndVectorMovementCallback();
                }
                else
                {
                    this.npcToControl.transform.position = Vector2.MoveTowards(this.npcToControl.transform.position, this.moveTowardsVector, speed * Time.deltaTime);
                }
            }
        }

        public void ProcessEndVectorMovementCallback()
        {
            this.endVectorMovementCallback();
            this.endVectorMovementCallback = null;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool IsChunkedMovement()
        {
            return this.chunkedMovement && this.movementChunks != null;
        }

        public void ChunkedMovement()
        {
            this.chunkedMovement = true;
        }

        public void EndChunkedMovement()
        {
            this.movementChunks.End();
            this.chunkedMovement = false;
            this.movementChunks = null;
        }

        private bool IsChunkedMovementLocked()
        {
            return this.chunkedMovementLocked;
        }

        private void LockChunkedMovement()
        {
            this.chunkedMovementLocked = true;
        }

        private void UnlockChunkedMovement()
        {
            this.chunkedMovementLocked = false;
        }

        /*
         * Determines Sprite orientation based on x / y directional 
         */
        public void DetermineSpriteDirection(float xDirection, float yDirection)
        {
            if(xDirection == 0 && yDirection != 0)
            {   
                // yDirectional Sprite
                if(yDirection > 0)
                {
                    // North Sprite
                    this.npcToControl.OrientNorthAnimated();
                }
                else
                {
                    // South Sprite
                    this.npcToControl.OrientSouthAnimated();
                }

            } else if(xDirection != 0 && yDirection == 0)
            {
                if(xDirection > 0)
                {
                    // east sprite
                    this.npcToControl.OrientEastAnimated();
                }
                else
                {
                    // west sprite
                    this.npcToControl.OrientWestAnimated();
                }
            } else
            {
                // No-op, we assume the directional sprite of the last prioritized axis direction
            }
        }

        // X and Y coordinate to go to
        public void Move(float x, float y)
        {
            this.npcToControl.transform.position = new Vector3 (
                this.transform.position.x + x,
                this.transform.position.y + y,
                0
            );
        }

        /*
         * Moves an NPC a percentage in the x/y direction of the current level. The percentage is based on the size of the current level's canvas size
         */
        public void MovePercent(float xPercent, float yPercent)
        {
            float xValueToMove = (GasStation.lvlWidth * xPercent);
            float yValueToMove = (GasStation.lvlHeight * yPercent);
            this.Move(xValueToMove, yValueToMove);
        }


        public void MoveChunkAtATime(float xPercentTotal, float yPercentTotal, float xPercentChunk, float yPercentChunk, Action endMovementCallback = null)
        {
            float xTotal = (GasStation.lvlWidth * xPercentTotal);
            float yTotal = (GasStation.lvlHeight * yPercentTotal);

            float xChunk = (GasStation.lvlWidth * xPercentChunk);
            float yChunk = (GasStation.lvlHeight * yPercentChunk);

            MovementChunk chunks = new MovementChunk(
                 xTotal: xTotal,
                 yTotal: yTotal,
                 xchunks: xChunk,
                 ychunks: yChunk,
                 endMovementCallback: endMovementCallback
            );

            this.MoveChunkAtATime(chunks);
        }

        public void MoveChunkAtATime(MovementChunk chunks)
        {
            this.movementChunks = chunks;
            this.DetermineSpriteDirection(chunks.xChunkTotal, chunks.yChunkTotal);
            this.ChunkedMovement();
        }

        public void RelevantVectorMove(float xDiff, float yDiff, Action endMovementCallback = null)
        {
            float xDirection = this.npcToControl.transform.position.x + xDiff;
            float yDirection = this.npcToControl.transform.position.y + yDiff;
            this.DetermineSpriteDirection(xDiff, yDiff);
            Vector2 vectorToMoveTo = new Vector2(xDirection, yDirection);
            this.MoveTowardsVector(vectorToMoveTo, endMovementCallback);
        }

        public void MoveTowardsVector(Vector2 v2MoveTo, Action endMovementCallback = null)
        {
            this.endVectorMovementCallback = endMovementCallback;
            this.moveTowardsVector = v2MoveTo;
        }
    }
}