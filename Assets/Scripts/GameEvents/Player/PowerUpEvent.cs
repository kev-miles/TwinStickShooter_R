using GameplayElements.User;

namespace GameEvents.Player
{
    public class PowerUpEvent : GameEvent
    {
        public PowerUpEvent(string powerupName)
        {
            name = EventNames.GotPowerUp;
            parameters["Name"] = powerupName;
        }
    }
}