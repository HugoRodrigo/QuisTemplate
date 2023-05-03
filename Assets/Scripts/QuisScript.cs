using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuisScript : MonoBehaviour
{
    public GameObject PainelRespostas;
    public TextMeshProUGUI Pergunta;

    public GameObject[] buttons;
    public GameObject buttonCerto;
    public GameObject buttonErrado;
    [SerializeField]
    public List<Pergunta> perguntas;
    public List<Pergunta> perguntasSelecionadas;
    Pergunta perguntaAtual;
    public string ProximoLevel;
    public string fileName;
    void Start()
    {
        LoadPerguntas();
        Random.InitState((int)(System.DateTime.Now.Second));
        SelecionarPergunta();
        IniciarButtons();
        SetResposta();
    }

    void Update()
    {

    }

    public bool SalvarPerguntas()
    {
        try
        {
            if (!System.IO.Directory.Exists(Application.dataPath + "/data/"))
            {
                System.IO.Directory.CreateDirectory(Application.dataPath + "/data/");
            }
            SerializableList<Pergunta> aux = new SerializableList<Pergunta>();
            aux.Perguntas = perguntas;
            string jsonData = JsonUtility.ToJson(aux);
            System.IO.File.WriteAllText(Application.dataPath + "/data/" + fileName + ".json", jsonData);
            Debug.Log(jsonData);
            Debug.Log("Salvo em: " + Application.dataPath + "/data/" + fileName + ".json");
            return true;
        }
        catch(System.Exception ex)
        {
            Debug.Log("Erro ao salvar: "+ex.ToString());
            return false;
        }
    }
    public bool LoadPerguntas()
    {
        try
        {

            if (!System.IO.Directory.Exists(Application.dataPath + "/data/"))
            {
                System.IO.Directory.CreateDirectory(Application.dataPath + "/data/");
            }
            string filePath = System.IO.Path.Combine(Application.dataPath + "/data/" + fileName + ".json");
            string data = System.IO.File.ReadAllText(filePath);
            SerializableList<Pergunta> aux = JsonUtility.FromJson<SerializableList<Pergunta>>(data);
            perguntas = aux.Perguntas;

            Debug.Log("Arquivo lido de: " + Application.dataPath + "/data/" + fileName + ".json");
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.Log("Erro no load: " + ex.ToString());
            return false;
        }
    }



    private void SetResposta()
    {
        
        for (int i = 0; i < buttons.Length; i++) buttons[i].transform.SetParent(null);
        for (int i = 0; i < buttons.Length; i++)
        {

            GameObject temp = buttons[i];
            int r = Random.Range(i, buttons.Length);
            buttons[i] = buttons[r];
            buttons[r] = temp;
            buttons[i].transform.SetParent(PainelRespostas.transform);
        }

    }
    void SelecionarPergunta()
    {
        if (perguntas.Count == 0 || perguntas == null)
        {
            SceneManager.LoadScene(ProximoLevel);
        }
        perguntaAtual = perguntas[Random.Range(0, perguntas.Count)];
        perguntasSelecionadas.Add(perguntaAtual);
        perguntas.Remove(perguntaAtual);
    }
    void IniciarButtons()
    {
        foreach (var button in buttons)
        {
            Destroy(button.gameObject);
        }
        buttons = new GameObject[perguntaAtual.qtdRespostas];
        buttons[0] = Instantiate(buttonCerto,PainelRespostas.transform);
        for (int i = 1; i < perguntaAtual.qtdRespostas; i++)
        {
            buttons[i] = Instantiate(buttonErrado, PainelRespostas.transform);
        }
        Pergunta.text = perguntaAtual.pergunta;
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = perguntaAtual.respostaCerta;
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = perguntaAtual.respostaErradaA;
        if (perguntaAtual.qtdRespostas > 2) 
        {
            buttons[2].GetComponentInChildren<TextMeshProUGUI>().text = perguntaAtual.respostaErradaB;
        }
        else 
        {
            //buttons[2].SetActive(false); 
        }
        if (perguntaAtual.qtdRespostas > 3)
        {
            buttons[3].GetComponentInChildren<TextMeshProUGUI>().text = perguntaAtual.respostaErradaC; 
        }
        else 
        { 
            //buttons[3].SetActive(false); 
        }

    }
    public void Certo()
    {
        Debug.Log("certo: " + perguntaAtual.respostaCerta);
        SelecionarPergunta();
        IniciarButtons();
        SetResposta();
    }
    public void Errado()
    {
        SetResposta();
    }
}
[System.Serializable]
public class SerializableList<T>
{
    public List<T> Perguntas;
}