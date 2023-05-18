using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public int roll = 2;
   [SerializeField] List<Sprite> die;
   private void Update() {
      if(Input.GetKey(KeyCode.W))
      {
      
         Roll(roll);
      }
   }
 public void RandomImage()
 {
    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
    renderer.sprite = die[Random.Range(0,die.Count)];
 }
 public void SetImage()
 {
    
    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
    renderer.sprite = die[roll - 1];
    print("hfaLDJ");
    GameManager.instance.MovePiece(roll);
 }

 public void Roll(int temp)
 {
    roll = temp;
    Animator animator = GetComponent<Animator>();
    animator.Play("Roll",-1,0f);
 }
}
