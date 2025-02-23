using UnityEngine;

public class SettingsDropdown : MonoBehaviour
{
    public GameObject option1;
    public GameObject option2;
    private bool isOpen = false;
    private Animator anim1;
    private Animator anim2;

    void Start()
    {
        anim1 = option1.GetComponent<Animator>();
        anim2 = option2.GetComponent<Animator>();

        // Initially disable buttons
        option1.SetActive(false);
        option2.SetActive(false);
    }

    public void ToggleDropdown()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            option1.SetActive(true);
            option2.SetActive(true);
        }

        anim1.SetBool("Show", isOpen);
        anim2.SetBool("Show", isOpen);

        if (!isOpen)
        {
            StartCoroutine(DisableAfterAnimation());
        }
    }

    private System.Collections.IEnumerator DisableAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        option1.SetActive(false);
        option2.SetActive(false);
    }
}
