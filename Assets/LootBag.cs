using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject prefabLoot;
    public List<Loot> listaLoot = new List<Loot>();

    Loot ItemDropado(){ // Há uma lista de drops possíveis, mas só 1 é sorteado
        int x = Random.Range(1,101); // A chance de drop é calculada em porcentagem
        int maisRaro = 100;
        int indiceMaisRaro = 0;
        List<Loot> itensPossiveis = new List<Loot>(); // Lista populada manualmente nos scriptable objects correspondentes
        foreach(Loot item in listaLoot){
            if(x <= item.chanceDrop){
                itensPossiveis.Add(item); // Quanto menor for a chance de drop, mais difícil a possibilidade
                if(item.chanceDrop <= maisRaro){
                    maisRaro = item.chanceDrop;
                    indiceMaisRaro = listaLoot.IndexOf(item);
                }
            }                                   // do sorteador escolher um número menor. 
        }
        /*
            Para dropar todos os itens possíveis:
            List<Loot> ItensDropados(){
                ...
                return itensPossiveis;
            }
        */
        if(itensPossiveis.Count > 0){ // De todos os drops possíveis, retorna o mais raro
            Loot itemDropado = itensPossiveis[indiceMaisRaro];
            return itemDropado;
        }
        Debug.Log("Sem loot");
        return null;
    }

    public void InstantiateLoot(Vector3 posicaoSpawn){ // Spawna o drop
        Loot itemDropado = ItemDropado();
        if(itemDropado != null){
            GameObject lootGameObject = Instantiate(prefabLoot, posicaoSpawn, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = itemDropado.spriteLoot; // Atualiza o sprite de acordo com o scriptable object

            // Efeitos visuais:
            float dropForce = 0.5f;
            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }
    }
    
}
