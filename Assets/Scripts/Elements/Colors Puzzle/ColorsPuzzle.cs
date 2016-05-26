using UnityEngine;
using System.Collections;

public class ColorsPuzzle : Controllable
{
    static public Color Orange = new Color(1.0f, 0.5f, 0);
    static public Color Purple = new Color(0.5f, 0, 0.5f);
    public HeroStrong heroStrong;
    public HeroFast heroFast;
    public GameObject ColorPanel;

    private PuzzleController puzzleController;
    private ColorsPuzzleActivator colorsPuzzleActivator;
    private Color puzzleSolutionColor;
    private bool puzzleSolved = false;
    private bool puzzleFail = false;

    private void updateSphereColor()
    {        
        // TODO: In future, we will not update the sphere color, but we will assign its color
        // based on value ranges
        float rotationPosition = puzzleController.cursor.transform.localEulerAngles.z;

        if (rotationPosition >= 0 && rotationPosition < 120)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        
        else if (rotationPosition >= 120 && rotationPosition < 240)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }

        else if (rotationPosition >= 240 && rotationPosition < 360)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    private Color getRandomPuzzleColor()
    {
        Color color = new Color(0, 0, 0);
        float randomValue = Random.value;
        if (randomValue < 0.33f)
        {
            color = Orange;
        }
        else if (randomValue >= 0.33f && randomValue < 0.67f)
        {
            color = Purple;
        }
        else if (randomValue >= 0.67f)
        {
            color = Color.green;
        }
        return color;
    }
    
    void Start ()
    {
        puzzleSolutionColor = getRandomPuzzleColor();
        ColorPanel.GetComponent<Renderer>().material.color = puzzleSolutionColor;
        clean();
        colorsPuzzleActivator = GetComponent<ColorsPuzzleActivator>();
    }

    void Awake()
    {
        heroStrong = FindObjectOfType<HeroStrong>();
        heroFast = FindObjectOfType<HeroFast>();
        puzzleController = this.GetComponent<PuzzleController>();
    }

    public override void OnFocus()
    {
        if (!puzzleSolved)
        {
            ColorPanel.GetComponent<Renderer>().enabled = true;
            GetComponent<Renderer>().enabled = true;
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            puzzleController.SelectionBox1.GetComponent<Renderer>().enabled = true;
            puzzleController.SelectionBox2.GetComponent<Renderer>().enabled = true;
            puzzleController.cursor.GetComponent<Renderer>().enabled = true;
        }
    }

    public override void OnFocusOut()
    {
        clean();
    }

    void Update()
    {
        Color colorBox1 = puzzleController.SelectionBox1.GetComponent<Renderer>().material.color;
        Color colorBox2 = puzzleController.SelectionBox2.GetComponent<Renderer>().material.color;

        if (!colorBox1.Equals(puzzleController.getDefaultColor()) &&
            !colorBox2.Equals(puzzleController.getDefaultColor()))
        {
            if (puzzleSolutionColor.Equals(Orange))
            {
                if ((colorBox1.Equals(Color.red) && colorBox2.Equals(Color.yellow)) ||
                    (colorBox1.Equals(Color.yellow) && colorBox2.Equals(Color.red)))
                {
                    puzzleSolved = true;
                    return;
                }
            }

            else if (puzzleSolutionColor.Equals(Purple))
            {
                if ((colorBox1.Equals(Color.red) && colorBox2.Equals(Color.blue)) ||
                    (colorBox1.Equals(Color.blue) && colorBox2.Equals(Color.red)))
                {
                    puzzleSolved = true;
                    return;
                }
            }

            else if (puzzleSolutionColor.Equals(Color.green))
            {
                if ((colorBox1.Equals(Color.yellow) && colorBox2.Equals(Color.blue)) ||
                    (colorBox1.Equals(Color.blue) && colorBox2.Equals(Color.yellow)))
                {
                    puzzleSolved = true;
                    return;
                }
            }
            puzzleFail = true;
        }
    }

    void FixedUpdate()
    {
        if (IsActive)
        {
            updateSphereColor();
            if (puzzleSolved)
            {
                ColorPanel.GetComponent<Renderer>().enabled = false;
                HeroUtil.ChangeHero();
                colorsPuzzleActivator.activate();
            }            
            if (puzzleFail)
            {
                restartPuzzle();
                puzzleController.setCurrentBox(puzzleController.SelectionBox1);
                puzzleFail = false;
            }
        }
    }

    public void clean()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        puzzleController.SelectionBox1.GetComponent<Renderer>().enabled = false;
        puzzleController.SelectionBox2.GetComponent<Renderer>().enabled = false;
        puzzleController.cursor.GetComponent<Renderer>().enabled = false;
    }

    public void restartPuzzle()
    {
        puzzleController.SelectionBox1.GetComponent<Renderer>().material.color = puzzleController.getDefaultColor();
        puzzleController.SelectionBox2.GetComponent<Renderer>().material.color = puzzleController.getDefaultColor();
    }
}
