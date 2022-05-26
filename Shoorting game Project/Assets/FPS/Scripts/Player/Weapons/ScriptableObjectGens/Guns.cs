using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FPS
{
    [CreateAssetMenu(fileName ="New Gun", menuName = "Gun")]
    public class Guns : ScriptableObject
    {
        public string name;
        public GameObject prefab;
        public int damage;
        public int burst; //0 - semi | 1 automatic \ 2 or more for burst fire
        public int ammo; //total ammo
        public int clipSize; //magazine size
        public float aimSpeed;
        public float fireRate;
        public float reload; //reload wait time for the gun to reload
        public float bloom;
        public float recoil;
        public float kickBack;
        

        private int clip;//current ammmo in clip
        private int stash; //current ammo

        public void Initialize()
        {
            stash = ammo;
            clip = clipSize;
        }

        public bool FireBullet()
        {
            if (clip > 0)
            {
                clip -= 1;
                return true;
            }
            else
            {
                return false;
            } 
        }

        public void Reload()
        {
            stash += clip;
            clip = Mathf.Min(clipSize, stash); //fills the magazine size based the size
            stash -= clip;
        }

        public int GetStash()
        {
            return stash;
        }

        public int GetClip()
        {
            return clip;
        }


    }

}
