using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeSmallChara : MonoBehaviour
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
        StartCoroutine(BeSmall());
    }

    IEnumerator BeSmall()
    {

        TxtTipsChara.text = "角色变小！";
        Character.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

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
