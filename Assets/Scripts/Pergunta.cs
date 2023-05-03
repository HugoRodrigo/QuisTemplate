using System;

[Serializable]
public class Pergunta
{
    public string pergunta = "";
    public string respostaCerta = "";
    public string respostaErradaA = "";
    public string respostaErradaB = "";
    public string respostaErradaC = "";
    public int qtdRespostas = 0;
    public Pergunta() { qtdRespostas = 2; }
    public Pergunta(string pergunta, string respostaCerta, string respostaErrada)
    {
        this.pergunta = pergunta;
        this.respostaCerta = respostaCerta;
        this.respostaErradaA = respostaErrada;
        qtdRespostas = 2;
    }
    public Pergunta(string pergunta, string respostaCerta, string respostaErradaA, string respostaErradaB)
    {
        this.pergunta = pergunta;
        this.respostaCerta = respostaCerta;
        this.respostaErradaA = respostaErradaA;
        this.respostaErradaB = respostaErradaB;
        qtdRespostas = 3;
    }
    public Pergunta(string pergunta, string respostaCerta, string respostaErradaA, string respostaErradaB, string respostaErradaC)
    {
        this.pergunta = pergunta;
        this.respostaCerta = respostaCerta;
        this.respostaErradaA = respostaErradaA;
        this.respostaErradaB = respostaErradaB;
        this.respostaErradaC = respostaErradaC;
        qtdRespostas = 4;
    }
}