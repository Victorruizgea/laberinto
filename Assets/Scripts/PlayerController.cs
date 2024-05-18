using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float velocidad;
    public Text countText;
    public Text winText;

	private Rigidbody rb;
    private int contador;

    public GameObject[] laberintos;
    private int niveles;
    void Start() 
	{
		rb = GetComponent<Rigidbody>();
        contador = 0;
        niveles = 0;
        SetCountText();
        for (int i = 0; i < laberintos.Length; i++)
        {
            laberintos[i].SetActive(i == 0);
        }

        winText.text = laberintos[niveles].name;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float posH = Input.GetAxis("Horizontal");
        float posV = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(posH, 0.0f, posV);

        rb.AddForce(movimiento * velocidad);

        if (rb.position.y < -10f)
        {
            winText.text = "Has perdido!! Inténtalo de nuevo :(";
            Invoke("QuitGame", 1f);
        }

    }

    void SetCountText()
    {
        countText.text = "Contador: " + contador.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("diamanteAmarillo"))
        {
            other.gameObject.SetActive(false);
            contador = contador + 5;
        }
        else if (other.gameObject.CompareTag("diamanteNegro"))
        {
            other.gameObject.SetActive(false);
            contador = contador + 30;
        }
        else if (other.gameObject.CompareTag("diamanteVerde"))
        {
            other.gameObject.SetActive(false);
            contador = contador + 15;

        }else if (other.gameObject.CompareTag("salida"))
        {
            if (niveles == laberintos.Length - 1)
            {
                winText.text = "¡Has ganado!";
                Invoke("QuitGame", 1f);
            }
            else
            {
                laberintos[niveles].SetActive(false);
                niveles++;
                laberintos[niveles].SetActive(true);
                winText.text = laberintos[niveles].name;
                rb.position = new Vector3(0, 0.5f, 0);
                rb.velocity = Vector3.zero;
            }
        }
        SetCountText();
        Debug.Log(other.gameObject.tag);
    }
 
    void QuitGame()
	{
		#if UNITY_EDITOR
		    UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
		    Application.OpenURL(webplayerQuitURL);
		#else
		    Application.Quit();
		#endif
	}



}