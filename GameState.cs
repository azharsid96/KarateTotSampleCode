using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace NunchuckGame
{
    class GameState
    {
        float MaxComboTime = 0.5f;
        float ComboTimer = 0f;
        int ComboCount = 0;
        float pikcups_Speed=300;

        //sounds
        List<SoundEffect> allSounds;

        public void InitSounds(List<SoundEffect> allSounds)
        {
            this.allSounds = allSounds;
        }
        

        public void HandleEnemyDestroyed(Pickup pickup)
        {
            ComboTimer = MaxComboTime;
            ComboCount++;
            switch(ComboCount)
            {
                case 1:
                    break;
                case 2:
                   
                    allSounds[2].Play(volume:0.4f,pitch:0.2f,pan:0f);
                    break;
                case 3:

                    allSounds[2].Play(volume: 0.6f, pitch: 0.2f, pan: 0f);
                    break;
                case 4:

                    allSounds[2].Play(volume: 0.8f, pitch: 0.2f, pan: 0f);
                    break;
                default:
                    allSounds[2].Play(volume: 1f, pitch: 0.2f, pan: 0f);
                    break;
            }
            Score += 1 << (ComboCount - 1);
        }

        public void Update(GameTime gameTime, Rectangle playArea, ref MainPlayer player)
        {
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            ComboTimer -= (float)deltaTime;
            if (ComboTimer <= 0)
            {
                ComboCount = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ref MainPlayer player, GameTime gameTime)
        {
            if (ComboCount > 1) {
                
                float shakeCount = ComboTimer; 
                string comboUI = "x" + ComboCount.ToString() + " COMBO!";
                Vector2 comboPos = new Vector2(20, -60f);
                float shake = (float)((int)gameTime.TotalGameTime.TotalMilliseconds % 20);
                
                if (shake < 10)
                {
                    shake = 10 - shake % 10;
                }

                comboPos.Y += shake;

                spriteBatch.DrawString(font, comboUI, player.Position + comboPos, Color.Black, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            }
        }
        public void RestartGame()
        {
            IsGameOver = false;
            Enemies.Clear();
            Score = 0;
            ComboTimer = 0f;
            ComboCount = 0;
            EnemiesKilled = 0;
        }
    }
}
