using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS
{
    public class Weapon : MonoBehaviour
    {

        #region Variables

        [SerializeField] Guns[] loadOut;
        [SerializeField] Transform weaponParent;
        [SerializeField] GameObject bulletHolePrefab;
        [SerializeField] LayerMask canBeShot;

        [HideInInspector] public Guns currentGunData;

        private GameObject currentWeapon;
        private int currentIndex;
        private float currentCoolDown = 0f;

        private bool isReloading;
        public bool isAiming = false;
        

        #endregion




        #region Monobehaviour Callbacks 

        private void Start()
        {

            foreach(Guns a in loadOut)
            {
                a.Initialize(); //calls initialize on each gun
                Equip(0); //by default gives you a weapon on launch
            }
            
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) //equip a gun when 1 is presssed
            {
                Equip(0);
               
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) //equip a gun when 1 is presssed
            {
                Equip(1);

            }
            if (Input.GetKeyDown(KeyCode.Alpha3)) //equip a gun when 1 is presssed
            {
                Equip(2);

            }

            if (currentWeapon != null) //if you have a gun
            {
                Aim(Input.GetMouseButton(1)); //checks whether you aim or not 

                if (loadOut[currentIndex].burst != 1) // if the weapon is not automatic 
                {
                    if (Input.GetMouseButtonDown(0) && currentCoolDown <= 0) //if you press the mouse button and cooled down
                    {
                        if (loadOut[currentIndex].FireBullet())
                        {
                            Shoot();
                        }
                        else if (loadOut[currentIndex].GetStash() > 0) //if you have any ammo left then only put it in the clip
                        {
                            StartCoroutine(Reload(loadOut[currentIndex].reload)); //instant reload when out of ammo    
                        }
                    }
                }
                else //if the gun is automatic 
                {
                    if (Input.GetMouseButton(0) && currentCoolDown <= 0) //if you press the mouse button and cooled down
                    {
                        if (loadOut[currentIndex].FireBullet())
                        {
                            Shoot();
                        }
                        else if (loadOut[currentIndex].GetStash() > 0) //if you have any ammo left then only put it in the clip
                        {
                            StartCoroutine(Reload(loadOut[currentIndex].reload)); //instant reload when out of ammo    
                        }
                    }
                }
                

                //Handle reload
                if(Input.GetKeyDown(KeyCode.R))
                {
                    if(loadOut[currentIndex].GetStash() > 0) //if you have any ammo left then only  reload
                        StartCoroutine(Reload(loadOut[currentIndex].reload));  //reload when R is pressed
                }

                //Cooldown - fireRate
                if(currentCoolDown > 0)
                {
                    currentCoolDown -= Time.deltaTime;
                }

                //weapon Position Elasticity
                currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 4f); //bring back the weapon to the origin - recoil restore
                
            }
        }

        #endregion




        #region Private Methods

        IEnumerator Reload(float wait)
        {
            isReloading = true;

            //here use if else to use either the reload animation or simply hide the weapon
            currentWeapon.SetActive(false); //hide the gun while reloading

            yield return new WaitForSeconds(wait); //wait for some time to reload

            loadOut[currentIndex].Reload();
            currentWeapon.SetActive(true); //make the gun visible after reloading 

            isReloading = false;
        }

        void Equip(int p_ind) //index as parameter - equips a weapon to the player
        {
            if (currentWeapon != null) //prevents duplication of weapon
            {
                if(isReloading) StopCoroutine("Reload"); //while reloading if the gun is changed then stop reloading 
                Destroy(currentWeapon);
            }

            currentIndex = p_ind;

            //Instantiate gun under the parent weapon game object based on the index passed
            GameObject newWeapon = Instantiate(loadOut[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
            newWeapon.transform.localPosition = Vector3.zero; // make position and rotation as 0 w.r.t parent ie., weaponParent
            newWeapon.transform.localEulerAngles = Vector3.zero;
            newWeapon.transform.rotation = Quaternion.identity; //added by me to resolve the ambiguous rotation of gun when instantiated.
            //Play Equip Animation for the weapon

            currentWeapon = newWeapon; //set new weapon as current weapon 
            currentGunData = loadOut[p_ind];
        }

        

        
        
        void Shoot()
        {
            Transform spawn = transform.Find("Cameras/Normal Camera");

            //Bloom
            Vector3 bloom = spawn.position + spawn.forward * 1000f;
            bloom += Random.Range(-loadOut[currentIndex].bloom, loadOut[currentIndex].bloom) * spawn.up; //gives a bit jitter to bullets to reduce accuracy a bit 
            bloom += Random.Range(-loadOut[currentIndex].bloom, loadOut[currentIndex].bloom) * spawn.right;
            bloom -= spawn.position;
            bloom.Normalize();


            //Raycast
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(spawn.position, bloom, out hit, 1000f, canBeShot) )
            {
                GameObject newHole = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.identity) as GameObject;
                newHole.transform.LookAt(hit.point + hit.normal); //make the hole look direction of normal
                Destroy(newHole, 5f);
            }

            //gun FX
            currentWeapon.transform.Rotate(-loadOut[currentIndex].recoil, 0, 0);
            currentWeapon.transform.position -= currentWeapon.transform.forward * loadOut[currentIndex].kickBack; //gives backward kick back

            //Cooldown
            currentCoolDown = loadOut[currentIndex].fireRate;

        }

       
        #endregion




        #region Public Methods

        public bool Aim(bool p_isAiming)
        {
            if(!currentWeapon) return false;
            if(isReloading) p_isAiming = false;

            isAiming = p_isAiming;
            
            Transform anchor = currentWeapon.transform.Find("Anchor"); //searching inside the current weapon prefab

            Transform stateAds = currentWeapon.transform.Find("States/ADS");  
            Transform stateHip = currentWeapon.transform.Find("States/HIP");


            if (p_isAiming)
            {
                
                //aim
                anchor.position = Vector3.Lerp(anchor.position, stateAds.position, Time.deltaTime * loadOut[currentIndex].aimSpeed);
            }
            else
            {
               
                //Normal position of gun
                anchor.position = Vector3.Lerp(anchor.position, stateHip.position, Time.deltaTime * loadOut[currentIndex].aimSpeed);
                                
            }

            return p_isAiming;

        }

        public void RefreshAmmo(Text text)
        {
            //Handles Ammo Ui on the screen
            int clip = loadOut[currentIndex].GetClip();
            int stash = loadOut[currentIndex].GetStash();

            text.text = clip.ToString("D2") + " / " + stash.ToString("D2"); // D2 says display two digits
        }

        #endregion



    }

}

