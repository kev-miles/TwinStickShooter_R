﻿using System;
using GameEvents;
using GameplayElements.Bullets;
using UniRx;

namespace GameplayElements.User
{
    public static class PlayerComposer
    {
        public static Player Compose(PlayerView view, IObserver<GameEvent> playerEventsObserver,
            EntityConfiguration configuration, BulletPool pool)
        {
            var playerSubject = new Subject<GameEvent>();
            var presenter = new PlayerPresenter(view, playerSubject, configuration, pool);
            var input = new PlayerInput(view, presenter, playerSubject);

            return new Player(view, presenter, input, playerEventsObserver, playerSubject);
        }
    }
}