using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Other
{
    public enum ControlAction { Null, Down, Up, Pressing }

    public struct Direction { public ControlAction Left, Right, Up, Down; };

    public class InputHelper
    {
        public int Player { get; set; }

        public ControlAction A, B, X, Y, Start, Back, LB, RB, LT, RT;
        public Direction AnalogLeft, AnalogRight, DigitalPad;

        public InputHelper(int Player)
        {
            this.Player = Player;

            string[] CA = { "A", "B", "X", "Y", "AnalogLeft.Left", "AnalogLeft.Right", "AnalogLeft.Up", "AnalogLeft.Down" };

            KeyCode[] KC = {
                KeyCode.A, // A
                KeyCode.Space, // B
                KeyCode.Y, // X
                KeyCode.X, // Y
                KeyCode.LeftArrow, // Left Analog Left
                KeyCode.RightArrow, // Left Analog Right
                KeyCode.UpArrow, // Left Analog Up
                KeyCode.DownArrow // Left Analog Down
            };

            if (Player == 1)
            {
                for(int i = 0; i < CA.Length; i++)
                {
                    if (Input.GetKeyDown(KC[i]))
                    {
                        if(CA[i].Contains("AnalogLeft") || CA[i].Contains("AnalogRight") || CA[i].Contains("DigitalPad"))
                        {
                            FieldInfo propertyInfo = typeof(Direction).GetField(CA[i].Split('.')[1]);
                            object boxed = GetType().GetField(CA[i].Split('.')[0]).GetValue(this);
                            propertyInfo.SetValue(boxed, ControlAction.Down);
                            GetType().GetField(CA[i].Split('.')[0]).SetValue(this, (Direction)boxed);
                        }
                        else
                        {
                            GetType().GetField(CA[i]).SetValue(this, ControlAction.Down);
                        }
                    }
                    else if (Input.GetKey(KC[i]))
                    {
                        if (CA[i].Contains("AnalogLeft") || CA[i].Contains("AnalogRight") || CA[i].Contains("DigitalPad"))
                        {
                            FieldInfo propertyInfo = typeof(Direction).GetField(CA[i].Split('.')[1]);
                            object boxed = GetType().GetField(CA[i].Split('.')[0]).GetValue(this);
                            propertyInfo.SetValue(boxed, ControlAction.Pressing);
                            GetType().GetField(CA[i].Split('.')[0]).SetValue(this, (Direction)boxed);
                        }
                        else
                        {
                            GetType().GetField(CA[i]).SetValue(this, ControlAction.Pressing);
                        }
                    }
                    else if (Input.GetKeyUp(KC[i]))
                    {
                        if (CA[i].Contains("AnalogLeft") || CA[i].Contains("AnalogRight") || CA[i].Contains("DigitalPad"))
                        {
                            FieldInfo propertyInfo = typeof(Direction).GetField(CA[i].Split('.')[1]);
                            object boxed = GetType().GetField(CA[i].Split('.')[0]).GetValue(this);
                            propertyInfo.SetValue(boxed, ControlAction.Up);
                            GetType().GetField(CA[i].Split('.')[0]).SetValue(this, (Direction)boxed);
                        }
                        else
                        {
                            GetType().GetField(CA[i]).SetValue(this, ControlAction.Up);
                        }
                    }
                }
            }
        }
    }
}