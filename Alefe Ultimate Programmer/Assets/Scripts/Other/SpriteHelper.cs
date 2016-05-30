using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Other
{
    public enum SpriteModes { Normal, Walk, Ninjutsu, Chakra, Combo1 };

    public class SpriteHelper
    {
        public Sprite[] normal, walk, ninjutsu, chakra, combo1;
        public int spritePosition = 0;
        public SpriteModes lastMode = SpriteModes.Normal;
        float lastPosition;
        bool walkingBack = true;

        public SpriteHelper(string Character)
        {
            normal = Resources.LoadAll<Sprite>("Sprites/Characters/" + Character + "/Normal");
            walk = Resources.LoadAll<Sprite>("Sprites/Characters/" + Character + "/Walk");
            ninjutsu = Resources.LoadAll<Sprite>("Sprites/Characters/" + Character + "/Ninjutsu");
            chakra = Resources.LoadAll<Sprite>("Sprites/Characters/" + Character + "/Chakra");
            combo1 = Resources.LoadAll<Sprite>("Sprites/Characters/" + Character + "/Combo1");
        }

        public Sprite LoadMode(GameObject gameObject, SpriteModes Mode, float player1position, float player2position, int player)
        {
            Sprite sprite;

            if (Mode != SpriteModes.Walk)
            {
                spritePosition++;
            }

            if (Mode != lastMode)
            {
                spritePosition = 0;
                lastMode = Mode;
            }

            Debug.Log(spritePosition);

            switch (Mode)
            {
                case SpriteModes.Normal:
                    if (spritePosition >= normal.Length)
                    {
                        spritePosition = 0;
                    }
                    sprite = gameObject.GetComponent<SpriteRenderer>().sprite = normal[spritePosition];
                    break;
                case SpriteModes.Walk:
                    bool direction;
                    if (player1position < player2position)
                        direction = player1position > lastPosition;
                    else
                        direction = player1position < lastPosition;

                    if (direction)
                    {
                        spritePosition++;
                        if (walkingBack)
                        {
                            spritePosition = 0;
                            walkingBack = false;
                        }
                        if (spritePosition >= walk.Length)
                        {
                            spritePosition = 0;
                        }
                    }
                    else
                    {
                        spritePosition--;
                        if (!walkingBack)
                        {
                            spritePosition = 5;
                            walkingBack = true;
                        }
                        if (spritePosition < 0)
                        {
                            spritePosition = 5;
                        }
                    }
                    sprite = gameObject.GetComponent<SpriteRenderer>().sprite = walk[spritePosition];
                    break;
                case SpriteModes.Ninjutsu:
                    if (spritePosition >= ninjutsu.Length)
                    {
                        spritePosition = 0;
                        if(player == 1)
                        {
                            CompletePlayerController.spriteMode = SpriteModes.Normal;
                        }
                        else
                        {
                            CompletePlayer2Controller.spriteMode = SpriteModes.Normal;
                        }
                    }
                    sprite = gameObject.GetComponent<SpriteRenderer>().sprite = ninjutsu[spritePosition];
                    break;
                case SpriteModes.Chakra:
                    if (spritePosition >= chakra.Length)
                    {
                        spritePosition = 2;
                    }
                    sprite = gameObject.GetComponent<SpriteRenderer>().sprite = chakra[spritePosition];
                    break;
                case SpriteModes.Combo1:
                    if (spritePosition >= combo1.Length)
                    {
                        spritePosition = 0;
                        if (player == 1)
                        {
                            CompletePlayerController.spriteMode = SpriteModes.Normal;
                        }
                        else
                        {
                            CompletePlayer2Controller.spriteMode = SpriteModes.Normal;
                        }
                    }
                    sprite = gameObject.GetComponent<SpriteRenderer>().sprite = combo1[spritePosition];
                    break;
                default:
                    return null;
            }
            
            lastPosition = player1position;
            return sprite;
        }
    }
}
