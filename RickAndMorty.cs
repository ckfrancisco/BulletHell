using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BulletHell.GameObjects.Characters;
using BulletHell.GameObjects.Bullets;
using BulletHell.Controllers.CollisionControllers;
using BulletHell.Controllers.MovementControllers;
using BulletHell.GameObjects;
using BulletHell.Decorators.MovementDecorators;
using BulletHell.Controllers.ShotControllers;
using System;
using BulletHell.Decorators.ShotDecorators;
using BulletHell.Menus;

namespace BulletHell
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class RickAndMorty : Game
    {
        // Graphic Objects
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Game Enviroment
        Level level;
        Sidebar sidebar;
        WinLoss winloss;
        Stats stats;

        // Game Objects
        Player player;
        HashSet<Character> characters;
        HashSet<Bullet> bullets;

        // Screen Parameters
        public static int screenWidth;
        public static int screenHeight;
       
        // Wave Settings
        private int currWave = 0;
        private int currCharInWave = 0;
        private double waveStartTime = 0;
        private double midbossSpawn = 0;
        private double bossSpawn = 0;
        private List<Wave> activeWaves = new List<Wave>();
        private List<int> currCharInWaves = new List<int>();
        private List<double> charSpawnTimes = new List<double>();

        // Controllers
        public BaseCollisionController collisionController;
        public Dictionary<string, BaseMovementController> movementController;
        public Dictionary<string, BaseShotController> shotController;


        public RickAndMorty()
        {
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1500;

            characters = new HashSet<Character>();
            bullets = new HashSet<Bullet>();

            collisionController = new BaseCollisionController(ref characters, ref bullets);
       
            movementController = new Dictionary<string, BaseMovementController>();
            movementController.Add("Player", new PlayerMovementController());
            movementController.Add("Linear", new LinearMovementController());
            movementController.Add("Turn", new TurnMovementController());
            movementController.Add("Teleport", new TeleportMovementController());
            movementController.Add("Multi", new MultiPositionMovementController());
            movementController.Add("Circular", new CircularMovementController());

            shotController = new Dictionary<string, BaseShotController>();
            shotController.Add("Player", new PlayerShotController());
            shotController.Add("Linear", new LinearShotController());
            shotController.Add("Circular", new CircularShotController());
            shotController.Add("Curtain", new CurtainShotController());
            shotController.Add("Triangle", new TriangleShotController());
            shotController.Add("Multi", new MultipleShotController());
            shotController.Add("LinearX3", new LinearX3ShotController());


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            screenHeight = GraphicsDevice.Viewport.Height;
            screenWidth = GraphicsDevice.Viewport.Width;

            level = new Space(Content);
            level.SetUpWaves(Content);
            waveStartTime = 2.0f; //first wave start will always be 2.0f;
            activeWaves.Add(level.waves[0]); //always first waves
            currCharInWaves.Add(0); //always start on first character
            charSpawnTimes.Add(level.waves[0].delay.TotalSeconds);
            sidebar = new Sidebar(Content);
            winloss = new WinLoss(Content);
            stats = new Stats(Content);

            GameObject tmpG;
            Character tmpC;


            tmpG = new Player(Content, 1, new Vector2(Level.levelWidth / 2 - 100, Level.levelHeight - 100), new PlayerMovementDecorator(5), new PlayerShotDecorator(new TimeSpan(2000000)));
            player = tmpG as Player;
            tmpC = tmpG as Character;
            characters.Add(tmpG as Character);
            movementController["Player"].Add(ref tmpG);
            shotController["Player"].Add(ref tmpC);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            List<int> wavesToRemove = new List<int>();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(Keyboard.GetState().IsKeyDown(Keys.C))
            {
                collisionController.isCheating = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.V))
            {
                collisionController.isCheating = false;
            }

            if (gameTime.TotalGameTime.TotalSeconds >= 2.0f) // 2 second grace period
            {
                foreach (var wave in activeWaves)
                {
                    int index = activeWaves.IndexOf(wave);
                    if (currCharInWaves[index] != wave.characters.Count) //if we are not done with this current wave
                    {
                        if (gameTime.TotalGameTime.TotalSeconds - charSpawnTimes[index] >= wave.delay.Seconds) //spawn delay is done, time to spawn enemies
                        {
                            //add to characters array and correct controllers
                            Character tempChar = wave.characters[currCharInWaves[index]];
                            characters.Add(tempChar);
                            GameObject obj = (GameObject)tempChar;
                            movementController[tempChar.movement.controllerName].Add(ref obj);
                            if (tempChar.shot.shotDecorator != null)
                            {
                                shotController[tempChar.shot.shotDecorator.controllerName].Add(ref tempChar);
                                //add the shotDecorator movement to the controller as well?
                                GameObject shotManager = tempChar.shot;
                                movementController[tempChar.shot.movement.controllerName].Add(ref shotManager);
                            }

                            currCharInWaves[index]++;
                            charSpawnTimes[index] = gameTime.TotalGameTime.TotalSeconds;
                            if (currCharInWave == 2)
                            {
                                tempChar.health++;
                            }
                        } //else dont add character
                    }
                    else if (wave.characters[currCharInWaves[index] - 1] is MillionAnts || wave.characters[currCharInWaves[index] - 1] is Supernova)//special boss case
                    {
                        Character boss = wave.characters[currCharInWaves[index] - 1];
                        double timer = boss is MillionAnts ? midbossSpawn : bossSpawn;
                        //check if their timer is up, if it is, then kill
                        if (gameTime.TotalGameTime.TotalSeconds - timer > wave.waveDelay.Seconds)
                        {
                            //timer is up, remove them
                            boss.Position = new Vector2(-400, -400);
                            wavesToRemove.Add(index);
                        }
                    }
                    else //remove this wave, its done
                    {
                        wavesToRemove.Add(index);
                        
                    }
                }
                //foreach (int waveIndex in wavesToRemove)
                //{
                //    activeWaves.RemoveAt(waveIndex);
                //    currCharInWaves.RemoveAt(waveIndex);
                //    charSpawnTimes.RemoveAt(waveIndex);
                //}
                //check to add more waves to active waves
                int i = activeWaves.Count - 1; //latest wave to be added to this list
                if( gameTime.TotalGameTime.TotalSeconds - waveStartTime >= activeWaves[i].waveDelay.TotalSeconds && currWave < level.waves.Count) //delay is done, time to add a new wave to active waves
                {
                    currWave++;

                    Stats.updateLevel();

                    if (currWave < level.waves.Count)
                    {
                        activeWaves.Add(level.waves[currWave]);
                        currCharInWaves.Add(0);
                        charSpawnTimes.Add(0);
                        waveStartTime = gameTime.TotalGameTime.TotalSeconds;
                        if (level.waves[currWave].characters[0] is MillionAnts)
                        {
                            midbossSpawn = waveStartTime;
                        }
                        else if (level.waves[currWave].characters[0] is Supernova)
                        {
                            bossSpawn = waveStartTime;
                        }
                    }

                }

                if( currWave >= level.waves.Count && (gameTime.TotalGameTime.TotalSeconds - waveStartTime >= activeWaves[i].waveDelay.TotalSeconds || characters.Count == 1))
                {
                    //done, print win!
                    //print victory and end
                    if (!collisionController.isPlayerDead)
                    {
                        winloss.message = "YOU'VE WON";//Menu's need to be better setup. 

                        collisionController.isPlayerDead = true;
                    }
                }

            }
           
            collisionController.DetectCollisions();
           

            foreach (var controller in movementController)
                controller.Value.Move(gameTime);

            List<Bullet> newBullets = new List<Bullet>();
            foreach (var controller in shotController)
                newBullets.AddRange(controller.Value.Shoot(Content, gameTime, player.Position));

            foreach (var bullet in newBullets)
            {
                bullets.Add(bullet);

                GameObject bulletGameObject = bullet as GameObject;
                if(bullet.movement != null)
                    movementController[bullet.movement.controllerName].Add(ref bulletGameObject);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (collisionController.isPlayerDead)
            {
                level.Draw(spriteBatch);
                winloss.Draw(spriteBatch, GraphicsDevice);
                sidebar.Draw(spriteBatch);
                stats.Draw(spriteBatch, GraphicsDevice);
                spriteBatch.End();
            }
            else
            {
                level.Draw(spriteBatch);

                foreach (var character in characters)
                {
                    character.Draw(spriteBatch);
                }

                foreach (var bullet in bullets)
                {
                    bullet.Draw(spriteBatch);
                }

                sidebar.Draw(spriteBatch);
                stats.Draw(spriteBatch, GraphicsDevice);
                spriteBatch.End();
                base.Draw(gameTime);
            }
        }
    }
}
