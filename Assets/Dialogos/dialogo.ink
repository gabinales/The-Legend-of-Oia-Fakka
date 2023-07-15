INCLUDE globals.ink
EXTERNAL StartQuest(id)
EXTERNAL FinishQuest(id)
EXTERNAL RecebeuChaveZoroastros()

EXTERNAL ChangeInkVariable(variableName, newValue)

-> main


=== main ===
{ArmaAtual != "Nenhuma" && CutsceneGrassblade == "Não viu cutscene": -> pegou_a_espada}
{CutsceneGrassblade == "Viu cutscene" && TalkedToZoroastros == "Não": -> conversou_depois_de_pegar_a_espada}

{CutsceneGrassblade == "Viu cutscene" && TalkedToZoroastros == "Sim": -> agora_va_e_corte}
{ZoroastrosDialogState == "Em progresso" && TalkedToZoroastros == "Sim": -> agora_va_e_corte}

{ArmaAtual == "Nenhuma" && TalkedToZoroastros == "Não": -> conversou_antes_de_pegar_a_espada}
{ZoroastrosDialogState == "Pode terminar": -> quest_pode_terminar}
{ZoroastrosDialogState == "Finalizada": -> quest_finalizada}

-> END

=== pegou_a_espada ===
    Ei, {~gurdim|garoto|moleque}! Cuidado com esse negócio! #speaker: Coveiro
    ~ ChangeInkVariable("CutsceneGrassblade", "Viu cutscene")
    (Deus, esse {~pirralho|diabo} é fogo-na-roupa!)
    -> END
    
    
=== conversou_depois_de_pegar_a_espada ===
    Você gostou dessa espada, hein? Que tal um desafio?
    * Senhor, desculpe-me, mas não há tempo para desafios. #speaker: Svard
        Meu destino é a Cidade das Moscas, onde uma importante missão me aguarda.
        Por acaso estamos perto?
        Ei... Cidade das Moscas? Eu nunca ouvi falar de um lugar assim. #speaker: Coveiro
        Gurdim, vem cá, você parece ter coragem. Um lugar com este nome não deve ser coisa boa.
        A minha raquete elétrica escangalhou, mas se você aceitar o meu desafio, talvez eu deixe você ficar com essa arma.
        Ué, que cara é essa? Por acaso achou que ela não tinha dono? Pois o dono sou eu!
        Mas não se preocupe, eu estava mesmo pensando em me desfazer dela.
        Só preciso que você me faça um favorzinho, ok?
        ** Tudo bem... De que tipo de favor estamos falando? #speaker: Svard
            A-ha! Vamos lá, você vai se divertir. #speaker: Coveiro
            É o seguinte: o patrão anda instatisfeito com o andamento dos serviços por aqui.
            O desafio é simples. Se você me ajudar a aparar as ervas daninhas que cresceram em todo o cemitério, a espada é sua!
            Caso já tenha se esquecido de como se usa uma lâmina, simplesmente aperte X para cortar. Mas cuidado ao mirar com essa coisa, pois só me resta um olho bom!
            Então estamos de acordo?
            *** Isso parece fácil. Pode deixar que eu me viro! #speaker: Svard
                ~ StartQuest("UmaEspadaChamadaGrassblade")
                ~ ChangeInkVariable("TalkedToZoroastros", "Sim")
                ~ ChangeInkVariable("CutsceneGrassblade", "XXX")
                ~ ChangeInkVariable("ZoroastrosDialogState", "Em progresso")
                Heh-heh! Estou esperando! #speaker: Coveiro
                ->END
    * Que tipo de desafio? #speaker: Svard
        A-ha! Eu gosto dessa empolgação. #speaker: Coveiro
        O desafio é simples: se você cortar todas as ervas daninhas que crescem no cemitério, coisas boas acontecerão.
        ** Só isso? Tudo bem... Vamos lá! #speaker: Svard
            ~ StartQuest("UmaEspadaChamadaGrassblade")
            ~ ChangeInkVariable("TalkedToZoroastros", "Sim")
            ~ ChangeInkVariable("CutsceneGrassblade", "XXX")
            ~ ChangeInkVariable("ZoroastrosDialogState", "Em progresso")
            É isso aí, {~gurdim|moleque|garoto}! Caso já tenha se esquecido como se usa uma lâmina, simplesmente aperte X para cortar. Mas cuidado, pois só me resta um único olho bom, hein! #speaker: Coveiro
            -> END
    //-> DONE
