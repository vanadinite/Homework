﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaBit1
{
    class FormulaBit1
    {
        class Car
        {
            public int vSpeed = 0;
            public int hSpeed = 0;
            public int currRow = 0;
            public int currCol = 0;
            public int stepsCount = 0;
            public List<string> directionLog = new List<string>();

            public Car(int vPos, int hPos, int vSpd, int hSpd)
            {
                this.currRow = vPos;
                this.currCol = hPos;
                this.vSpeed = vSpd;
                this.hSpeed = hSpd;
            }

            public Car(int vPos, int hPos)
            {
                this.currRow = vPos;
                this.currCol = hPos;
            }
        }

        static void Main()
        {
            //build the track
            string[] bitTrack = new string[8];
            for (int row = 0; row< bitTrack.Length; row++)
            {
                bitTrack[row] = Convert.ToString(
                    int.Parse(Console.ReadLine()), 2);
            }
            //start by moving South
            //@top right corner of the trach
            Car car5 = new Car(0, bitTrack.Length-1); // seb vettel ftw
            car5.vSpeed = 1;
            car5.directionLog.Add("south");
            //check if starting position is 0
            if (bitTrack[car5.currRow][car5.currCol] != 0)
            {
                Console.WriteLine("No {0}", car5.stepsCount);
            }
            //get moving
            while(true)
            {
                //check if the next step in the current direction is free ( == 0 )
                if (bitTrack[car5.currRow+car5.vSpeed][
                    car5.currCol+car5.hSpeed] == 0)
                {
                    car5.currRow += car5.vSpeed;
                    car5.currCol += car5.hSpeed;
                    car5.stepsCount++;
                } 
                else //change direction of travel
                {

                }
            }
        }
    }
}