using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class text : MonoBehaviour
    
{
    public TextMeshProUGUI textUI, textChoque;

    // Start is called before the first frame update
    void Start()
    {
        textUI.text = "El nùmero de veces que has chocado es el siguiente: ";
        textUI.text += GameManager.instance.choqueCounter;
        if(GameManager.instance.choqueCounter < 1)
        {
            textChoque.text = "Exelente trabajo. Estas listo para hacer la prueba, habla con tu supervisor.";
        }
        if (GameManager.instance.choqueCounter >= 1 && GameManager.instance.choqueCounter < 4)
        {
            textChoque.text = "Buen trabajo. Sigue practicando y estaras listo.";
        }
        if (GameManager.instance.choqueCounter >= 4)
        {
            textChoque.text = "Has chocado mucho. Te recomendamos volver a intentarlo.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
