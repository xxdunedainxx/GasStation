namespace gasstation.code.core.npc
{
    public class Oscar: Npc
    {

        public override void AfterGasStationStart() 
        {
            base.AfterGasStationStart();
            this.controller.speed = .1f;
        }

        public static Oscar GetOscar()
        {
            return (Oscar)Oscar.getInstance("Oscar");
        }

        public override void OrientNorth()
        {
            this.animator.Play("OscarLookingBackwards");
        }

        public override void OrientSouth()
        {
            this.animator.Play("OscarLookingForward");
        }

        public override void OrientWest()
        {
            this.animator.Play("OscarLookingLeft");
        }

        public override void OrientEast()
        {
            this.animator.Play("OscarLookingLeft");
        }

        public override void OrientNorthAnimated()
        {
            this.animator.Play("OscarWalkingBackwards");
        }

        public override void OrientSouthAnimated()
        {
            this.animator.Play("OscarWalkingForward");
        }

        public override void OrientWestAnimated()
        {
            this.animator.Play("OscarWalkingLeftDefault");
        }

        public override void OrientEastAnimated()
        {
            this.animator.Play("OscarWalkingRight");
        }
    }
}