-> END
=== conversou_antes_de_pegar_a_espada ===
    Ora, se não é só um {~gurdim|garoto|moleque}! Pensei que tivesse visto um fantasma! #speaker: Coveiro
    (Ou pior: por um instante, pensei que fosse um capanga do barão...)
    {~Moleque|Garoto|Pirralho|Gurdim}, vem cá. Se você estiver de bobeira, que tal um desafio para passar o tempo, hein?
    * Senhor, desculpe-me, mas não há tempo para desafios. #speaker: Svard
        Meu destino é a Cidade das Moscas, onde uma importante missão me aguarda.
        Por acaso estamos perto?
        Ei... Cidade das Moscas? Eu nunca ouvi falar de um lugar assim. #speaker: Coveiro
        Gurdim, vem cá, você parece ter coragem. Um lugar com este nome não deve ser coisa boa.
        A minha raquete elétrica escangalhou, mas se você aceitar o meu desafio, talvez eu deixe você levar aquela espada ali no chão. Eu já estava mesmo pensando em como me desfazer dela.
        Só preciso que você me faça um favorzinho, ok?
        ** Tudo bem... De que tipo de favor estamos falando? #speaker: Svard
            A-ha! Vamos lá, você vai se divertir. #speaker: Coveiro
            É o seguinte: o patrão anda instatisfeito com o andamento dos serviços por aqui.
            O desafio é simples. Você só precisa me ajudar a acabar com as ervas daninhas que cresceram em todo o cemitério.
            A minha foice automática está no reparo, então você pode usar a própria espada para cortar.
            Aperte C para pegá-la e X para usá-la!
            *** Isso parece fácil. Pode deixar que eu me viro! #speaker: Svard
                ~ StartQuest("UmaEspadaChamadaGrassblade")
                ~ ChangeInkVariable("TalkedToZoroastros", "Sim")
                ~ ChangeInkVariable("ZoroastrosDialogState", "Em progresso")
                Heh-heh! Estou esperando! #speaker: Coveiro
                ->END
    * Que tipo de desafio? #speaker: Svard
        A-ha! Eu gosto dessa empolgação. #speaker: Coveiro
        O desafio é simples: se você cortar todas as ervas daninhas que cresceram no cemitério, coisas boas acontecerão.
        ** Só isso? Tudo bem... Vamos lá! #speaker: Svard
            ~ StartQuest("UmaEspadaChamadaGrassblade")
            ~ ChangeInkVariable("TalkedToZoroastros", "Sim")
            ~ ChangeInkVariable("ZoroastrosDialogState", "Em progresso")
            É isso aí, {~gurdim|moleque|garoto}! Caso já tenha se esquecido como se usa uma lâmina, simplesmente aperte C para pegar a espada do chão e X para cortar. Mas cuidado, pois só me resta um único olho bom, hein! #speaker: Coveiro
            -> END

=== agora_va_e_corte ===
    O que é que você tá esperando, {~gurdim|moleque|garoto}? Por acaso já se esqueceu como se usa uma espada? #speaker: Coveiro
    * Sim, é isso mesmo... #speaker: Svard
        Tudo bem, vou repetir outra vez: aperte C para pegá-la e X para usá-la! #speaker: Coveiro
        -> END
    * Já estou indo! #speaker: Svard
        -> END

