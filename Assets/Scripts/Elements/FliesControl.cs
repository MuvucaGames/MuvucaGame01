using UnityEngine;
using System.Collections;

public class FliesControl : MonoBehaviour {

    float speed = 0.05f;

    public Transform PontoA;
    public Transform PontoB;
    public GameObject FliePrefab;
    public int NumFlies = 1;
    [Tooltip("Mostrar o quadrado verde das moscas?")]
    public bool _debug = true;
    
    private float _minY, _maxY, _minX, _maxX;
    private Vector3 PontoA2, PontoB2;
    private GameObject[] _flieList;

    void Start()
    {
        //Cálculo dos bounds, para saber até onde as moscas podem voar
        _minY = Mathf.Min(PontoA.transform.localPosition.y, PontoB.transform.localPosition.y);
        _maxY = Mathf.Max(PontoA.transform.localPosition.y, PontoB.transform.localPosition.y);
        _minX = Mathf.Min(PontoA.transform.localPosition.x, PontoB.transform.localPosition.x);
        _maxX = Mathf.Max(PontoA.transform.localPosition.x, PontoB.transform.localPosition.x);
        
        //Criando as moscas
        _flieList = new GameObject[NumFlies];
        for (int i = 0; i < NumFlies; i++)
        {
            GameObject flieAux = (GameObject)Instantiate(FliePrefab);
            flieAux.transform.SetParent(this.transform);
            flieAux.transform.localPosition = new Vector3(Random.Range(_minX + 0.2f, _maxX - 0.2f), Random.Range(_minY + 0.2f, _maxY - 0.2f), 0.0f);
            flieAux.SetActive(true);
            _flieList[i] = flieAux;
        }
        
    }

    void Update()
    {
        //Um código de wander bem simples, se precisar vejo outra lógica para ele
        foreach (GameObject flie in _flieList)
        {
            flie.transform.localPosition = Vector3.Lerp(flie.transform.localPosition,
                                     flie.transform.localPosition + new Vector3(Random.Range(-0.5f, 0.5f) * speed, Random.Range(-0.5f, 0.5f) * speed,
                                     0), Time.time);

            //aqui é o clamp para manter as moscas dentro do bound
            flie.transform.localPosition = new Vector3(Mathf.Clamp(flie.transform.localPosition.x, _minX, _maxX), Mathf.Clamp(flie.transform.localPosition.y, _minY, _maxY), 0);
        }
    }

    void OnDrawGizmos()
    {
        //só o desenho do quadrado verde e do circulo, para facilitar a visualização de onde as moscas podem voar
        if (_debug)
        {
            PontoA2 = new Vector3(PontoB.position.x, PontoA.position.y, 0.0f);
            PontoB2 = new Vector3(PontoA.position.x, PontoB.position.y, 0.0f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(PontoA.position, PontoA2);
            Gizmos.DrawLine(PontoA2, PontoB.position);
            Gizmos.DrawLine(PontoB.position, PontoB2);
            Gizmos.DrawLine(PontoB2, PontoA.position);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.1f * transform.localScale.magnitude);
        }
    }
}
