using UnityEngine;
using System.Collections;

public class Esteira : MonoBehaviour {
    [SerializeField]
    private Transform RoldanaE;
    [SerializeField]
    private Transform RoldanaD;
    [SerializeField]
    private Transform Corpo;
    [SerializeField]
    private Transform Engrenagem;
    // Use this for initialization
    void Start () {
        Transform ttRoldanaE = RoldanaE;
        Transform ttCorpo = Corpo;
        Transform ttRoldanaD = RoldanaD;
        Quaternion rot = transform.localRotation;
        transform.localRotation = new Quaternion(0, 0, 0, transform.localRotation.w);

        //ttRoldanaE.transform.localScale = new Vector3(transform.localScale.y, transform.localScale.y,0);
        //ttRoldanaD.transform.localScale = new Vector3(transform.localScale.y, transform.localScale.y, 0);
        //ttCorpo.transform.localScale = new Vector3(transform.localScale.y - ttRoldanaE.localScale.y - ttRoldanaD.localScale.y, transform.localScale.y, 0);

        Transform ttEngrenagemInicial = Engrenagem;
        Transform ttEngrenagemAnt = ttEngrenagemInicial;
        Transform ttEngrenagemAtual = ttEngrenagemInicial;
        HingeJoint2D ttHingJEngrenagemAtual;
        for (int i = 0; i < (int)((ttCorpo.localScale.x) / ttEngrenagemInicial.localScale.x) - 1; i++)
        {
            ttEngrenagemAtual = (Transform)Instantiate(Engrenagem, new Vector3(ttEngrenagemAnt.position.x + (ttEngrenagemAnt.localScale.x + Engrenagem.localScale.x) / 2, ttEngrenagemAnt.position.y, 0), transform.localRotation);
            ttEngrenagemAtual.parent = ttCorpo.parent;
            ttHingJEngrenagemAtual = ttEngrenagemAtual.GetComponentInParent<HingeJoint2D>();
            ttHingJEngrenagemAtual.connectedBody = ttEngrenagemAnt.GetComponentInParent<Rigidbody2D>();
            ttEngrenagemAnt = ttEngrenagemAtual;
        }

        float pX = ttRoldanaD.position.x + ttRoldanaD.localScale.x / 2 + Engrenagem.localScale.x / 2;
        for (int i = 0; i < (int)((3.14 * ttRoldanaD.localScale.y / 2) / ttEngrenagemInicial.localScale.x); i++)
        {
            ttEngrenagemAtual = (Transform)Instantiate(Engrenagem, new Vector3(pX, ttEngrenagemAnt.position.y - ttEngrenagemAnt.localScale.x, 0), transform.localRotation);
            ttEngrenagemAtual.parent = ttCorpo.parent;
            ttEngrenagemAtual.Rotate(new Vector3(0, 0, 90));
            ttHingJEngrenagemAtual = ttEngrenagemAtual.GetComponentInParent<HingeJoint2D>();
            ttHingJEngrenagemAtual.connectedBody = ttEngrenagemAnt.GetComponentInParent<Rigidbody2D>();
            ttEngrenagemAnt = ttEngrenagemAtual;
        }

        float pY = ttCorpo.position.y - Corpo.localScale.y / 2 - Engrenagem.localScale.x / 2;
        for (int i = (int)((ttCorpo.localScale.x) / ttEngrenagemInicial.localScale.x); i > 0; i--)
        {
            ttEngrenagemAtual = (Transform)Instantiate(Engrenagem, new Vector3(ttEngrenagemAnt.position.x - (ttEngrenagemAnt.localScale.x + Engrenagem.localScale.x) / 2, pY, 0), transform.localRotation);
            ttEngrenagemAtual.parent = ttCorpo.parent;
            ttHingJEngrenagemAtual = ttEngrenagemAtual.GetComponentInParent<HingeJoint2D>();
            ttHingJEngrenagemAtual.connectedBody = ttEngrenagemAnt.GetComponentInParent<Rigidbody2D>();
            ttEngrenagemAnt = ttEngrenagemAtual;
        }
        pX = ttRoldanaE.position.x - ttRoldanaE.localScale.x / 2 - Engrenagem.localScale.x / 2;
        for (int i = (int)((3.14 * ttRoldanaD.localScale.y / 2) / ttEngrenagemInicial.localScale.x); i > 0; i--)
        {
            ttEngrenagemAtual = (Transform)Instantiate(Engrenagem, new Vector3(pX, ttEngrenagemAnt.position.y + ttEngrenagemAnt.localScale.x, 0), transform.localRotation);
            ttEngrenagemAtual.parent = ttCorpo.parent;
            ttEngrenagemAtual.Rotate(new Vector3(0, 0, 90));
            ttHingJEngrenagemAtual = ttEngrenagemAtual.GetComponentInParent<HingeJoint2D>();
            ttHingJEngrenagemAtual.connectedBody = ttEngrenagemAnt.GetComponentInParent<Rigidbody2D>();
            ttEngrenagemAnt = ttEngrenagemAtual;
        }

        ttHingJEngrenagemAtual = ttEngrenagemInicial.GetComponentInParent<HingeJoint2D>();
        ttHingJEngrenagemAtual.connectedBody = ttEngrenagemAtual.GetComponentInParent<Rigidbody2D>();

        transform.localRotation = rot;

    }

}
