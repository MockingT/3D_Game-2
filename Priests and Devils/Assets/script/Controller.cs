using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;

namespace mygame
{
    public enum State { Start, End, Win, Lose, Start_to_End, End_to_Start };
    public interface Movement
    {
        void priest_start();
        void priest_end();
        void devil_start();
        void devil_end();
        void boat_move();
        void Left_off_boat();
        void Right_off_boat();
        void reset();
    }

    public class SSDirector : System.Object, Movement
    {
        // only one instance
        private static SSDirector _instance;
        public static SSDirector GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SSDirector();
            }
            return _instance;
        }

        public Controller CurScenceCtr;
        public State state = State.Start;

        //get game_obj
        private Model game_obj;
        public Model getModel()
        {
            return game_obj;
        }
        internal void setModel(Model someone)
        {
            if (game_obj == null)
            {
                game_obj = someone;
            }
        }

        // get off boat
        public void Left_off_boat()
        {
            game_obj.getOffTheboat(0);
        }
        public void Right_off_boat()
        {
            game_obj.getOffTheboat(1);
        }

        // priest gets on the boat
        public void priest_start()
        {
            game_obj.priS();
        }
        public void priest_end()
        {
            game_obj.priE();
        }

        // devil gets on the boat
        public void devil_start()
        {
            game_obj.delS();
        }
        public void devil_end()
        {
            game_obj.delE();
        }

        // boat moves
        public void boat_move()
        {
            game_obj.boat_move();
        }

        // reset
        public void reset()
        {
            state = State.Start;
            game_obj.Reset_game();
        }
    }


}

public class Controller : MonoBehaviour
{
    // Use this for initialization  
    void Start()
    {
        SSDirector one = SSDirector.GetInstance();
    }

}