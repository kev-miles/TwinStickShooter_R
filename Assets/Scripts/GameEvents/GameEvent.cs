﻿using System.Collections.Generic;

namespace GameEvents
{
    public class GameEvent
    {
        public string name;

        public Dictionary<string,string> parameters = new Dictionary<string, string>();

        public GameEvent(string name = "")
        {
            this.name = name;
        }
    }
}