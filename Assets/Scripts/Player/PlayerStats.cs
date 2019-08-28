using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public partial class Player {
    public class PlayerStats {

        Player player;

        float health = 100f;
        public float Health {
            get { return health; }
            set {
                health = value;
                if (health > 100f) {
                    health = 100f;
                }
                if (health < 0f) {
                    health = 0f;
                }
            }
        }

        // empty constructor for unit test purposes
        public PlayerStats() {}
        public PlayerStats(Player _player) {
            player = _player;
        }
    }

}


