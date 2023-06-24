using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject prefabLoot;
    public List<ItemData> listaLoot = new List<ItemData>();

    ItemData ItemDropado(){ // Há uma lista de drops possíveis, mas só 1 é sorteado
        int x = Random.Range(1,101); // A chance de drop é calculada em porcentagem
        int maisRaro = 100;
        int indiceMaisRaro = 0;
        List<ItemData> itensPossiveis = new List<ItemData>(); // Lista populada manualmente nos scriptable objects correspondentes
        foreach(ItemData item in listaLoot){
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
            List<ItemData> ItensDropados(){
                ...
                return itensPossiveis;
            }
        */
        if(itensPossiveis.Count > 0){ // De todos os drops possíveis, retorna o mais raro
            ItemData itemDropado = itensPossiveis[indiceMaisRaro];
            return itemDropado;
        }
        Debug.Log("Sem loot");
        return null;
    }

    public void InstantiateLoot(Vector3 posicaoSpawn){ // Spawna o drop
        ItemData itemDropado = ItemDropado();
        if(itemDropado != null){
            GameObject lootGameObject = Instantiate(prefabLoot, posicaoSpawn, Quaternion.identity);
            lootGameObject.name = itemDropado.nomeLoot;
            //lootGameObject.GetComponent<SpriteRenderer>().sprite = itemDropado.spriteLoot; // Atualiza o sprite de acordo com o scriptable object
            
            Loot loot = lootGameObject.GetComponent<Loot>();
            loot.Initialize(itemDropado);

            // Efeitos visuais:
            float dropForce = 0.5f;
            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }
    }
    
}
