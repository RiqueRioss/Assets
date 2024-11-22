using UnityEngine;
using TMPro;

public class PorcentagemVida : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Status status;
    private float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //status.health = maxHealth;
        UpdateHealthText();
    }


    public void UpdateHealthText()
    {
        //float percentage = (currentHealth / maxHealth) * 100;
        healthText.text = $"{status.health:F0}%"; // Formata para zero casas decimais
    }
}
