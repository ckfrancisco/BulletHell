using System;
using System.Collections.Generic;
using BulletHell.GameObjects.Characters;
using BulletHell.Decorators.MovementDecorators;
using BulletHell.Decorators.ShotDecorators;

namespace BulletHell
{
    public class Wave
    {
        public List<Character> enemies;

        private int id;
        public List<Character> characters;
        public TimeSpan delay; //amount of time before next enemy in curr wave spawns
        public TimeSpan waveDelay; //amount of time before next wave starts

        public Wave(List<Character> characters, TimeSpan delay, TimeSpan waveDelay)
        {
            this.characters = characters;
            this.delay = delay;
            this.waveDelay = waveDelay;
        }
    }
}
