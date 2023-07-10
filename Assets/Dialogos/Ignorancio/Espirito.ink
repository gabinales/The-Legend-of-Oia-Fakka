INCLUDE ../globals.ink
EXTERNAL StartQuest(id)
EXTERNAL FinishQuest(id)

{questActive == 0: ->Dialogo1}
{questActive == 1: ->QuestEmAndamento}
{questActive == 2: ->Terminou}

=== Dialogo1 ===
oOooOoOoo...
Ooõoòo0oºOo.o0ó...
->Choices

=== Choices ===
 +[Ignorar]
    ok #speaker: Svard 
  ->END
  
 +[Quem está aí?]
    Tem alguém aí? 
    {HoraDoDia != 2: tão claro... | ->Dialogo2} 
     ->END
 
 === Dialogo2 ===
 você pode me ouvir? #speaker: ???
 ~SPAWN = 1
 ~questActive = 1
 ~StartQuest("RufarDosTambores")
 Me chamo Ignorâncio e agora você está em uma quest. Ache meu Vinil. #speaker: Ignorancio
 ->END
 
 === QuestEmAndamento ===
 e entao, como vai a busca?
 ->END
 
 === Terminou ===
  ~FinishQuest("RufarDosTambores")
 boa!!!
->END
 

