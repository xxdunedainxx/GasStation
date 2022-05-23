using System;

namespace gasstation.code.core.movement
{
    public class MovementChunk
    {
        public float xChunkTotal;
        public float yChunkTotal;
        public float XChunkSize;
        public float YChunkSize;
        public float XCurrentChunk;
        public float YCurrentChunk;
        public float WaitTimeMilliseconds;
        private Action endMovementCallback;

        public MovementChunk(float xTotal, float yTotal, float xchunks, float ychunks, int waitTimeMilis = 1000, Action endMovementCallback = null)
        {
            this.xChunkTotal = xTotal;
            this.yChunkTotal = yTotal;

            this.XChunkSize = xchunks;
            this.YChunkSize = ychunks;

            this.XCurrentChunk = 1;
            this.YCurrentChunk = 1;
            this.WaitTimeMilliseconds = waitTimeMilis;
            this.endMovementCallback = endMovementCallback; 
        }

        public void End()
        {
            if (this.endMovementCallback != null)
            {
                this.endMovementCallback();
            }
        }

        public void IterateChunk()
        {
            this.XCurrentChunk += 1;
            this.YCurrentChunk += 1;
        }

        public bool IsComplete()
        {
            return ( (Math.Abs(this.XChunkSize * this.XCurrentChunk) >= Math.Abs(this.xChunkTotal)) && (Math.Abs(this.YChunkSize * this.YCurrentChunk) >= Math.Abs(this.yChunkTotal)) );
        }
    }
}
