using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace NoOO
{
    public sealed class GameManager : MonoBehaviour
    {
        // Collisions
        public readonly List<CollisionInfo> collisions = new List<CollisionInfo>();

        // Prefabs
        [SerializeField] GameObject startStatePrefab;
        [SerializeField] GameObject gameStatePrefab;
        [SerializeField] GameObject restartStatePrefab;
        [SerializeField] GameObject deathParticlePrefab;

        // Assets
        [SerializeField] Sprite asteroidBigSprite;
        [SerializeField] Sprite asteroidSmallSprite;
        [SerializeField] Sprite bulletSprite;
        [SerializeField] Sprite circleSprite;
        [SerializeField] Sprite shipSprite;

        // Menu management
        MenuState menuState;
        GameObject currentState;

        // Game state
        int numLives;
        int score;

        // Player
        GameObject player;
        SpriteRenderer playerRenderer;
        Vector2 playerVelocity;
        float lastShotTime;
        float playerSpawnTime;

        // Asteroid spawner
        bool hasRunInitialSpawn;
        float lastSpawnTime;

        // Heads up display
        Text scoreText;
        SpriteRenderer[] livesGuis;
        int lastScore;
        int lastLives;

        // Game over screen
        Text gameOverScoreText;

        // Bullets
        readonly List<Bullet> bullets = new List<Bullet>(30);

        // Asteroids
        readonly List<Asteroid> asteroids = new List<Asteroid>(100);
        readonly Dictionary<GameObject, Asteroid> goToAsteroid = new Dictionary<GameObject, Asteroid>(100);

        // Particles
        readonly List<ParticleSystem> particles = new List<ParticleSystem>(100);

        // Temp set of objects to destroy at the end of frame
        readonly HashSet<GameObject> objectsToDestroy = new HashSet<GameObject>();

        void Start()
        {
            //Create start menu
            currentState = Instantiate(startStatePrefab);
            menuState = MenuState.Start;
        }

        void Update()
        {
            if (menuState != MenuState.Game)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // Transition to the gameplay state
                    {
                        menuState = MenuState.Game;
                        Destroy(currentState);
                        currentState = Instantiate(gameStatePrefab);
                        numLives = 4;

                        scoreText = currentState.GetComponentInChildren<Text>();
                        livesGuis = currentState.GetComponentsInChildren<SpriteRenderer>();
                    }

                    // Initialize the player
                    {
                        player = new GameObject("Player");
                        playerRenderer = player.AddComponent<SpriteRenderer>();
                        var rigidbody = player.AddComponent<Rigidbody2D>();
                        var listener = player.AddComponent<CollisionListener>();

                        playerRenderer.sprite = shipSprite;
                        playerRenderer.sortingLayerID = SortingLayer.NameToID("Gameplay");
                        playerRenderer.color = new Color(1f, 1f, 1f, 0.25f);
                        rigidbody.isKinematic = true;
                        var collider = player.AddComponent<PolygonCollider2D>();
                        collider.isTrigger = true;
                        listener.Init(collisions);

                        player.transform.localPosition = new Vector3(50f, 50f, 0f);
                        player.transform.localRotation = Quaternion.identity;
                        player.layer = LayerMask.NameToLayer("Player");
                        player.tag = "Player";
                        playerSpawnTime = Time.time;
                        playerVelocity = Vector2.zero;
                    }

                    // Initialize spawner
                    {
                        hasRunInitialSpawn = false;
                        lastSpawnTime = Time.time;
                    }
                }

                if (menuState == MenuState.Restart)
                {
                    gameOverScoreText.text = score.ToString();
                }
            }
            else
            {
                // Update player velocity
                {
                    float aX = 0f, aY = 0f;

                    // Read inputs
                    if (Input.GetKey(KeyCode.W))
                    {
                        aY += 1f;
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        aX -= 1f;
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        aY -= 1f;
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        aX += 1f;
                    }

                    playerVelocity += new Vector2(aX, aY).normalized * 0.3f * Time.deltaTime;
                    if (playerVelocity.sqrMagnitude > 0.5f)
                    {
                        playerVelocity.Normalize();
                        playerVelocity *= 0.5f;
                    }
                }
                player.transform.localPosition += new Vector3(playerVelocity.x, playerVelocity.y, 0f);
                WrapBounds(player.transform);

                // Update player rotation
                {
                    float r = 0f;
                    bool isDirty = false;
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        r += 1f;
                        isDirty = true;
                    }
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        r -= 1f;
                        isDirty = true;
                    }

                    if (isDirty)
                    {
                        player.transform.Rotate(0f, 0f, r * 270f * Time.deltaTime);
                    }
                }

                // Update player's invincibility
                bool isPlayerInvincible = playerSpawnTime + 3f > Time.time;
                if (isPlayerInvincible)
                {
                    // Do a blink effect until the invulnerability period ends
                    float invulnerabilityTime = Time.time - playerSpawnTime;
                    if ((int)(invulnerabilityTime / 3f * 6) % 2 == 0)
                    {
                        playerRenderer.color = new Color(1f, 1f, 1f, 0.25f);
                    }
                    else
                    {
                        playerRenderer.color = new Color(1f, 1f, 1f, 0.75f);
                    }
                }
                else
                {
                    playerRenderer.color = Color.white;
                }

                // Shoot weapon
                if (lastShotTime + 0.25f < Time.time && Input.GetKey(KeyCode.Space))
                {
                    GameObject go = new GameObject("Bullet");
                    go.transform.localScale = Vector3.one * 0.5f;
                    go.transform.localPosition = player.transform.localPosition;
                    go.transform.localRotation = player.transform.localRotation;
                    go.layer = LayerMask.NameToLayer("Player");
                    go.tag = "Bullet";

                    var renderer = go.AddComponent<SpriteRenderer>();
                    var rigidbody = go.AddComponent<Rigidbody2D>();
                    var listener = go.AddComponent<CollisionListener>();
                    renderer.sprite = bulletSprite;
                    renderer.sortingLayerID = SortingLayer.NameToID("Gameplay");
                    rigidbody.isKinematic = true;
                    var collider = go.AddComponent<PolygonCollider2D>();
                    collider.isTrigger = true;
                    listener.Init(collisions);

                    bullets.Add(new Bullet()
                    {
                        go = go,
                        spawnTime = Time.time
                    });

                    lastShotTime = Time.time;
                }

                // Spawn asteroids
                {
                    int asteroidToSpawnCount = 0;
                    if (!hasRunInitialSpawn)
                    {
                        asteroidToSpawnCount += 3;
                        hasRunInitialSpawn = true;
                    }

                    if (lastSpawnTime + 3f < Time.time)
                    {
                        asteroidToSpawnCount++;
                    }

                    if (asteroidToSpawnCount > 0)
                    {
                        for (int i = 0; i < asteroidToSpawnCount; i++)
                        {
                            GameObject go = new GameObject("AsteroidBig");
                            var asteroid = new Asteroid()
                            {
                                go = go,
                                velocity = Random.Range(4f, 8f),
                                startRot = transform.up,
                                rotateScalar = Random.Range(0, 2) == 0 ? 1 : -1,
                                isBig = true,
                                scoreValue = 100
                            };

                            bool isTooCloseToPlayer = false;
                            Vector2 v;
                            do
                            {
                                v = new Vector2(Random.Range(0f, 100f), Random.Range(0f, 100f));

                                var offsetToPlayer = (v - new Vector2(player.transform.position.x, player.transform.position.y));
                                isTooCloseToPlayer = offsetToPlayer.sqrMagnitude < 5f;
                            }
                            while (isTooCloseToPlayer);

                            InitAsteroid(asteroid, asteroidBigSprite, v.x, v.y);
                        }

                        lastSpawnTime = Time.time;
                    }
                }

                // Update bullet positions
                foreach (var bullet in bullets)
                {
                    // Check if lifespan is over
                    if (bullet.spawnTime + 0.5f < Time.time)
                    {
                        objectsToDestroy.Add(bullet.go);
                    }

                    var t = bullet.go.transform;
                    t.localPosition += t.up * 120f * Time.deltaTime;
                    WrapBounds(t);
                }

                // Update asteroid positions
                foreach (var asteroid in asteroids)
                {
                    var t = asteroid.go.transform;
                    t.localPosition += t.up * asteroid.velocity * Time.deltaTime;
                    WrapBounds(t);
                }

                // Mark completed particles for deletion
                foreach (var particle in particles)
                {
                    if (!particle.isPlaying)
                    {
                        objectsToDestroy.Add(particle.gameObject);
                    }
                }

                // Handle collisions
                foreach (var collision in collisions)
                {
                    GameObject gameObjectToSpawnParticle = null;

                    // Handle collisions between bullets and asteroids
                    if (collision.source.tag == "Bullet" && collision.target.tag == "Asteroid")
                    {
                        gameObjectToSpawnParticle = collision.target;
                        objectsToDestroy.Add(collision.source);
                        objectsToDestroy.Add(collision.target);

                        var asteroid = goToAsteroid[collision.target];
                        score += asteroid.scoreValue;

                        if (asteroid.isBig)
                        {
                            // Make small asteroids
                            for (int i = 0; i < 4; i++)
                            {
                                GameObject go = new GameObject("AsteroidSmall");
                                var asteroidSmall = new Asteroid()
                                {
                                    go = go,
                                    velocity = Random.Range(12f, 16f),
                                    startRot = transform.up,
                                    rotateScalar = Random.Range(0, 2) == 0 ? 1 : -1,
                                    scoreValue = 150
                                };

                                InitAsteroid(asteroidSmall, asteroidSmallSprite,
                                    asteroid.go.transform.position.x, asteroid.go.transform.position.y);
                            }
                        }
                    }

                    // Handle player collisions
                    if(!isPlayerInvincible)
                    {
                        if (collision.source == player)
                        {
                            objectsToDestroy.Add(player);
                            gameObjectToSpawnParticle = player;
                        }
                        else if (collision.target == player)
                        {
                            objectsToDestroy.Add(player);
                            gameObjectToSpawnParticle = player;
                        }
                    }


                    // If a particle was requested, spawn it.
                    if(gameObjectToSpawnParticle != null)
                    {
                        var go = Instantiate(deathParticlePrefab);
                        go.transform.localPosition = gameObjectToSpawnParticle.transform.localPosition;
                        particles.Add(go.GetComponentInChildren<ParticleSystem>());
                    }
                }
                collisions.Clear();

                // Clean up destroyed objects at the end of the frame.
                if (objectsToDestroy.Contains(player))
                {
                    numLives--;

                    // Check for end of game condition
                    if (numLives <= 0)
                    {
                        Destroy(currentState);
                        menuState = MenuState.Restart;
                        currentState = Instantiate(restartStatePrefab);
                        gameOverScoreText = currentState.GetComponentInChildren<Text>();

                        Destroy(player);
                        player = null;

                        foreach (var bullet in bullets)
                        {
                            objectsToDestroy.Add(bullet.go);
                        }
                        foreach (var asteroid in asteroids)
                        {
                            objectsToDestroy.Add(asteroid.go);
                        }
                    }
                    else
                    {
                        // Reset the player's variables
                        player.transform.localPosition = new Vector3(50f, 50f, 0f);
                        player.transform.localRotation = Quaternion.identity;
                        playerRenderer.color = new Color(1f, 1f, 1f, 0.25f);
                        lastShotTime = 0f;
                        playerVelocity = Vector2.zero;
                        playerSpawnTime = Time.time;
                    }
                }
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (objectsToDestroy.Contains(bullets[i].go))
                    {
                        Destroy(bullets[i].go);
                        bullets.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < asteroids.Count; i++)
                {
                    if (objectsToDestroy.Contains(asteroids[i].go))
                    {
                        goToAsteroid.Remove(asteroids[i].go);
                        Destroy(asteroids[i].go);
                        asteroids.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < particles.Count; i++)
                {
                    if (objectsToDestroy.Contains(particles[i].gameObject))
                    {
                        Destroy(particles[i].transform.parent.gameObject);
                        particles.RemoveAt(i);
                        i--;
                    }
                }
                objectsToDestroy.Clear();

                // Update the heads up display
                if (lastLives != numLives)
                {
                    var lives = Mathf.Clamp(numLives - 1, 0, 3);
                    for (int i = 0; i < lives; i++)
                    {
                        livesGuis[i].gameObject.SetActive(true);
                    }
                    for (int i = lives; i < livesGuis.Length; i++)
                    {
                        livesGuis[i].gameObject.SetActive(false);
                    }
                    lastLives = numLives;
                }

                if (lastScore != score)
                {
                    scoreText.text = score.ToString();
                    lastScore = score;
                }
            }
        }

        private void InitAsteroid(Asteroid asteroid, Sprite sprite, float x, float y)
        {
            var renderer = asteroid.go.AddComponent<SpriteRenderer>();
            var rigidbody = asteroid.go.AddComponent<Rigidbody2D>();
            var listener = player.AddComponent<CollisionListener>();
            renderer.sprite = sprite;
            renderer.sortingLayerID = SortingLayer.NameToID("Gameplay");
            rigidbody.isKinematic = true;
            var collider = asteroid.go.AddComponent<PolygonCollider2D>();
            collider.isTrigger = true;
            listener.Init(collisions);
            asteroid.go.transform.localPosition = new Vector3(x, y, 0f);
            asteroid.go.transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            asteroid.go.layer = LayerMask.NameToLayer("Asteroid");
            asteroid.go.tag = "Asteroid";

            asteroids.Add(asteroid);
            goToAsteroid.Add(asteroid.go, asteroid);
        }

        private void WrapBounds(Transform t)
        {
            bool isDirty = false;
            float x = t.position.x;
            float y = t.position.y;
            if (x <= 0f)
            {
                x += 100f;
                isDirty = true;
            }
            if (x > 100f)
            {
                x -= 100f;
                isDirty = true;
            }
            if (y <= 0f)
            {
                y += 100f;
                isDirty = true;
            }
            if (y > 100f)
            {
                y -= 100f;
                isDirty = true;
            }

            if (isDirty)
            {
                t.position = new Vector3(x, y, 0f);
            }
        }
    }
}