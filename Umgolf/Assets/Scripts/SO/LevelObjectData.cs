using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelObjectData
{
	//Transform Components
    public  string _name;
    public Vector3 _position;
    public Quaternion _rotation;
    public Vector3 _scale;
	//Collider Data
	public bool _collider2DState;
    public Vector2 _collider2DSize;
	//Sprite Renderer Data
    public Color _spriteRendererColor;
	public int _spriteRendererLayer;
}
