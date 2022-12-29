using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBH.Runtime
{
    public class MobData : ScriptableObject
    {
        public Sprite sprite;
        public float HP;
        public float hitboxRaidus;

        public MobData CreateCopy()
        {
            MobData temp = CreateInstance<MobData>();

            temp.name = name;
            temp.sprite = sprite;
            temp.HP = HP;
            temp.hitboxRaidus = hitboxRaidus;

            return temp;
        }

        public void Overwrite(MobData other)
        {
            name = other.name;
            sprite  = other.sprite;
            HP = other.HP;
            hitboxRaidus = other.hitboxRaidus;
        }
    }
}
