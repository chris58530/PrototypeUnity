using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private Vector3 mTarget;  
    private Vector3 mScreen;  
    public int Value;
    public float ContentWidth;  
    public float ContentHeight;
    private Vector2 mPoint;
    public float FreeTime = 1.5F;
    public Font font;
    public Color color;
    public int fontSize;
    public float speed;
    void Start ()
    {
        mTarget = transform.position;  
        mScreen = Camera.main.WorldToScreenPoint(mTarget);  
        mPoint = new Vector2(mScreen.x, Screen.height - mScreen.y);  
        StartCoroutine("Free");  
    }  
  
    void Update()  
    {  
        transform.Translate(Vector3.up * (speed * Time.deltaTime));
        mTarget = transform.position;  
        mScreen = Camera.main.WorldToScreenPoint(mTarget); 
        mPoint = new Vector2(mScreen.x, Screen.height - mScreen.y);
    }  
  
    void OnGUI()  
    {  
        if(mScreen.z>0)  
        {  
            GUIStyle style = new GUIStyle();
            style.fontSize = fontSize;
            style.font = font;
            style.normal.textColor = color;
            GUI.Label(new Rect(mPoint.x, mPoint.y, ContentWidth, ContentHeight), "-"+Value.ToString(),style);
        }  
    }  
  
    IEnumerator Free()  
    {  
        yield return new WaitForSeconds(FreeTime);  
        Destroy(this.gameObject);  
    }  
}
