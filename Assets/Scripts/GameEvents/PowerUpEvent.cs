using GameplayElements.User;

namespace GameEvents
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