using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeLargeChara : MonoBehaviour
{
    public GameObject Character;
    private Renderer renderer;
    public Text TxtTipsChara;
    public Text TxtTips;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(BeLarge());
    }

    IEnumerator BeLarge()
    {


        TxtTipsChara.text = "角色变大！";
        Character.transform.localScale = new Vector3(2, 2f, 2f);

        renderer.enabled = false;
        TxtTips.text = null;
        yield return new WaitForSeconds(1f);
        TxtTipsChara.text = null;
        yield return new WaitForSeconds(10f);

        Character.transform.localScale = new Vector3(1f, 1f, 1f);
        TxtTipsChara.text = "角色大小恢复！";
       
        yield return new WaitForSeconds(1f);
        TxtTipsChara.text = null;
    }
}
