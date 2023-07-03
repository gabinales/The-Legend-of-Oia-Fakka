INCLUDE globals.ink

->main
=== main ===
{not TalkedToZoroastros: -> primeira_conversa}
{TalkedToZoroastros}{ZoroastrosQuestInProgress} -> quest_em_andamento
{TalkedToZoroastros}{ZoroastrosQuestCompleted} -> quest_finalizada

=== primeira_conversa ===
    {ArmaAtual == "Nenhuma": -> svard_desarmado}
    {ArmaAtual != "Nenhuma": -> svard_armado}
    
    = svard_resposta_1
        Meu destino é a Cidade das Moscas, onde uma importante missão me aguarda. #speaker: Svard
        Por acaso estamos perto?  #speaker: Svard
        Ei... Cidade das Moscas? Eu nunca ouvi falar de um lugar assim. #speaker: Coveiro
        Garoto, vem cá, você parece ter coragem. Um lugar com este nome não deve ser coisa boa. #speaker: Coveiro
        Se você aceitar o meu desafio, talvez eu deixe você ficar com essa espada. #speaker: Coveiro
        Ué, que cara é essa? Por acaso achou que ela não tinha dono? Pois o dono sou eu! #speaker: Coveiro
        Mas não se preocupe, eu estava mesmo pensando em me desfazer dela. #speaker: Coveiro
        Só preciso que você me faça um favorzinho, ok? #speaker: Coveiro
        ** Tudo bem. De que tipo de favor estamos falando? #speaker: Svard
            A-ha! Vamos lá, você vai se divertir... #speaker: Coveiro
            É o seguinte: o patrão anda instatisfeito com o andamento dos serviços por aqui. #speaker: Coveiro
            O desafio é simples. Se você me ajudar a aparar as ervas daninhas que crescem em todo o cemitério, <>
            a espada é sua. #speaker: Coveiro
            De acordo? #speaker: Coveiro
            *** Ok! #speaker: Svard
                Não deixe uminha, meu garoto! E divirta-se! #speaker: Coveiro
                ->DONE
            *** Ok... #speaker: Svard
                Pense pelo lado positivo: você está ajudando um pobre coveiro a manter o seu emprego! #speaker: Coveiro
                ->DONE
        ** Se não temos outra escolha... #speaker: Svard
            Ânimo, garoto! Garanto que você vai se divertir. #speaker: Coveiro
            Está vendo essa vegetação selvagem que cresce em todo o cemitério? #speaker: Coveiro
            O patrão me pediu para acabar com ela. Se você me ajudar, a espada é sua. #speaker: Coveiro
            Temos um acordo? #speaker: Coveiro
            *** Ok! #speaker: Svard
                Obrigado! Volte aqui quando terminar. E divirta-se! #speaker: Coveiro
                ->DONE
            *** Ok... #speaker: Svard
                ->DONE
                Até mais, e divirta-se! #speaker: Coveiro
    = svard_resposta_2
        A-ha! Eu gosto dessa empolgação. #speaker: Coveiro
        O desafio é simples: se você cortar todas as ervas daninhas que crescem no cemitério, coisas boas acontecerão. #speaker: Coveiro
        Mas não se esqueça de vir falar comigo quando terminar de cortar tudo! #speaker: Coveiro
        ** Ok! #speaker: Svard
            Divirta-se! #speaker: Coveiro
            ->DONE
        ** Ok... #speaker: Svard
            ->DONE
                
=== svard_desarmado ===
    Ora, se não é só um moleque! Pensei que tinha visto um fantasma! #speaker: Coveiro
    (Ou pior: por um instante, pensei que fosse o patrão...) #speaker: Coveiro
    Garoto, venha cá. Se estiver de bobeira, que tal um desafio para passar o tempo? #speaker: Coveiro
    * Senhor, desculpe-me, mas não há tempo para desafios. #speaker: Svard
        -> svard_resposta_1
    
    ->END

=== svard_armado ===
    Ei, garoto, cuidado com esse negócio! (Deus, esse moleque é fogo-na-roupa!) #speaker: Coveiro
    Fique calmo, vamos bater um papo. #speaker: Coveiro
    Você gosta de espadas, certo? Que tal um desafio, hein? #speaker: Coveiro
    * Senhor, desculpe-me, mas não há tempo para desafios. #speaker: Svard
        -> svard_resposta_1
    * Que tipo de desafio? #speaker: Svard
        -> svard_resposta_2
    ->END
    
=== quest_em_andamento ===













/*INCLUDE globals.ink

Ola Svard #speaker: Camponês

{pokemon_name == "": ->Choices | ->ja_escoheu}

=== Choices ===
o que tu diz?
 +[Charmander]
  ->chosen("Charmander")

 +[Bulbasaur]
 ->chosen("Bulbasaur")

 +[Squirtle]
     ->chosen("Squirtle")

=== chosen(pokemon) ===
~pokemon_name = pokemon
Vc escolheu o {pokemon}. Nem fale mais comigo. Meu nome é Abu abdul al-Rahman #speaker: Abu abdul al-Rahman
ok #speaker: Svard
->END

=== ja_escoheu ===
vc ja escolheu o {pokemon_name}
->END
*/