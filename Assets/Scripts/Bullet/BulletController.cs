using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{    
    Rigidbody2D rb;
    string tag_bullet;
    int damage_bullet;

    private void Start()
    {
        this.tag = tag_bullet;

        rb = GetComponent<Rigidbody2D>();
      
    }
    
    
    public Rigidbody2D Get_rb { get { return rb; } }
    public int Get_bullet_damage { get { return damage_bullet; } set { damage_bullet = value; } }
    public string Set_Tag_Bullet { get { return tag_bullet; } set { tag_bullet = value; } }
}
