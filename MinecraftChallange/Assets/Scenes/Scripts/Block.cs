using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public Block()
    {
        blockType = BlockType.Air;
    }
    public BlockType blockType;
}

public enum BlockType
{
    Air = 0,
    Dirt
}