=== quest_pode_terminar ===
    Ora, {~gurdim|moleque|garoto}, mas que belo trabalho! #speaker: Coveiro
    Não posso negar, eu não teria feito melhor.
    (Quem diria que hoje seria um dia tão tranquilo no cemitério, hein?)
    Aposto que você encontrou coisas muito interessantes explorando por aí, estou certo?
    * É, eu acho que sim... #speaker: Svard
        Heh! Mas que {~gurdim|moleque|garoto} valente! #speaker: Coveiro
        A propósito, pode me chamar de Zoroastros. Sou o coveiro deste cemitério desde que eu tinha um pouco mais que a sua idade. #speaker: Zoroastros
        Vestido assim, você até me lembra de como eu era antigamente, hah-ha!
        Sabe, garoto... Poucas crianças gostam de brincar por aqui. Elas ouvem os pais cochichando a respeito de coisas estranhas que andam acontecendo, e o povo só fala nisso ultimamente...
        Bem, a verdade é que... Pessoas estão desaparecendo misteriosamente!
        Você não estava sabendo disso?
        ** Não mesmo! #speaker: Svard
            Escute bem, {~gurdim|moleque|garoto}, pois o que vou te falar pode te poupar de uma enrascada! #speaker: Zoroastros
            Jamais, em hipótese alguma, venha aqui no cemitério à noite. É o melhor conselho que eu posso te dar! Se você tem amor à própria vida, guarde estas palavras.
            Agora, {~gurdim|moleque|garoto}, leve esta espada para longe daqui. Se não quiser arrumar problemas, não fique mostrando ela para qualquer um. E não fale para ninguém que você conseguiu ela comigo, de acordo?
            ~ FinishQuest("UmaEspadaChamadaGrassblade")
            ~ ChangeInkVariable("ZoroastrosDialogState", "Finalizada")
            De acordo! #speaker: Svard
            -> END
        ** Isso é alarmante! Conte-me mais! #speaker: Svard
            Pois é a mais dura realidade de nossos tempos, criança! Minha intenção não era te assustar, mas precisamos estar atentos a todo tipo de perigo. #speaker: Zoroastros
            Até que se descubra quem é o culpado por trás dos desaparecimentos, é assim que as coisas serão por aqui.
            Mas agora, {~gurdim|moleque|garoto}, dê o fora daqui com essa espada. Não mostre ela para ninguém se não quiser arrumar problemas. E se alguém perguntar alguma coisa, deixe meu nome fora disso, de acordo?
            ~ FinishQuest("UmaEspadaChamadaGrassblade")
            ~ ChangeInkVariable("ZoroastrosDialogState", "Finalizada")
            De acordo! #speaker: Svard
            -> END
    * Você nem imagina! #speaker: Svard
        Hah! {~Gurdim|Moleque|Garoto}, não precisa me contar nada. Veja bem, este par de olhos já testemunhou coisas que deixariam
        homens com o dobro do meu tamanho dormindo de velas acesas e pedindo chupeta. #speaker: Coveiro
        Sabe, garoto, você parece mesmo ter coragem.
        Te vendo vestido assim, parece até que estou me vendo quando criança! Hah-ha!
        Aliás, pode me chamar de Zoroastros. #speaker: Zoroastros
        Gurdim, escute o conselho que eu vou te dar: jamais, em hipótese alguma, venha aqui neste cemitério à noite!
        Dizem que pessoas têm sumido misteriosamente! Por acaso você não sabia disso?
        ** Não mesmo! #speaker: Svard
            Pois é a mais dura realidade de nossos tempos, criança! Precisamos estar atentos a todo tipo de perigo. #speaker: Zoroastros
            Se ninguém prender o culpado pelos desaparecimentos, o povo não poderá mais dormir em paz.
            Agora, {~gurdim|moleque|garoto}, suma daqui com essa espada. Sugiro que você não fique exibindo ela por aí. E veja bem: se perguntarem alguma coisa, eu não tenho nada a ver com isso, de acordo?
            ~ FinishQuest("UmaEspadaChamadaGrassblade")
            ~ ChangeInkVariable("ZoroastrosDialogState", "Finalizada")
            De acordo! #speaker: Svard
            -> END
        ** Mas é claro que não! #speaker: Svard
            Infelizmente, é a mais dura verdade! De preferência, fique em casa à noite. Até que se descubra o verdadeiro culpado pelos desaparecimentos, é o mais seguro a se fazer. #speaker: Zoroastros
            Agora, {~gurdim|moleque|garoto}, vá embora daqui com essa espada. Se não quiser chamar atenção, mantenha ela escondida. E se alguém te perguntar, não diga nada a meu respeito, de acordo?
            ~ FinishQuest("UmaEspadaChamadaGrassblade")
            ~ ChangeInkVariable("ZoroastrosDialogState", "Finalizada")
            De acordo! #speaker: Svard
            ->END
=== quest_finalizada ===
    {~Gurdim|Moleque|Garoto}, como vai? O que ainda faz por aqui? #speaker: Zoroastros
    * Eu gostaria de ouvir mais a respeito das pessoas desaparecidas. #speaker: Svard
        {~Gurdim|Moleque|Garoto}, venha cá, eu sei o que você está pensando: "Eu sou jovem, tenho uma arma... Sou invencível como um rinoceronte-lanudo!". #speaker: Zoroastros
        Criança, isso é balela. Não tente bancar o herói. Siga na linha e quem sabe você não consegue chegar no seu destino sem nenhum arranhão, hein?
        Há perigos que você mal pode imaginar, e eles estão correndo soltos por aí, especialmente à noite. Deixe os problemas de gente grande com as autoridades.
        ** Pode deixar, sr. Zoroastros. #speaker: Svard
            E não se esqueça... Você não conseguiu essa espada comigo! #speaker: Zoroastros
            -> END
    * Por acaso há algum banheiro por aqui? #speaker: Svard
        Ora, mas é claro! #speaker: Zoroastros
        Ou você acha que eu faço as minhas necessidades no chão, como um animal?
        {EntregouChave == "N": -> zoroastros_entrega_a_chave}
        {EntregouChave == "S": -> zoroastros_ja_entregou_a_chave}
        -> END
            
        
=== zoroastros_entrega_a_chave ===
    O banheiro fica à direita. Você vai precisar passar por dentro da minha cabana.
    ~ RecebeuChaveZoroastros()
    ~ ChangeInkVariable("EntregouChave", "S")
    Pegue esta chave, {~gurdim|moleque|garoto}. E aproveite a vista, heh-heh!
    -> END
        
=== zoroastros_ja_entregou_a_chave ===
    Siga à direita e passe por dentro da cabana. Use a chave que eu te entreguei (aquela com o chaveiro maneiro) para abrir a porta.
    E não deixe de dar descarga quando terminar, {~gurdim|moleque|garoto}!
    -> END
