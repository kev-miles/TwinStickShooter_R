using GameplayElements.User;

namespace GameEvents.Player
{
    public class PowerUpEvent : GameEvent
    {
        public PowerUpEvent(string powerupName)
        {
            name = PlayerEventNames.GotPowerUp;
            parameters["Name"] = powerupName;
        }
    }
}