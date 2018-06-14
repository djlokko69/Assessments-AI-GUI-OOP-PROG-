using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWarehouse
{
    [RequireComponent(typeof(CharacterController))]

    [AddComponentMenu("RPG/Character Handler")]

    public class FinalCharacter : MonoBehaviour
    {
        #region Variables
        [Header("CHAR HAND")]
        [Space(5)]
        [Header("General")]
        #region H.S.M
        // Health, Stamina, Mana
        public bool liv; // if alive
        public int maxHealth, maxStamina, maxMana;// Maxium
        public float curHealth, curStamina, curMana;// Current
        #endregion
        [Space(3)]
        [Header("Player Stats")]
        #region Stats 
        public int strength;
        public int intelligence;
        public int charisma;
        public int constitution;
        public int dexterity;
        public int wisdom;
        #endregion
        [Space(3)]
        [Header("Levels and Exp")]
        #region Level and Exp
        public int level;
        public int curExp, maxExp;
        #endregion
        [Space(3)]
        [Header("Weapon Stats")]
        #region Weapon
        public int weaponDamage;
        public int ammo, ammoUsed;
        public float coolDown; // WaitTime
        public bool isAttack, isEquipped;

        #endregion
        [Space(3)]
        [Header("Movement Connect")]
        #region Movement Connect
        public Vector3 moveD = Vector3.zero; // move direction
        public CharacterController charCo;

        #endregion
        int damage = 10;
        [Space(3)]
        [Header("Move Variables")]
        #region Movement Var
        // Speeds/Gravity
        public float jumpSped = 8.0f;
        public float speedy = 6.0f;
        public float walkSped = 6, crouchSped = 2, sprintSped = 20;
        public float grav = 21.0f;
        #endregion
        [Space(3)]
        [Header("Camera Connect")]
        #region MiniMap
        public RenderTexture miniMap;
        public Camera minMapCamera;

        public enum RotateAxis
        {
            MouseXAndY = 0,
            MouseX = 1,
            MouseY = 2
        }
        public RotateAxis axis = RotateAxis.MouseX;

        public float sensX = 15f;
        public float sensY = 15f;
        public float miniY = -60f;
        public float maxiY = 60f;
        float rotatY = 0;

        #endregion
        [Space(3)]
        [Header("Animations")]
        #region Animator
        public Animator anime;
        #endregion
        [Space(3)]
        [Header("Check Point Elements")]
        #region CheckPoints
        public GameObject curCheckPoint;
        #endregion
        [Space(3)]
        [Header("Textures")]
        #region Textures
        public Renderer meshRenderer;
        public GUIStyle healthColor, manaColor, staminaColor, expColor, bar, backGround, mapBorder;
        #endregion
        #endregion
        #region Awake
        void Awake()
        {
            #region Auto
            charCo = this.GetComponent<CharacterController>();

            minMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
            meshRenderer = GameObject.FindGameObjectWithTag("PlayMesh").GetComponent<Renderer>();
            miniMap = Resources.Load("MiniMap") as RenderTexture;
            //minMapCamera.targetTexture = miniMap;
            anime = this.GetComponent<Animator>();

            #endregion
            #region Stat
            strength = PlayerPrefs.GetInt("Strength", 9);
            charisma = PlayerPrefs.GetInt("Charisma", 9);
            dexterity = PlayerPrefs.GetInt("Dexterity", 9);
            constitution = PlayerPrefs.GetInt("Constituion", 10);
            intelligence = PlayerPrefs.GetInt("Intelligence", 9);
            wisdom = PlayerPrefs.GetInt("Wisdom", 9);
            level = PlayerPrefs.GetInt("Level", 0);
            curExp = PlayerPrefs.GetInt("CurrentExp", 0);
            maxExp = PlayerPrefs.GetInt("MaxExp", 60);
            maxHealth = PlayerPrefs.GetInt("MaxHealth", 50 + constitution * 2);
            curHealth = PlayerPrefs.GetInt("CurrentHealth", maxHealth);
            maxStamina = PlayerPrefs.GetInt("MaxStamina", 25 + dexterity * 2);
            curStamina = PlayerPrefs.GetInt("CurrentStamina", maxStamina);
            maxMana = PlayerPrefs.GetInt("MaxMana", 25 + intelligence * 2);
            curMana = PlayerPrefs.GetInt("CurrentMana", maxMana);
            curCheckPoint = GameObject.Find(PlayerPrefs.GetString("SpawnPoint"));
            #endregion
        }
        #endregion
        #region Start
        void Start()
        {
            #region RigidBody
            if (this.GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().freezeRotation = true;
            }
            if (curCheckPoint != null)
            {
                this.transform.position = curCheckPoint.transform.position;
            }
            #endregion
        }
        #endregion
        #region Updates
        #region Update
        // Update is called once per frame
        void Update()
        {
            Movement();
            //MouseLookie();
            //CheckPoint();
            ExpHandler();
            InteractHandler();
        }
        #endregion
        #region LateUpdate
        void LateUpdate()
        {
            HealthCap();
            ManaCap();
            StaminaCap();
            #region Anime
            if (anime.GetBool("Jump") == true)
            {
                anime.SetBool("Jump", false);
            }
            if (anime.GetBool("Crawl") == true)
            {
                anime.SetBool("Crawl", false);
            }
            if (anime.GetBool("Attack") == true)
            {
                anime.SetBool("Attack", false);
            }
            if (anime.GetBool("Attack2") == true)
            {
                anime.SetBool("Attack2", false);
            }

            #endregion
        }
        #endregion
        #endregion
        #region Movement
        void Movement()
        {

            if (charCo.isGrounded)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speedy = sprintSped;

                }
                else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
                {
                    speedy = crouchSped;
                    anime.SetBool("Crawl", true);

                }
                else
                {
                    speedy = walkSped;

                }

                moveD = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveD = transform.TransformDirection(moveD);
                moveD *= speedy;
                if (Input.GetButtonDown("Jump"))
                {
                    moveD.y = jumpSped;
                    anime.SetBool("Jump", true);

                }

                if (Input.GetAxis("Horizontal") > 0.1 || Input.GetAxis("Horizontal") < -0.1 || Input.GetAxis("Vertical") > 0.1 || Input.GetAxis("Vertical") < -0.1)
                {
                    anime.SetBool("Moving", true);
                }
                else
                {
                    anime.SetBool("Moving", false);
                }
                #region Attack
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    anime.SetBool("Attack", true);
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    anime.SetBool("Attack2", true);
                }
                #endregion
            }

            moveD.y -= grav * Time.deltaTime;
            charCo.Move(moveD * Time.deltaTime);
        }
        #endregion
        #region CheckPoint
        /*void CheckPoint()
        {
            #region Update
            if (curHealth == 0)
            {
                this.transform.position = curCheckPoint.transform.position;
                curHealth = maxHealth;

            }
            #endregion
        }*/
        #endregion
        #region OnTriggerEnter
        void OnTriggerEnter(Collider other)
        {


            if (other.gameObject.tag == "Enemy")
            {

                curHealth -= damage;

                if (curHealth == 0)
                {
                    curMana = 0;
                    curStamina = 0;
                    anime.SetBool("Death", true);
                }


            }
        }
        #endregion
        #region OnGUI
        void OnGUI()
        {

            float sW = Screen.width / 16;
            float sH = Screen.height / 9;
            #region HEALTH
            GUI.Box(new Rect(2 * sW, 0.25f * sH, 4 * sW, 0.25f * sH), "");
            GUI.Box(new Rect(2 * sW, 0.25f * sH, curHealth * (4 * sW) / maxHealth, 0.25f * sH), "", healthColor);

            #endregion
            #region MANA
            GUI.Box(new Rect(2 * sW, 0.55f * sH, 4 * sW, 0.25f * sH), "");
            GUI.Box(new Rect(2 * sW, 0.55f * sH, curMana * (4 * sW) / maxMana, 0.25f * sH), "", manaColor);
            #endregion
            #region STAMINA
            GUI.Box(new Rect(2 * sW, 0.85f * sH, 4 * sW, 0.25f * sH), "");
            GUI.Box(new Rect(2 * sW, 0.85f * sH, curStamina * (4 * sW) / maxStamina, 0.25f * sH), "", staminaColor);
            #endregion
            #region EXP
            GUI.Box(new Rect(2 * sW, 1.15f * sH, 4 * sW, 0.25f * sH), "");
            GUI.Box(new Rect(2 * sW, 1.15f * sH, curExp * (4 * sW) / maxExp, 0.25f * sH), "", expColor);
            #endregion
            #region MINIMAP
            GUI.Box(new Rect(0.02f * sW, 6.35f * sH, 4.17f * sW, 2.7f * sH), "");
            GUI.DrawTexture(new Rect(0.10f * sW, 6.5f * sH, 4 * sW, 2.5f * sH), miniMap);
            #endregion


        }
        #endregion
        #region CAPS/Called from LateUpate
        // Health/Mana/Stamina
        void HealthCap()
        {

            if (curHealth > maxHealth) { curHealth = maxHealth; }
            if (curHealth <= 0) { curHealth = 0; }
        }
        void ManaCap()
        {
            if (curMana > maxMana) { curMana = maxMana; }
            if (curMana <= 0) { curMana = 0; }
        }
        void StaminaCap()
        {
            if (curStamina > maxStamina) { curStamina = maxStamina; }
            if (curStamina <= 0) { curStamina = 0; }
        }
        #endregion
        #region ExpHandler
        void ExpHandler()
        {
            if (curExp >= maxExp)
            {
                curExp -= maxExp;
                level++;
                maxExp += 50;
            }
        }
        #endregion
        #region MouseLookie
        /*void MouseLookie()
        {
            if (axis == RotateAxis.MouseXAndY)
            {
                float rotatX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensX;

                rotatY += Input.GetAxis("MouseY") * sensY;
                rotatY = Mathf.Clamp(rotatY, miniY, maxiY);

                transform.localEulerAngles = new Vector3(-rotatY, rotatX, 0);
            }
            else if (axis == RotateAxis.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensX, 0);
            }
            else
            {
                rotatY += Input.GetAxis("Mouse") * sensY;
                rotatY = Mathf.Clamp(rotatY, miniY, maxiY);
                transform.localEulerAngles = new Vector3(-rotatY, transform.localEulerAngles.y, 0);
            }
        }*/
        #endregion
        #region Interact
        void InteractHandler()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Ray interact;
                interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                RaycastHit hitInf;
                if (Physics.Raycast(interact, out hitInf, 10))
                {
                    #region NPC Tag
                    if (hitInf.collider.CompareTag("NPC"))
                    {

                        Debug.Log("Hit the NPC");


                    }
                    #endregion
                    /* #region Item
                     if (hitInf.collider.CompareTag("Item"))
                     {
                         Debug.Log("Hit Item");
                     }
                     #endregion*/
                }
            }

        }
        #endregion
    }
}


