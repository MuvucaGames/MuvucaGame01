using UnityEngine;
using System.Collections;

public class ColorsPuzzle : ActionableElement 
{
    static public Color Orange = new Color(1.0f, 0.5f, 0);
    static public Color Purple = new Color(0.5f, 0, 0.5f);
    public HeroStrong heroStrong;
    public HeroFast heroFast;
    public bool isActive = false;
    public GameObject Terminal;

    private PuzzleController puzzleController;
    private ColorsPuzzleActivator colorsPuzzleActivator;
    private Color puzzleSolutionColor;
    private bool puzzleSolved = false;

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
        Terminal.GetComponent<Renderer>().material.color = puzzleSolutionColor;
        GetComponent<Renderer>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        puzzleController.SelectionBox1.GetComponent<Renderer>().enabled = false;
        puzzleController.SelectionBox2.GetComponent<Renderer>().enabled = false;
        puzzleController.cursor.GetComponent<Renderer>().enabled = false;
        colorsPuzzleActivator = GetComponent<ColorsPuzzleActivator>();
    }

    void Awake()
    {
        heroStrong = FindObjectOfType<HeroStrong>();
        heroFast = FindObjectOfType<HeroFast>();
        puzzleController = this.GetComponent<PuzzleController>();
    }

    public override void Activate()
    {
        Terminal.GetComponent<Renderer>().enabled = true;
        GetComponent<Renderer>().enabled = true;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        puzzleController.SelectionBox1.GetComponent<Renderer>().enabled = true;
        puzzleController.SelectionBox2.GetComponent<Renderer>().enabled = true;
        puzzleController.cursor.GetComponent<Renderer>().enabled = true;
        heroStrong.GetComponent<HeroControl>().enabled = false;
        heroFast.GetComponent<HeroControl>().enabled = false;
        heroStrong.StopWalk();
        heroFast.StopWalk();
        puzzleController.setControlActive();
        this.isActive = true;
    }

    public override void Deactivate()
    {
        this.isActive = false;
    }

    void Update()
    {
        Color colorBox1 = puzzleController.SelectionBox1.GetComponent<Renderer>().material.color;
        Color colorBox2 = puzzleController.SelectionBox2.GetComponent<Renderer>().material.color;

        if (puzzleSolutionColor.Equals(Orange))
        {
            if ((colorBox1.Equals(Color.red) && colorBox2.Equals(Color.yellow)) ||
                (colorBox1.Equals(Color.yellow) && colorBox2.Equals(Color.red)))
            {
                puzzleSolved = true;
            }
        }

        else if (puzzleSolutionColor.Equals(Purple))
        {
            if ((colorBox1.Equals(Color.red) && colorBox2.Equals(Color.blue)) ||
                (colorBox1.Equals(Color.blue) && colorBox2.Equals(Color.red)))
            {
                puzzleSolved = true;
            }
        }

        else if (puzzleSolutionColor.Equals(Color.green))
        {
            if ((colorBox1.Equals(Color.yellow) && colorBox2.Equals(Color.blue)) ||
                 (colorBox1.Equals(Color.blue) && colorBox2.Equals(Color.yellow)))
            {
                puzzleSolved = true;
            }
        }

        if (puzzleSolved)
        {
            clean();
            Terminal.GetComponent<Renderer>().enabled = false;
            colorsPuzzleActivator.activate();
        }
        else if (!colorBox1.Equals(puzzleController.getDefaultColor()) &&
                 !colorBox2.Equals(puzzleController.getDefaultColor()))
        {
            restartPuzzle();
            puzzleController.setCurrentBox(puzzleController.SelectionBox1);
        }
    }

    void FixedUpdate()
    {
        updateSphereColor();
    }

    public void clean()
    {
        puzzleController.deactivateControl();
        GetComponent<Renderer>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        heroStrong.GetComponent<HeroControl>().enabled = true;
        heroFast.GetComponent<HeroControl>().enabled = true;
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
