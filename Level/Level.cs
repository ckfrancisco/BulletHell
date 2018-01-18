using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using BulletHell.Decorators.MovementDecorators;
using BulletHell.Decorators.ShotDecorators;
using BulletHell.GameObjects.Characters;
using Newtonsoft.Json.Linq;
using System;

namespace BulletHell
{
    public abstract class Level
    {
        public string texturePath;

        public Texture2D texture;
        public Vector2 position;

        public const int levelWidth = 1000;
        public const int levelHeight = 1000;

        public List<Wave> waves;
        public List<Wave> waves2;

        public Level(ContentManager content)
        {
            position = new Vector2(0);
            waves = new List<Wave>();
        }

        public void Initialize(ContentManager content)
        {
            texture = content.Load<Texture2D>(texturePath);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(0, 0, levelWidth, levelHeight), null, Color.White);
        }

        public void SetUpWaves(ContentManager content)
        {
            List<Character> characters;

            using (StreamReader file = File.OpenText("..\\..\\..\\..\\Level\\Enemies.json"))
            {
                string json = file.ReadToEnd();
                JArray enemies = JArray.Parse(json);
                foreach (JObject enemy in enemies)
                {
                    characters = new List<Character>();
                    string type; //meeseeks, gromfly, boss, etc.
                    int health = 0; //obvious;
                    BaseMovementDecorator movementDecorator;
                    BaseShotDecorator shotDecorator;

                    type = enemy["Enemy"].Value<string>();
                    health = enemy["Health"].Value<int>();
                    Vector2 startLocation = enemy["StartLocation"].ToObject<Vector2>();
                    int amount = enemy["Amount"].Value<int>();
                    int delay = enemy["Delay"].Value<int>();
                    int offShot = enemy["OffShot"].Value<int>();

                    int waveDelay;
                    try
                    {
                        waveDelay = enemy["WaveDelay"].Value<int>();
                    }
                    catch (Exception ex)
                    {
                        waveDelay = delay * amount;
                    }
                    for (int i = 0; i < amount; i++)
                    {
                        string movementType = enemy["Movement"]["Type"].Value<string>(); //todo use this to determine type of movement
                                                                                         //there might be a way to create an object that matches
                                                                                         //this string value. If you find this out it would cut out
                                                                                         //the need for 3 switch statements. 3 swtiches is the
                                                                                         //easy way out tho, but its gross
                                                                                         //this might help https://stackoverflow.com/questions/223952/create-an-instance-of-a-class-from-a-string
                        switch (movementType)//against Dr. Peters orders. #Git Blame Tony
                        {
                            case "Turn":
                                movementDecorator = enemy["Movement"].ToObject<TurnMovementDecorator>(); //Meeseel
                                break;
                            case "Multi":
                                movementDecorator = enemy["Movement"].ToObject<MultiPositionMovementDecorator>(); //Gromflomite, MillionAnts
                                break;
                            case "Teleport":
                                movementDecorator = enemy["Movement"].ToObject<TeleportMovementDecorator>(); // Supernova
                                break;
                            case "Linear":
                                movementDecorator = enemy["Movement"].ToObject<LinearMovementDecorator>();
                                break;
                            default:
                                throw new FileLoadException("Not correct parameters");
                        }

                        string shotType = enemy["Shot"]["Type"].Value<string>(); //todo use this to determine type of shot
                        switch (shotType)
                        {
                            case "Linear":
                                shotDecorator = enemy["Shot"].ToObject<LinearShotDecorator>(); //shot freq might be messed up? its reading it in fine
                                                                                               //but idk if the value matches what we were doing before
                                                                                               //Meeseek
                                break;
                            case "Circular":
                                shotDecorator = enemy["Shot"].ToObject<CircularShotDecorator>(); //Milionants
                                break;
                            case "Curtain":
                                shotDecorator = enemy["Shot"].ToObject<CurtainShotDecorator>();          //Gromflomite,  Seperator  
                                break;
                            case "Triangle":
                                shotDecorator = enemy["Shot"].ToObject<TriangleShotDecorator>();          //Gromflomite,  Seperator  
                                break;
                            case "LinearX3":
                                shotDecorator = enemy["Shot"].ToObject<LinearX3ShotDecorator>();
                                break;
                            case "None":
                                shotDecorator = null;
                                break;
                            case "Multiple":
                                int j = 0;
                                List<BaseShotDecorator> allShots = new List<BaseShotDecorator>();
                                foreach (var dummy in enemy["Shot"]["Shots"])
                                {
                                    allShots.Add(this.getDecorator((string)enemy["Shot"]["Shots"][j]["Type"], enemy, j++));
                                }
                                shotDecorator = new MultipleShotDecorator(allShots, (TimeSpan)enemy["Shot"]["ShotFreq"]);
                                break;
                            default:
                                throw new FileLoadException("Not correct parameters");
                        }

                        switch (type)
                        {
                            case "Meeseek":
                                characters.Add(new Meeseek(content, health, startLocation, movementDecorator, shotDecorator, offShot)); //todo obviously not always gunna be meeseeks
                                break;
                            case "Gromflomite":
                                characters.Add(new Gromflomite(content, health, startLocation, movementDecorator, shotDecorator, offShot));
                                break;
                            case "MillionAnts":
                                characters.Add(new MillionAnts(content, health, startLocation, movementDecorator, shotDecorator, offShot));
                                break;
                            case "Supernova":
                                characters.Add(new Supernova(content, health, startLocation, movementDecorator, shotDecorator, offShot));
                                break;
                        }
                    }
                    System.TimeSpan ts = System.TimeSpan.FromTicks(delay);
                    System.TimeSpan waveTs = System.TimeSpan.FromTicks(waveDelay);
                    waves.Add(new Wave(characters, ts, waveTs));
                }
            }
        }

        public BaseShotDecorator getDecorator(string name, JObject enemy, int i)
        {
            BaseShotDecorator shotDecorator = null;

            switch (name)
            {
                case "Linear":
                    shotDecorator = enemy["Shot"]["Shots"][i].ToObject<LinearShotDecorator>();
                    break;
                case "Circular":
                    shotDecorator = enemy["Shot"]["Shots"][i].ToObject<CircularShotDecorator>(); //Milionants
                    break;
                case "Curtain":
                    shotDecorator = enemy["Shot"]["Shots"][i].ToObject<CurtainShotDecorator>();          //Gromflomite,  Seperator  
                    break;
                case "Triangle":
                    shotDecorator = enemy["Shot"]["Shots"][i].ToObject<TriangleShotDecorator>();          //Gromflomite,  Seperator  
                    break;
                case "LinearX3":
                    shotDecorator = enemy["Shot"]["Shots"][i].ToObject<LinearX3ShotDecorator>();
                    break;
                case "None":
                    shotDecorator = null;
                    break;
            }
            return shotDecorator;
        }
    }
}