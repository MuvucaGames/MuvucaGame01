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
        Game.LoadLevel(GameLevel.PrototypeScene);
    }

    public void BackButton()
    {
        Game.LoadLevel(GameLevel.MainMenu);
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
                btnFast.transform.localScale = Vector3.one;
                btnStrong.transform.localScale = growing ? new Vector3(scaleFactor, scaleFactor, 1.0f) :
                                                                   Vector3.one;
            }
            else
            {
                btnStrong.transform.localScale = Vector3.one;
                btnFast.transform.localScale = growing ? new Vector3(scaleFactor, scaleFactor, 1.0f) :
                                                                  Vector3.one;
            }

            if (mainGenderIsMale)
            {
                btnMainFemaleSign.transform.localScale = Vector3.one;
                btnMainMaleSign.transform.localScale = growing ? new Vector3(scaleFactor, scaleFactor, 1.0f) :
                                                                   Vector3.one;
            }
            else
            {
                btnMainMaleSign.transform.localScale = Vector3.one;
                btnMainFemaleSign.transform.localScale = growing ? new Vector3(scaleFactor, scaleFactor, 1.0f) :
                                                                   Vector3.one;
            }

            if (mateGenderIsMale)
            {
                btnMateFemaleSign.transform.localScale = Vector3.one;
                btnMateMaleSign.transform.localScale = growing ? new Vector3(scaleFactor, scaleFactor, 1.0f) :
                                                                   Vector3.one;
            }
            else
            {
                btnMateMaleSign.transform.localScale = Vector3.one;
                btnMateFemaleSign.transform.localScale = growing ? new Vector3(scaleFactor, scaleFactor, 1.0f) :
                                                                   Vector3.one;
            }

            GetActiveSprites();

            switch (mainOptSprite)
            {
                case 1:
                    btnMainSprite2.transform.localScale = Vector3.one;
                    btnMainSprite1.transform.localScale = growing ? new Vector3(scaleFactor, scaleFactor, 1.0f) :
                                                                   Vector3.one;
                    break;
                case 2:
                    btnMainSprite1.transform.localScale = Vector3.one;
                    btnMainSprite2.transform.localScale = growing ? new Vector3(scaleFactor, scaleFactor, 1.0f) :
                                                                   Vector3.one;
                    break;
                default:
                    break;
            }

            switch (mateOptSprite)
            {
                case 1:
                    btnMateSprite2.transform.localScale = Vector3.one;
                    btnMateSprite1.transform.localScale = growing ? new Vector3(scaleFactor, scaleFactor, 1.0f) :
                                                                   Vector3.one;
                    break;
                case 2:
                    btnMateSprite1.transform.localScale = Vector3.one;
                    btnMateSprite2.transform.localScale = growing ? new Vector3(scaleFactor, scaleFactor, 1.0f) :
                                                                   Vector3.one;
                    break;
                default:
                    break;
            }


            growing = !growing;
            timer = delay;
        }
    }

    //Get the active sprite for each button
    private void GetActiveSprites()
    {
        btnMainSprite1 = GameObject.Find("MainSprite1").GetComponentInChildren<Image>();
        btnMainSprite2 = GameObject.Find("MainSprite2").GetComponentInChildren<Image>();

        btnMateSprite1 = GameObject.Find("MateSprite1").GetComponentInChildren<Image>();
        btnMateSprite2 = GameObject.Find("MateSprite2").GetComponentInChildren<Image>();
    }
}
