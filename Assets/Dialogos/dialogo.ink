INCLUDE globals.ink

-> main
-> END

=== main ===
AQUI
{not TalkedToZoroastros: -> primeira_conversa}
{TalkedToZoroastros && ZoroastrosQuestInProgress: -> quest_em_andamento}
{TalkedToZoroastros && ZoroastrosQuestCanBeFinished: -> quest_pode_terminar}
->DONE


=== primeira_conversa ===
    É a primeira vez que nos vemos! #speaker: Zoroastros
    {ArmaAtual == "Nenhuma": -> svard_desarmado}
    {ArmaAtual != "Nenhuma": -> svard_armado}
    -> DONE
    
    = svard_desarmado
        Está desarmado! #speaker: Zoroastros
        -> DONE
    = svard_armado
        Está armado! #speaker: Zoroastros
        -> DONE
=== quest_em_andamento ===
    A quest está em andamento! #speaker: Zoroastros
    -> DONE
=== quest_pode_terminar ===
    A quest pode terminar! #speaker: Zoroastros
    -> DONE
        
    
        






/*
INCLUDE globals.ink

->main
->END

= svard_resposta_1
        Meu destino é a Cidade das Moscas, onde uma importante missão me aguarda. #speaker: Svard
        Por acaso estamos perto?  #speaker: Svard
        Ei... Cidade das Moscas? Eu nunca ouvi falar de um lugar assim. #speaker: Coveiro
        Gurdim, vem cá, você parece ter coragem. Um lugar com este nome não deve ser coisa boa. #speaker: Coveiro
        Se você aceitar o meu desafio, talvez eu deixe você ficar com essa arma. #speaker: Coveiro
        Ué, que cara é essa? Por acaso achou que ela não tinha dono? Pois o dono sou eu! #speaker: Coveiro
        Mas não se preocupe, eu estava mesmo pensando em me desfazer dela. #speaker: Coveiro
        Só preciso que você me faça um favorzinho, ok? #speaker: Coveiro
        ** Tudo bem... De que tipo de favor estamos falando? #speaker: Svard
            A-ha! Vamos lá, você vai se divertir. #speaker: Coveiro
            É o seguinte: o patrão anda instatisfeito com o andamento dos serviços por aqui. #speaker: Coveiro
            O desafio é simples. Se você me ajudar a aparar as ervas daninhas que crescem em todo o cemitério,
            a espada é sua. #speaker: Coveiro
            De acordo? #speaker: Coveiro
            *** Ok! #speaker: Svard
                Estou esperando! E divirta-se! #speaker: Coveiro
                ->END
            *** Ok... #speaker: Svard
                Pense pelo lado positivo: você está ajudando um pobre coveiro a manter o seu emprego! #speaker: Coveiro
                ->END
        ** Se não temos outra escolha... #speaker: Svard
            Ânimo, garoto! Garanto que você vai se divertir. #speaker: Coveiro
            Está vendo essa vegetação selvagem que cresce em todo o cemitério? #speaker: Coveiro
            O patrão me pediu para acabar com ela. Se você me ajudar, então a espada é sua. #speaker: Coveiro
            Temos um acordo? #speaker: Coveiro
            *** Ok! #speaker: Svard
                Obrigado! Volte aqui quando terminar. E divirta-se! #speaker: Coveiro
                ->END
            *** Ok... #speaker: Svard
                ->END
                Até logo, gurdim! #speaker: Coveiro
    = svard_resposta_2
        A-ha! Eu gosto dessa empolgação. #speaker: Coveiro
        O desafio é simples: se você cortar todas as ervas daninhas que crescem no cemitério, coisas boas acontecerão. #speaker: Coveiro
        Mas não se esqueça de vir falar comigo quando terminar de cortar tudo! #speaker: Coveiro
        ** Ok! #speaker: Svard
            Divirta-se! #speaker: Coveiro
            ->END
        ** Ok... #speaker: Svard
            ->END


=== main ===
{not TalkedToZoroastros: -> primeira_conversa}
{TalkedToZoroastros && ZoroastrosQuestInProgress: -> quest_em_andamento}
{TalkedToZoroastros && ZoroastrosQuestCanBeFinished: -> quest_pode_terminar}

=== primeira_conversa ===
    {ArmaAtual == "Nenhuma": -> svard_desarmado}
    {ArmaAtual != "Nenhuma": -> svard_armado}
=== svard_desarmado ===
    Ora, se não é um gurdim! Pensei que tivesse visto um fantasma! #speaker: Coveiro
    (Ou pior: por um instante, pensei que fosse um capanga do barão...) #speaker: Coveiro
    Moleque, vem cá. Se estiver de bobeira, que tal um desafio para passar o tempo, hein? #speaker: Coveiro
    * Senhor, desculpe-me, mas não há tempo para desafios. #speaker: Svard
        -> svard_resposta_1
    * Que tipo de desafio? #speaker: Svard
        -> svard_resposta_2
    -> END

=== svard_armado ===
    Ei, gurdim! #speaker: Coveiro
    Cuidado com esse negócio! (Deus, esse moleque é fogo-na-roupa!) #speaker: Coveiro
    Acalme os nervos, vamos bater um papo. #speaker: Coveiro
    Você gostou dessa espada, hein? Que tal um desafio? #speaker: Coveiro
    * Senhor, desculpe-me, mas não há tempo para desafios. #speaker: Svard
        -> svard_resposta_1
    * Que tipo de desafio? #speaker: Svard
        -> svard_resposta_2
    -> END
    
=== quest_em_andamento ===
    Gurdim! Como vai o desafio? #speaker: Coveiro
    Ei... Parece que você ainda tem trabalho a fazer! #speaker: Coveiro
    Volte aqui quando não sobrar mais nenhuma, certo? #speaker: Coveiro
    Aperte X para atacar com a espada. Mas tenha cuidado para não balançar esse negócio perto de mim! #speaker: Coveiro
    -> END
    
=== quest_pode_terminar ===
    Ora, gurdim, mas que belo trabalho! #speaker: Coveiro
    Não posso negar, eu não teria feito melhor.  #speaker: Coveiro
    (Quem diria que hoje seria um dia tão tranquilo no cemitério, hein?) #speaker: Coveiro
    Aposto que você encontrou coisas muito interessantes explorando por aí, estou certo? #speaker: Coveiro
    * É, eu acho que sim... #speaker: Svard
        Heh! Mas que gurdim valente! #speaker: Coveiro
        A propósito, pode me chamar de Zoroastros. Sou o coveiro deste cemitério desde que eu tinha um pouco mais que a sua idade! #speaker: Zoroastros
        Vestido assim, você até me lembra de como eu era antigamente, hah-ha! #speaker: Zoroastros
        Sabe, garoto... Poucas crianças gostam de brincar por aqui. Elas ouvem os pais cochichando a respeito de coisas estranhas que andam acontecendo, e o povo só fala nisso ultimamente... #speaker: Zoroastros
        Bem, a verdade é que... Pessoas estão desaparecendo misteriosamente! #speaker: Zoroastros
        Você não estava sabendo disso? #speaker: Zoroastros
        ** Não mesmo! #speaker: Svard
            Escute bem, gurdim, pois o que vou te falar pode te poupar de uma enrascada! #speaker: Zoroastros
            Jamais, em hipótese alguma, venha aqui no cemitério à noite. É o melhor conselho que eu posso te dar! Se você tem amor à vida, guarde estas palavras. #speaker: Zoroastros
            Agora, gurdim, leve esta espada para longe daqui. Se não quiser arrumar problemas, não fique mostrando ela para qualquer um. E não fale para ninguém que você conseguiu ela comigo, de acordo? #speaker: Zoroastros
            De acordo! #speaker: Svard
            -> END
        ** Isso é alarmante! Conte-me mais! #speaker: Svard
            Pois é a mais dura realidade de nossos tempos, criança! Minha intenção não era te assustar, mas precisamos estar atentos a todo tipo de perigo. #speaker: Zoroastros
            Até que se descubra quem é o culpado por trás dos desaparecimentos, é assim que as coisas serão por aqui. #speaker: Zoroastros
            Mas agora, gurdim, dê o fora daqui com essa espada. Não mostre ela para ninguém se não quiser arrumar problemas. E se alguém perguntar alguma coisa, deixe meu nome fora disso, de acordo? #speaker: Zoroastros
            De acordo! #speaker: Svard
            -> END
    * Você nem imagina! #speaker: Svard
        Hah! Gurdim, não precisa me contar nada. Veja bem, este par de olhos já testemunhou coisas que deixariam
        homens com o dobro do meu tamanho dormindo de velas acesas e pedindo chupeta. #speaker: Coveiro
        Sabe, garoto, você parece mesmo ter coragem. #speaker: Coveiro
        Te vendo vestido assim, parece até que estou me vendo quando criança! Hah-ha! #speaker Coveiro
        Aliás, pode me chamar de Zoroastros. #speaker: Zoroastros
        Gurdim, escute o conselho que eu vou te dar: jamais, em hipótese alguma, venha aqui neste cemitério à noite! #speaker: Zoroastros
        Dizem que pessoas têm sumido misteriosamente! Por acaso você não sabia disso? #speaker: Zoroastros
        ** Não mesmo! #speaker: Svard
            Pois é a mais dura realidade de nossos tempos, criança! Precisamos estar atentos a todo tipo de perigo. #speaker: Zoroastros
            Se ninguém prender o culpado pelos desaparecimentos, o povo não poderá mais dormir em paz. #speaker: Zoroastros
            Agora, gurdim, suma daqui com essa espada. Sugiro que você não fique exibindo ela por aí. E veja bem: se perguntarem alguma coisa, eu não tenho nada a ver com isso, de acordo? #speaker: Zoroastros
            De acordo! #speaker: Svard
            -> END
        ** Mas é claro que não! #speaker: Svard
            Infelizmente, é a mais dura verdade! De preferência, fique em casa à noite. Até que se descubra o verdadeiro culpado pelos desaparecimentos, é o mais seguro a se fazer. #speaker: Zoroastros
            Agora, gurdim, vá embora daqui com essa espada. Se não quiser chamar atenção, mantenha ela escondida. E se alguém te perguntar, não diga nada a meu respeito, de acordo? #speaker: Zoroastros
            De acordo! #speaker: Svard
            ->END
        
        
    
        
    */




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