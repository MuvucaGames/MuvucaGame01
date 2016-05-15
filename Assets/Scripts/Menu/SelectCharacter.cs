using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectCharacter : MonoBehaviour
{

    [SerializeField]
    private Image btnStrong, btnFast, btnMainMaleSign, btnMainFemaleSign, btnMateMaleSign, btnMateFemaleSign;
    [SerializeField]
    private GameObject mainOpt1MaleStrong, mainOpt1MaleFast, mainOpt1FemaleStrong, mainOpt1FemaleFast;
    [SerializeField]
    private GameObject mainOpt2MaleStrong, mainOpt2MaleFast, mainOpt2FemaleStrong, mainOpt2FemaleFast;
    [SerializeField]
    private GameObject mateOpt1MaleStrong, mateOpt1MaleFast, mateOpt1FemaleStrong, mateOpt1FemaleFast;
    [SerializeField]
    private GameObject mateOpt2MaleStrong, mateOpt2MaleFast, mateOpt2FemaleStrong, mateOpt2FemaleFast;

    //Scale variables
    private Image btnMainSprite1, btnMainSprite2, btnMateSprite1, btnMateSprite2;
    private bool growing;
    [SerializeField]
    private float scaleFactor = 1.3f;
    private float delay, timer;


    //Selected variables
    private bool mainGenderIsMale, mainTypeIsStrong, mateGenderIsMale;
    private int mainOptSprite, mateOptSprite;

    void Awake()
    {
        delay = timer = 0.5f;

        mainGenderIsMale = true;
        mainTypeIsStrong = true;
        mateGenderIsMale = false;
        mainOptSprite = mateOptSprite = 1;

        growing = false;
    }

    void Update()
    {
        UpdateSprites();
        ShowSelected();
    }

    public void ConfirmationButton()
    {
        //Game.LoadLevel(GameLevel.PrototypeScene);
    }

    public void BackButton()
    {
        //Game.LoadLevel(GameLevel.MainMenu);
    }

    private void UpdateSprites()
    {
        mainOpt1MaleStrong.SetActive(mainGenderIsMale && mainTypeIsStrong);
        mainOpt1MaleFast.SetActive(mainGenderIsMale && !mainTypeIsStrong);
        mainOpt1FemaleStrong.SetActive(!mainGenderIsMale && mainTypeIsStrong);
        mainOpt1FemaleFast.SetActive(!mainGenderIsMale && !mainTypeIsStrong);

        mainOpt2MaleStrong.SetActive(mainGenderIsMale && mainTypeIsStrong);
        mainOpt2MaleFast.SetActive(mainGenderIsMale && !mainTypeIsStrong);
        mainOpt2FemaleStrong.SetActive(!mainGenderIsMale && mainTypeIsStrong);
        mainOpt2FemaleFast.SetActive(!mainGenderIsMale && !mainTypeIsStrong);

        mateOpt1MaleStrong.SetActive(mateGenderIsMale && !mainTypeIsStrong);
        mateOpt1MaleFast.SetActive(mateGenderIsMale && mainTypeIsStrong);
        mateOpt1FemaleStrong.SetActive(!mateGenderIsMale && !mainTypeIsStrong);
        mateOpt1FemaleFast.SetActive(!mateGenderIsMale && mainTypeIsStrong);

        mateOpt2MaleStrong.SetActive(mateGenderIsMale && !mainTypeIsStrong);
        mateOpt2MaleFast.SetActive(mateGenderIsMale && mainTypeIsStrong);
        mateOpt2FemaleStrong.SetActive(!mateGenderIsMale && !mainTypeIsStrong);
        mateOpt2FemaleFast.SetActive(!mateGenderIsMale && mainTypeIsStrong);
    }

    public void SetMainGender(bool value)
    {
        mainGenderIsMale = value;
    }

    public void SetMainType(bool value)
    {
        mainTypeIsStrong = value;
    }

    public void SetMainSprite(int value)
    {
        mainOptSprite = value;
    }

    public void SetMateGender(bool value)
    {
        mateGenderIsMale = value;
    }

    public void SetMateSprite(int value)
    {
        mateOptSprite = value;
    }

    private void ShowSelected()
    {
        timer -= Time.deltaTime;


        if (timer < 0)
        {
            if (mainTypeIsStrong)
            {
                AnimateSelectedButton(btnFast, btnStrong);
            }
            else
            {
                AnimateSelectedButton(btnStrong, btnFast);
            }

            if (mainGenderIsMale)
            {
                AnimateSelectedButton(btnMainFemaleSign, btnMainMaleSign);
            }
            else
            {
                AnimateSelectedButton(btnMainMaleSign, btnMainFemaleSign);
            }

            if (mateGenderIsMale)
            {
                AnimateSelectedButton(btnMateFemaleSign, btnMateMaleSign);
            }
            else
            {
                AnimateSelectedButton(btnMateMaleSign, btnMateFemaleSign);
            }

            SetActiveSprites();

            switch (mainOptSprite)
            {
                case 1:
                    AnimateSelectedButton(btnMainSprite2, btnMainSprite1);
                    break;
                case 2:
                    AnimateSelectedButton(btnMainSprite1, btnMainSprite2);
                    break;
                default:
                    break;
            }

            switch (mateOptSprite)
            {
                case 1:
                    AnimateSelectedButton(btnMateSprite2, btnMateSprite1);
                    break;
                case 2:
                    AnimateSelectedButton(btnMateSprite1, btnMateSprite2);
                    break;
                default:
                    break;
            }

            growing = !growing;
            timer = delay;
        }
    }

    private void AnimateSelectedButton(Image btnStatic, Image btnAnimated)
    {
        btnStatic.transform.localScale = Vector3.one;
        btnAnimated.transform.localScale = growing ? new Vector3(scaleFactor, scaleFactor, 1.0f) :
                                                                   Vector3.one;
    }

    //Get the active sprite for each button
    private void SetActiveSprites()
    {
        btnMainSprite1 = GetActiveImage("MainSprite1");
        btnMainSprite2 = GetActiveImage("MainSprite2");

        btnMateSprite1 = GetActiveImage("MateSprite1");
        btnMateSprite2 = GetActiveImage("MateSprite2");
    }

    private Image GetActiveImage(string name)
    {
        return GameObject.Find(name).GetComponentInChildren<Image>();
    }
}
