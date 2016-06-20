using UnityEngine;
using System.Collections;

public class PuzzleController : MonoBehaviour
{
    private bool controlActive = false;
    private bool _horizontalButtonPressed = false;
    private bool _colorSelected = false;
    private float _spinningPosition = 0.0f;
    private HeroStrong heroStrong;
    private HeroFast heroFast;
    public GameObject SelectionBox1;
    public GameObject SelectionBox2;

    private ColorsPuzzle colorsPuzzle;
    private Color defaultColor;

    private GameObject SelectionBoxCurrent;
    // TODO: Change this, still hard-coded
    public GameObject cursor;
    public float puzzleVelocity = 5.0f;

    void Awake ()
    {
        heroStrong = FindObjectOfType<HeroStrong>();
        heroFast = FindObjectOfType<HeroFast>();
        SelectionBoxCurrent = SelectionBox1;
        defaultColor = SelectionBox1.GetComponent<Renderer>().material.color;
        colorsPuzzle = this.GetComponent<ColorsPuzzle>();
    }

	void Update () 
    {
        if (colorsPuzzle.IsActive)
        {
            controlActive = true;
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                colorsPuzzle.clean();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                _colorSelected = true;
            }            
        }
        else
        {
            controlActive = false;
        }
	}

    void FixedUpdate()
    {
        _spinningPosition = Input.GetAxis("Horizontal") * puzzleVelocity;
        if (controlActive)
        {
            Vector3 sphereCenter = GetComponent<SphereCollider>().bounds.center;
            cursor.transform.RotateAround(sphereCenter, Vector3.forward, -_spinningPosition);   
        }

        if(_colorSelected)
        {
            float spinningPosition = cursor.transform.localEulerAngles.z;

            // Choose color of boxes based on angle
            // That is temporary code. Once we have art, we don´t need to constant change its color
            if (spinningPosition >= 0 && spinningPosition < 120)
            {
                SelectionBoxCurrent.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (spinningPosition >= 120 && spinningPosition < 240)
            {
                SelectionBoxCurrent.GetComponent<Renderer>().material.color = Color.blue;
            }
            else if (spinningPosition >= 240 && spinningPosition < 360)
            {
                SelectionBoxCurrent.GetComponent<Renderer>().material.color = Color.yellow;
            }

            if (SelectionBoxCurrent.Equals(SelectionBox1))
            {
                SelectionBoxCurrent = SelectionBox2;
            }

            _colorSelected = false;
        }
        DeactivateTriggerButtons();
    }

    public float getRotationPosition()
    {
        return _spinningPosition;
    }

    private void DeactivateTriggerButtons()
    {
        _colorSelected = false;
    }

    public Color getDefaultColor()
    {
        return defaultColor;
    }

    public void setCurrentBox(GameObject gameObj)
    {
        SelectionBoxCurrent = gameObj;
    }
}

