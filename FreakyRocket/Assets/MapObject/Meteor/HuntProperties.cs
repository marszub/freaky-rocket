using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MapObject.Meteor
{
    [CreateAssetMenu(menuName = "Freaky Rocket/Hunt Properties")]
    public class HuntProperties : ScriptableObject
    {
        public List<float> HuntPhaseTimes;
    }
}